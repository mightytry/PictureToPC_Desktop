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
        public static Thread network;
        public static Thread network2;
        

        public static void Start(Connection _conn, TextBox text, string _ip = "224.69.69.69", int _port = 42069)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);

            socket.Bind(ipep);

            IPAddress ip = IPAddress.Parse(_ip);

            socket.SetSocketOption(SocketOptionLevel.IP,SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
            
            conn = _conn;
            txtLog = text;
            
            if (network != null)
            {
                network.Abort();
            }

            network = new(new ThreadStart(new Action(() => { Recive(); })));

            network.IsBackground = true;

            network.Start();

            network2 = new(new ThreadStart(new Action(() => { hostRecive(); })));

            network2.IsBackground = true;

            network2.Start();
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

        public static void hostRecive()
        {
            while (true)
            {
                if (!conn.connecting && !conn.connected)
                { 
                    conn.Loop(new IPEndPoint(IPAddress.Parse(GetDefaultGateway().ToString()), 42069)); 
                }
                    
                Thread.Sleep(1000);
            }
            
        }

        public static void Recive()
        {
            while (true)
            {
                byte[] b = new byte[1024];
                EndPoint endPoint = new IPEndPoint(0,0);
                socket.ReceiveFrom(b, 0, 1024, SocketFlags.None, ref endPoint);

                Connect msg = JsonConvert.DeserializeObject<Connect>(Encoding.ASCII.GetString(b, 0, b.Length));

                msg.ip = endPoint.ToString().Split(':')[0];


                if (txtLog.Text == msg.code)
                {
#if RELEASE
                    try
                    {
#endif
                        conn.Loop(new IPEndPoint(IPAddress.Parse(msg.ip), msg.port));
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
}
