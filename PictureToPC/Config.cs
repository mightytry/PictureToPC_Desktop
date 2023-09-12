using Microsoft.Win32;
using Newtonsoft.Json;

namespace PictureToPC
{
    public class Config
    {
        public static string FolderName = "Mees Studio";
        public static string ProgramName = "PictureToPC";
        public static string FileName = "PictureToPC.json";

        public static string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + FolderName + "\\" + FileName;
        public Data Data;
        public string Version;
        public Config()
        {
            check();
            load();
            Version = Registry.GetValue(@$"HKEY_LOCAL_MACHINE\SOFTWARE\{FolderName}\{ProgramName}", "VersionName", "v0.0.0") as string;
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

    public class Data
    {
        public int InternalResulutionIndex;
        public int OutputResulutionIndex;

        public bool UsingExperimentalContrast;

        public string ConnectionCode;
        public string ConnectionName;


        public Data()
        {
            InternalResulutionIndex = 0;
            OutputResulutionIndex = 0;

            UsingExperimentalContrast = false;

            ConnectionCode = "";
            ConnectionName = "Unbekannt";
        }
    }
}

