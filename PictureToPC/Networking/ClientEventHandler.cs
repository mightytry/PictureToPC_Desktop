using Forms;

namespace PictureToPC.Networking
{
    internal class ClientEventHandler
    {
        public Client client;
        public static Form1 form;
        internal static string connectionName { get => form.Config.Data.ConnectionName; }
        internal static List<Client> clients = new List<Client>();

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
                clients.Add(client);
            }
            else
            {
                clients.Remove(client);
                Discovery.clients.Remove(client);
            }
            if (form == null)
            {
                return;
            }
            form.changeConenctionCount(clients);
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
