using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureToPC.Networking
{
    internal class Messages
    {
        public class Connect
        {
            public int port;

            public string name;
            public string code;
        }
    }
}
