using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
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

            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

            conn = _conn;
            txtLog = text;
            text.KeyUp += (object sender, KeyEventArgs e) =>
            {
                foreach (Client client in clients.ToArray())
                {
                    if (client.Code != null && !client.Code.Equals(text.Text))
                    {
                        client.Stop();
                        clients.Remove(client);
                    }
                }
            };

            hostRecive();
            Recive();
        }

        public static IPAddress? GetDefaultGateway()
        {
           return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault((n) =>
                       n != null &&
                       n.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                       n.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                       n.IsReceiveOnly == false &&
                      !n.Name.StartsWith("vEthernet") &&
                       n.OperationalStatus == OperationalStatus.Up
            , null)?.GetIPProperties().UnicastAddresses.First((a) => a.Address.AddressFamily == AddressFamily.InterNetwork).Address;
        }

        public static async void hostRecive()
        {
            IPAddress? ip = GetDefaultGateway();
            Client? client = null;
            if (ip != null)
            {
                client = new Client(new IPEndPoint(ip, 42069), "Local");
                clients.Add(client);
            }
            
            while (!ct.IsCancellationRequested)
            {
                if (client != null && !client.connected && !client.connecting)
                {
                    client.Start();
                }
                try
                {
                    await Task.Delay(2000, ct);
                }
                catch
                {
                    break;
                }
                if (client == null || (!client.connected && !ip.Equals(GetDefaultGateway())))
                {
                    if (client != null)
                    {
                        client.Stop();
                        clients.Remove(client);
                    }
                    ip = GetDefaultGateway();
                    if (ip == null)
                    {
                        foreach (Client c in clients.ToArray())
                        {
                            c.Stop();
                        }
                        clients.Clear();
                        client = null;
                        continue;
                    }
                    client = new Client(new IPEndPoint(ip, 42069), "Local");
                    clients.Add(client);
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
                    EndPoint endPoint = new IPEndPoint(0, 0);
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
                        Client client = new Client(ipEndPoint, msg.name, msg.code);
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
