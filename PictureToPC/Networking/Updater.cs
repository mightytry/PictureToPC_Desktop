using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.IO;

namespace PictureToPC.Networking
{
    internal static class Updater
    {
        public const string Author = "mightytry";
        public const string Repository = "PictureToPC_Desktop";


        public class Asset
        {
            public string browser_download_url { get; set; }
        }

        public class Root
        {
            public string tag_name { get; set; }
            public Asset[] assets { get; set; }
        }


        public async static void SearchUpdate(string old_version)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", Repository);
                Root? new_version = await Get_Online_Version($"https://api.github.com/repos/{Author}/{Repository}/releases/latest", client);
                if (new_version == null || old_version == null)
                {
                    return;
                }

                int[] ov = old_version.Replace("v", "").Split(".").Select(int.Parse).ToArray();
                int[] nv = new_version.tag_name.Replace("v", "").Split(".").Select(int.Parse).ToArray();

                if ((nv[0] > ov[0]) || (nv[0] == ov[0] && nv[1] > ov[1]) || (nv[0] == ov[0] && nv[1] == ov[1] && nv[2] > ov[2]))
                {
                    if (MessageBox.Show($"A new version of {Config.ProgramName} is available. Update from version {old_version} to {new_version.tag_name}. Do you want to download it?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task.Run(() =>
                        {
                            MessageBox.Show("Downloading...", "Update", MessageBoxButtons.OK);
                        }).ConfigureAwait(false);
                        using (HttpResponseMessage response = await client.GetAsync(new_version.assets[0].browser_download_url))
                        using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                        using (FileStream file = File.OpenWrite(Path.Combine(Path.GetTempPath(), "Update.exe")))
                        {
                            await streamToReadFrom.CopyToAsync(file);
                        }
                        Process.Start(Path.Combine(Path.GetTempPath(), "Update.exe"));
                        
                        Environment.Exit(0);
                    }
                }

            }
        }
        

        private async static Task<Root?> Get_Online_Version(string url, HttpClient client)
        {
#if !DEBUG
            try
            {
#endif
                string response = await client.GetStringAsync(url);
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);
                return myDeserializedClass;
#if !DEBUG
            }
            catch (Exception )
            {
                return null;
            }
#endif
        }

    }
}
