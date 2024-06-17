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

        private static IPAddress ip;
        private static MulticastOption? multicastOption = null;

        public static void Start(Connection _conn, TextBox text, string _ip = "224.69.69.69", int _port = 42069)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);

            socket.Bind(ipep);

            ip = IPAddress.Parse(_ip);


            UpdateMCastOption();


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
            NetworkChange.NetworkAddressChanged += NetworkAddressChanged;

        }

        private static void UpdateMCastOption()
        {
            if (multicastOption != null)
            {
                try
                {
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, multicastOption);
                }
                catch
                { }
            }
            multicastOption = new MulticastOption(ip, GetIPAddress() ?? IPAddress.Any);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastOption);
        }

        private static void NetworkAddressChanged(object? sender, EventArgs e)
        {
            Stop(false);
            cts = new CancellationTokenSource();
            UpdateMCastOption();
            hostRecive();
            Recive();
        }

        public static IPAddress? GetIPAddress()
        {
            IPHostEntry localhost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress? localIpAddress = localhost.AddressList.FirstOrDefault((n) => n.AddressFamily == AddressFamily.InterNetwork, null);
            return localIpAddress;
        }

        public static IPAddress? GetDefaultGateway()
        {
            IPAddress? searchIP = GetIPAddress();
            if (searchIP == null) { return null; }
            var addresses = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var address in addresses) {
                if (address.GetIPProperties().UnicastAddresses.Any(l => l.Address.Equals(searchIP)))
                {
                    return address.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address;
                }
            }
            return null;
        }

        public static async void hostRecive()
        {
            IPAddress? ip = GetDefaultGateway();
            Client? client = null;
            if (ip == null)
            {
                cts.Cancel();
                return;
            }

            client = new Client(new IPEndPoint(ip, 42069), "Local");
            clients.Add(client);

            CancellationToken ct = cts.Token;


            while (!ct.IsCancellationRequested)
            {
                if (!client.connected && !client.connecting)
                {
                    client.Start(false);
                }
                try
                {
                    await Task.Delay(2000, ct);
                }
                catch
                {
                    break;
                }
            }
        }

        public static void Stop(bool closeSocket = false)
        {
            cts.Cancel();
            if (closeSocket) socket.Close();
            foreach (Client client in clients.ToArray())
            {
                client.Stop();
            }
            clients.Clear();
        }

        public static async void Recive()
        {
            CancellationToken ct = cts.Token;
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
