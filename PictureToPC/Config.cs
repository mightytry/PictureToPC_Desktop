using Newtonsoft.Json;

namespace PictureToPC
{
    internal class Config
    {
        public static string FolderName = "Mees Studio";
        public static string FileName = "ImageToPC.json";

        public static string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + FolderName + "\\" + FileName;
        public Data Data;
        internal Config()
        {
            check();
            load();
        }

        private void load()
        {
            string data = File.ReadAllText(FilePath);
            if (data == "")
            {
                Data = new Data();
                save();
            }
            else
            {
                Data = JsonConvert.DeserializeObject<Data>(data);
            }
        }

        public void Save()
        {
            save();
        }

        private void check()
        {
            //check if file and folder exist
            if (!File.Exists(FilePath))
            {
                _ = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + FolderName);
                File.Create(FilePath).Close();
                Data = new Data();
                save();
            }
        }

        private void save()
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(Data));
        }
    }

    internal class Data
    {
        public int InternalResulutionIndex;
        public int OutputResulutionIndex;

        public string PartnerIpAddress;
        internal Data(int iI, int oI, string pA)
        {
            InternalResulutionIndex = iI;
            OutputResulutionIndex = oI;

            PartnerIpAddress = pA;
        }
        internal Data()
        {
            InternalResulutionIndex = 0;
            OutputResulutionIndex = 0;

            PartnerIpAddress = "";
        }
    }
}

