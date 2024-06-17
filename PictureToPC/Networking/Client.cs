using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PictureToPC.Networking
{
    internal class Client
    {
        internal string ServerName { private set; get; }
        public readonly IPEndPoint endPoint;
        private readonly TcpClient client;
        private NetworkStream stream;
        public bool connecting;
        public bool connected { get => client.Connected; }
        public string connCode;
        public readonly string? Code;
        private byte[] buffer;
        private readonly ClientEventHandler eventHandler;
        CancellationTokenSource source;


        public Client(IPEndPoint endPoint, string name, string? code = null)
        {
            eventHandler = new(this);
            ServerName = name;
            client = new TcpClient();
            this.endPoint = endPoint;
            source = new CancellationTokenSource();
            Code = code;
        }

        public async void Start(bool closeSocket = true)
        {
            CancellationToken token = source.Token;

            if (await Connect())
            {
                await Task.WhenAny(Loop(token), Timeout(token));
            }
            Close(source, closeSocket);
        }

        private async Task<bool> Connect()
        {
            connecting = true;
            try
            {
                await client.ConnectAsync(endPoint).WaitAsync(TimeSpan.FromSeconds(10));
                if (connected)
                {
                    connecting = false;
                    eventHandler.onClientConnectionChanged(true);

                    stream = client.GetStream();


                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        private async Task<string?> Receive(CancellationToken cToken)
        {
            int bytesRead;
            if (connected)
            {
                try { bytesRead = await stream.ReadAsync(buffer, 0, 1024, cToken); }
                catch (Exception)
                {
                    return null;
                }
                return Encoding.UTF8.GetString(buffer, 0, bytesRead).TrimEnd();
            }
            return null;
        }
        private async Task<byte[]?> Receive(int size, CancellationToken cToken)
        {
            int bytesRead = 0;
            byte[] data = new byte[size];
            if (connected)
            {
                while (bytesRead != size)
                {
                    try
                    {
                        bytesRead += await stream.ReadAsync(data, bytesRead, size - bytesRead, cToken);
                    }
                    catch
                    {
                        return null;
                    }
                    eventHandler.onDataReceved((int)((float)bytesRead / size * 100));
                }
                await stream.ReadAsync(buffer, 0, 1024 - (size % 1024), cToken);
                return data;
            }
            return null;
        }
        public void Stop()
        {
            source.Cancel();
        }

        private void Close(CancellationTokenSource cSource, bool closeAll = true)
        {
            cSource.Cancel();
            if (!connecting)
            {
                connecting = false;
                eventHandler.onClientConnectionChanged(false);
            }
            if (!closeAll) return;
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }

            if (client != null)
            {
                client.Close();
                client.Dispose();
            }

        }
        private async Task Timeout(CancellationToken cToken)
        {
            while (connected)
            {
                try
                {
                    await stream.WriteAsync(new byte[1], 0, 1, cToken);
                }
                catch (Exception)
                {
                    return;
                }
                await Task.Delay(1000, cToken);
            }
        }

        private async Task Loop(CancellationToken cToken)
        {
            //send name
            byte[] bytes = new byte[1024];
            Encoding.UTF8.GetBytes(ClientEventHandler.connectionName).CopyTo(bytes, 0);
            await stream.WriteAsync(bytes, 0, bytes.Length, cToken);

            while (!cToken.IsCancellationRequested)
            {
                buffer = new byte[1024];
                string? pictureData = await Receive(cToken);

                if (pictureData == null || pictureData.Length == 0)
                {
                    return;
                }
                int s;
                try
                {
                    s = int.Parse(pictureData);
                }
                catch
                {
                    return;
                }

                if (s == -1)
                {
                    continue;
                }


                byte[]? pictureBytes = await Receive(s, cToken);
                if (pictureBytes == null)
                {
                    return;
                }

                Bitmap im = new Bitmap(new MemoryStream(pictureBytes));

                im.RotateFlip(RotateFlipType.Rotate90FlipNone);


                //Mat mat = im.ToMat();
                //Mat dst = new(im.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 3
                //CvInvoke.CvtColor(mat, dst, Emgu.CV.CvEnum.ColorConversion.Rgba2Bgr
                //Image img = dst.ToBitmap();



                eventHandler.onNewPicture(im);
            }
        }
    }
}
