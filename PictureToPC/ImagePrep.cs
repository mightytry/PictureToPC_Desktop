using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Forms;
using System.Diagnostics;
using Point = System.Drawing.Point;

namespace PictureToPC
{
    internal static class ImagePrep
    {
        public static float GetFactor(Mat img, int maxsize)
        {
            return getFactor(img.Width, img.Height, maxsize);
        }
        public static float GetFactor(Size size, int maxsize)
        {
            return getFactor(size.Width, size.Height, maxsize);
        }
        private static float getFactor(int width, int height, int maxsize)
        {
            {
                float resizeFactor = 1;
                if (width > maxsize || height > maxsize)
                {
                    resizeFactor = width > height ? (float)maxsize / width : (float)maxsize / height;
                }
                return resizeFactor;
            }
        }
        public static List<Point[]> getCorners(Image img, int maxSize)
        {
            List<Point[]> result = new();

            Mat image = (img as Bitmap).ToMat();

            float f = GetFactor(image, maxSize);

            CvInvoke.Resize(image, image, new Size(0, 0), f, f);

            Mat gray = new(new Size(img.Width, img.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);

            //blur the image to reduce noise

            CvInvoke.CvtColor(image, gray, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);

            CvInvoke.GaussianBlur(gray, gray, new Size(7, 7), 0);

            VectorOfVectorOfPoint contours = new();

            CvInvoke.AdaptiveThreshold(gray, gray, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.Binary, 69, 0);

            CvInvoke.FindContours(gray, contours, null, mode: Emgu.CV.CvEnum.RetrType.Tree, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);


            //CvInvoke.DrawContours(image, contours, -1, new MCvScalar(255, 255, 255));

            //_ = CvInvoke.Imwrite("lol.png", gray);

            //sort the contours by arcLenght
            List<VectorOfPoint> sortedContours = new();
            for (int i = 0; i < contours.Size; i++)
            {
                if (CvInvoke.ContourArea(contours[i]) > 10000*f)
                {
                    sortedContours.Add(contours[i]);
                }
            }
            sortedContours.Sort((a, b) => CvInvoke.ArcLength(a, true).CompareTo(CvInvoke.ArcLength(b, true)));
            sortedContours.Reverse();

            //loop through the contours and find the 4 corners
            foreach (VectorOfPoint contour in sortedContours)
            {
                VectorOfPoint approx = new();
                CvInvoke.ApproxPolyDP(contour, approx, CvInvoke.ArcLength(contour, true) * 0.02, true);

                if (approx.Size is >= 4 and <= 10)
                {
                    Point[] corners = approx.ToArray();


                    result.Add(sortPointResize(img, corners, f));

                }
            }

            return result;
        }
        public static Point[] sortPointResize(Image img, Point[] corners, float factor)
        {
            Point tr;
            Point tl;
            Point bl;
            Point br;

            float f = (float)Math.Pow(factor, -1);

            int[] sum = new int[corners.Length];

            int[] diff = new int[corners.Length];

            for (int i = 0; i < corners.Length; i++)
            {
                sum[i] = corners[i].X + corners[i].Y;
                diff[i] = corners[i].X - corners[i].Y;
            }

            Debug.WriteLine(string.Join("", corners));

            tl = corners[Array.IndexOf(sum, sum.Min())];
            br = corners[Array.IndexOf(sum, sum.Max())];

            tr = corners[Array.IndexOf(diff, diff.Max())];
            bl = corners[Array.IndexOf(diff, diff.Min())];

            tl.Offset((int)((tl.X * f) - tl.X), (int)((tl.Y * f) - tl.Y));
            tr.Offset((int)((tr.X * f) - tr.X), (int)((tr.Y * f) - tr.Y));
            bl.Offset((int)((bl.X * f) - bl.X), (int)((bl.Y * f) - bl.Y));
            br.Offset((int)((br.X * f) - br.X), (int)((br.Y * f) - br.Y));
            
            return new Point[] { tl, tr, bl, br };
        }

        public static Image Crop(Image img, Point[] Corners)
        {
            Mat mat = (img as Bitmap).ToMat();

            Point tl = new(Corners[0].X, Corners[0].Y);
            Point tr = new(Corners[1].X, Corners[1].Y);
            Point bl = new(Corners[2].X, Corners[2].Y);
            Point br = new(Corners[3].X, Corners[3].Y);

            int widthA = (int)Math.Sqrt(Math.Pow(tr.X - tl.X, 2) + Math.Pow(tr.Y - tl.Y, 2));
            int widthB = (int)Math.Sqrt(Math.Pow(br.X - bl.X, 2) + Math.Pow(br.Y - bl.Y, 2));
            int maxWidth = Math.Max(widthA, widthB);

            int heightA = (int)Math.Sqrt(Math.Pow(tr.X - br.X, 2) + Math.Pow(tr.Y - br.Y, 2));
            int heightB = (int)Math.Sqrt(Math.Pow(tl.X - bl.X, 2) + Math.Pow(tl.Y - bl.Y, 2));
            int maxHeight = Math.Max(heightA, heightB);


            Mat dst = new(new Size(maxWidth, maxHeight), Emgu.CV.CvEnum.DepthType.Cv8U, 3);



            CvInvoke.WarpPerspective(mat, dst, CvInvoke.GetPerspectiveTransform(new PointF[] { tl, tr, bl, br }, new PointF[] { new Point(0, 0), new Point(maxWidth, 0), new Point(0, maxHeight), new Point(maxWidth, maxHeight) }), new Size(maxWidth, maxHeight));

            return dst.ToBitmap();
        }
        private static int getShape(Size size, float devidor)
        {
            int i = size.Width > size.Height ? (int)(size.Width / devidor) : (int)(size.Height / devidor);
            if (i % 2 == 0)
            {
                i += 1;
            }

            return i;
        }

        public static Image Contrast(Image img)
        {
            float f = GetFactor(img.Size, Form1.InternalResulution);

            Mat image = (img as Bitmap).ToMat();

            Mat small = new(new Size((int)(image.Width * f), (int)(image.Height * f)), Emgu.CV.CvEnum.DepthType.Cv8U, 3);

            CvInvoke.Resize(image, small, new Size(0, 0), f, f);

            Mat gray = new(new Size(small.Width, small.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);

            CvInvoke.CvtColor(small, gray, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);

            Mat thresh1 = new(new Size(small.Width, small.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);
            Mat thresh2 = new(new Size(small.Width, small.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);

            CvInvoke.AdaptiveThreshold(gray, thresh1, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.BinaryInv, getShape(small.Size, 20), 15);
            CvInvoke.AdaptiveThreshold(gray, thresh2, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC, Emgu.CV.CvEnum.ThresholdType.BinaryInv, getShape(small.Size, 2), 30);


            CvInvoke.BitwiseOr(thresh1, thresh2, thresh1);

            CvInvoke.Resize(thresh1, thresh1, new Size(image.Width, image.Height), 0, 0, Emgu.CV.CvEnum.Inter.Area);

            CvInvoke.BitwiseAnd(image, image, image, thresh1);

            CvInvoke.BitwiseNot(thresh1, thresh1);

            //Convert all black grb to white
            image.SetTo(new MCvScalar(255, 255, 255), thresh1);


            return image.ToBitmap();
        }
    }
}
