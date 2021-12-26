using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace SkinMaker
{
    public class OptionsLoader
    {
        public class OptionsFile
        {
            public string OsuFolderPath { get; set; }
            public string SkinsFolderPath { get; set; }
            public string ImageEditorPath { get; set; }
        }

        private static readonly string optionsFilename = "options.json";
        public static OptionsFile options;

        public void Init()
        {            
            if (!File.Exists(optionsFilename))
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"osu!\Skins");

                OptionsFile newFile = new()
                {
                    SkinsFolderPath = Directory.Exists(dir) ? dir : "",
                    ImageEditorPath = File.Exists(@"C:\Windows\system32\mspaint.exe") ? @"C:\Windows\system32\mspaint.exe" : ""
                };

                File.WriteAllText(optionsFilename, JsonSerializer.Serialize(newFile));
            }

            options = JsonSerializer.Deserialize<OptionsFile>(File.ReadAllText(optionsFilename));
        }

        public static void Save()
        {
            File.WriteAllText(optionsFilename, JsonSerializer.Serialize(options));
        }
    }
}
