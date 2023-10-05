using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms.Design;
using Newtonsoft.Json;
using static PictureToPC.Networking.Messages;

namespace PictureToPC.Networking
{
    internal class Discovery
    {
        public static Socket socket;
        public static Connection conn;
        public static TextBox txtLog;
        public static List<Client> clients = new List<Client>();
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public static CancellationToken ct = cts.Token;

        public static void Start(Connection _conn, TextBox text, string _ip = "224.69.69.69", int _port = 42069)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);

            socket.Bind(ipep);

            IPAddress ip = IPAddress.Parse(_ip);

            socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
            
            conn = _conn;
            txtLog = text;

            hostRecive();
            Recive();
        }

        public static IPAddress GetDefaultGateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault();
        }

        public static async void hostRecive()
        {
            Client client = new Client(new IPEndPoint(IPAddress.Parse(GetDefaultGateway().ToString()), 42069), "Local");
            clients.Add(client);
            while (!ct.IsCancellationRequested)
            {
                if (!client.connected && !client.connecting)
                {
                    client.Start();
                }
                try
                {
                    await Task.Delay(1000, ct);
                }
                catch
                {
                    break;
                }
            }
            
        }

        public static void Stop()
        {
            cts.Cancel();
            socket.Close();
            foreach (Client client in clients.ToArray())
            {
                client.Stop();
            }
        }

        public static async void Recive()
        {
            while (!ct.IsCancellationRequested)
            {
#if RELEASE
                try
                {
#endif

                    byte[] b = new byte[1024];
                    EndPoint endPoint = new IPEndPoint(0,0);
                SocketReceiveFromResult erg;
                try
                    {
                        erg = await socket.ReceiveFromAsync(b, SocketFlags.None, endPoint, ct);
                    
                }
                    catch
                    {
                    continue;
                    }


                endPoint = erg.RemoteEndPoint;
                Connect msg = JsonConvert.DeserializeObject<Connect>(Encoding.ASCII.GetString(b, 0, erg.ReceivedBytes));

                    IPEndPoint ipEndPoint = (IPEndPoint)endPoint;
                    ipEndPoint.Port = msg.port;

                    if (clients.Find(x => x.endPoint.GetHashCode() == ipEndPoint.GetHashCode()) != null)
                {
                        continue;
                    }

                    if (txtLog.Text == msg.code)
                    {
                        Client client = new Client(ipEndPoint, msg.name);
                        clients.Add(client);
                        client.Start();
                    }
#if RELEASE
                }
                catch
                {
                    continue;
                }
#endif
            }
        }
    }
}
