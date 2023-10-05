using Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PictureToPC.Networking
{
    internal class ClientEventHandler
    {
        public Client client;
        public static Form1 form;
        internal static string connectionName { get => form.Config.Data.ConnectionName; }
        internal static int connectionCount = 0;

        internal ClientEventHandler(Client pClient)
        {
            client = pClient;
        }

        internal void onDataReceved(int data)
        {
            if (form == null)
            {
                return;
            }
            form.UpdateProgressBar(data);
        }
        internal void onClientConnectionChanged(bool connected)
        {
            if (connected)
            {
                connectionCount++;
                
            }
            else
            {
                connectionCount--;
                Discovery.clients.Remove(client);
            }
            if (form == null)
            {
                return;
            }
            form.changeConenctionCount(connectionCount);
        }
        internal void onNewPicture(Bitmap image)
        {
            if (form == null)
            {
                return;
            }
            form.SetImg(image);
        }
    }
}
