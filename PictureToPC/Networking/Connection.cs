using Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PictureToPC.Networking
{
    internal class Connection
    {
        private TcpClient client;
        private NetworkStream stream;
        public bool connected;
        public bool connecting;
        public string connCode;
        private byte[] buffer;
        private readonly Form1 form;
        private Action<int> OnDataReceved;
        public Connection(Form1 form, Action<int> OnDataReceved)
        {
            this.OnDataReceved = OnDataReceved;
            this.form = form;
        }
        public string? Receive()
        {
            int bytesRead;
            if (connected)
            {
                try { bytesRead = stream.Read(buffer, 0, 1024); }
                catch (Exception)
                {
                    Close();
                    return null;
                }
                Debug.WriteLine(string.Join(",", buffer));
                return Encoding.UTF8.GetString(buffer, 0, bytesRead).TrimEnd();
            }
            return null;
        }
        public byte[]? Receive(int size)
        {
            int bytesRead = 0;
            int old = 0;
            byte[] data = new byte[size];
            if (connected)
            {
                while (bytesRead != size)
                {
                    try
                    {
                        bytesRead += stream.Read(data, bytesRead, size - bytesRead);
                    }
                    catch
                    {
                        Close();
                        return null;
                    }
                    OnDataReceved((int)((float)bytesRead / size * 100));
                    old = bytesRead;
                }
                stream.Read(buffer, 0, 1024-(size%1024));
                return data;
            }
            return null;
        }
        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
            try { form.Invoke(new Action(() => form.checkBox.Checked = false)); }
            catch { }

            connected = false;

        }
        public void Timeout()
        {
            while (connected)
            {
                try
                {
                    stream.Write(new byte[1], 0, 1);
                }
                catch (Exception)
                {
                    Close();
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        public void Loop(IPEndPoint endPoint)
        {
            connecting = true;
            try { client = new TcpClient(endPoint.Address.ToString(), endPoint.Port); }
            catch { return; connecting = false; }

            Thread timeout = new Thread(Timeout);
            timeout.IsBackground = true;
            timeout.Start();


            stream = client.GetStream();
            connecting = false;
            connected = true;
            form.Invoke(new Action(() => form.checkBox.Checked = true));

            
            while (connected)
            {
                buffer = new byte[1024];
                string? pictureData = Receive();

                if (pictureData == null)
                {
                    Close();
                    return;
                }

                if (pictureData.Length == 0)
                {
                    Close();
                    return;
                }
                
                int s = int.Parse(pictureData);

                if (s == -1)
                {
                    continue;
                }


                byte[]? pictureBytes = Receive(s);
                if (pictureBytes == null)
                {
                    Close();
                    return;
                }

                Bitmap im = new Bitmap(new MemoryStream(pictureBytes));

                im.RotateFlip(RotateFlipType.Rotate90FlipNone);


                //Mat mat = im.ToMat();

                //Mat dst = new(im.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3);

                //CvInvoke.CvtColor(mat, dst, Emgu.CV.CvEnum.ColorConversion.Rgba2Bgr);

                //Image img = dst.ToBitmap();

                form.SetImg(im);
            }
        }
    }
}
