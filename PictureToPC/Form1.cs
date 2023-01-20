using PictureToPC;
using PictureToPC.Networking;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<Button, MouseEventHandler> MoveEvent;
        private readonly Dictionary<Button, MouseEventHandler> UpEvent;
        private int ActiveCorner;
        private Connection Server;
        private List<Point[]> CornersList;
        public static int InternalResulution;
        public static int OutputResulution;
        private Image? prevImage;
        private Image orginalImage;
        private readonly List<Image> imageQueue;
        private static readonly int[] ResulutionIndex = new int[] { 1920, 2560, 3840 };
        private readonly Config Config;
        public CheckBox checkBox;

        public Form1()
        {
            InitializeComponent();

            checkBox = checkBox1;

            Server = new(this, UpdateProgressBar);

            Config = new();

            comboBox1.SelectedIndex = Config.Data.OutputResulutionIndex;
            comboBox2.SelectedIndex = Config.Data.InternalResulutionIndex;
            textBox1.Text = Config.Data.PartnerIpAddress;

            

            MoveEvent = new Dictionary<Button, MouseEventHandler>();
            UpEvent = new Dictionary<Button, MouseEventHandler>();

            imageQueue = new List<Image>();

            Resize += new EventHandler(ResizeMarkers);
            //NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(Discovery.NetworkAddressChanged); //maybe

            FormClosed += new FormClosedEventHandler((o, t) => { Server.Close(); });
        }
        
        private void GetCorners()
        {
            List<Point[]> corners = ImagePrep.getCorners(pictureBox1.Image, InternalResulution);
            if (corners.Count == 0)
            {
                return;
            }

            CornersList = corners;
            ActiveCorner = CornersList.Count > 1 ? 1 : 0;
            Invoke(new Action(() => { ResizeMarkers(null, null); }));

        }
        private void ResizeMarkers(object? sender, EventArgs? e)
        {
            if (CornersList == null || CornersList.Count == 0)
            {
                return;
            }

            put_marker(button6, CornersList[ActiveCorner][0]);
            put_marker(button7, CornersList[ActiveCorner][1]);
            put_marker(button8, CornersList[ActiveCorner][2]);
            put_marker(button9, CornersList[ActiveCorner][3]);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputResulution = ResulutionIndex[comboBox1.SelectedIndex];

            Config.Data.OutputResulutionIndex = comboBox1.SelectedIndex;
            Config.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            InternalResulution = ResulutionIndex[comboBox1.SelectedIndex];

            Config.Data.InternalResulutionIndex = comboBox2.SelectedIndex;
            Config.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4_Click(null, null);
            button3_Click(null, null);
            button2_Click(null, null);
        }

        private void button3_Click(object? sender, EventArgs? e)
        {
            pictureBox1.Image = ImagePrep.Contrast(pictureBox1.Image);
        }

        private void button2_Click(object? sender, EventArgs? e)
        {
            float f = ImagePrep.GetFactor(pictureBox1.Image.Size, OutputResulution);

            Bitmap resized = new(pictureBox1.Image, new Size((int)(pictureBox1.Image.Width * f), (int)(pictureBox1.Image.Height * f)));

            Clipboard.SetImage(resized);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (imageQueue.Count > 0)
            {
                prevImage = pictureBox1.Image;
                orginalImage = imageQueue[0];
                pictureBox1.Image = imageQueue[0];
                imageQueue.RemoveAt(0);
                GetCorners();
            }
        }

        private void button4_Click(object? sender, EventArgs? e)
        {
            pictureBox1.Image = ImagePrep.Crop(pictureBox1.Image, CornersList[ActiveCorner]);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            GetCorners();

        }
        public static float[] getRatioAndOffset(Size orginalSize, Size currentSize)
        {
            float[] output = new float[3];

            Size Current = currentSize;
            Size Original = orginalSize;

            if (Current.Width / (float)Current.Height > Original.Width / (float)Original.Height)
            {
                output[0] = Current.Height / (float)Original.Height;
                output[1] = (int)((Current.Width - (Original.Width * output[0])) / 2f);
                output[2] = 0;
            }
            else
            {
                output[0] = Current.Width / (float)Original.Width;
                output[1] = 0;
                output[2] = (int)((Current.Height - (Original.Height * output[0])) / 2f);
            }

            return output;
        }

        private void put_marker(Button marker, Point point)
        {
            float[] rao = getRatioAndOffset(pictureBox1.Image.Size, pictureBox1.Size);

            Point pos = new((int)((point.X * rao[0]) + rao[1]), (int)((point.Y * rao[0]) + rao[2]));

            marker.Location = pos.X > 0 && pos.Y > 0 && pos.X < Size.Width - 30 && pos.Y < Size.Height - 50 ? pos : new Point(0, 0);


        }

        private void markers_MouseDown(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (MoveEvent.ContainsKey(btn))
            {
                return;
            }
            FollowMouse(btn);
        }
        private new void Move(Button button)
        {
            Point pos = PointToClient(MousePosition);
            button.Location = pos.X > 0 && pos.Y > 0 && pos.X < Size.Width - 30 && pos.Y < Size.Height - 50 ? pos : new Point(0, 0);

        }

        private void FollowMouse(Button button)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            MoveEvent.Add(button, new MouseEventHandler((s, e) => Move(button)));
            UpEvent.Add(button, new MouseEventHandler((s, e) => UnfollowMouse(button)));
            button.MouseMove += MoveEvent[button];
            button.MouseUp += UpEvent[button];
        }
        private void UnfollowMouse(Button button)
        {
            button.MouseMove -= MoveEvent[button];
            button.MouseUp -= UpEvent[button];

            _ = MoveEvent.Remove(button);
            _ = UpEvent.Remove(button);
            groupBox1.Visible = true;
            groupBox2.Visible = true;

            int num = int.Parse(button.Text) - 1;

            CornersList[ActiveCorner][num] = ScreenposToImagepos(button.Location);

        }
        private Point ScreenposToImagepos(Point screenpos)
        {
            float[] rao = getRatioAndOffset(pictureBox1.Image.Size, pictureBox1.Size);

            Point output = new((int)((screenpos.X - rao[1]) / rao[0]), (int)((screenpos.Y - rao[2]) / rao[0]));

            return output;
        }

        internal void SetImg(Image img)
        {
            if (pictureBox1.Image == null)
            {
                Invoke(new Action(() => { orginalImage = img;  pictureBox1.Invalidate(); pictureBox1.Image = img; GetCorners(); }));
            }
            else
            {
                imageQueue.Add(img);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //prev
            if (CornersList == null)
            {
                return;
            }

            if (ActiveCorner > 0)
            {
                ActiveCorner--;
                ResizeMarkers(null, null);
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //next
            if (CornersList == null)
            {
                return;
            }

            if (ActiveCorner < CornersList.Count - 1)
            {
                ActiveCorner++;
                ResizeMarkers(null, null);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Config.Data.PartnerIpAddress = textBox1.Text;
            Config.Save();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Config.Save();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateProgressBar(int i)
        {
            Invoke(new Action(() =>
            {
                progressBar1.Value = i;
            }));
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Discovery.Start(Server, textBox1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = orginalImage;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Prev image
            if (prevImage != null)
            {
                imageQueue.Insert(0, pictureBox1.Image);
                orginalImage = prevImage;
                pictureBox1.Image = prevImage;
                prevImage = null;
                GetCorners();
            }
        }
    }
}