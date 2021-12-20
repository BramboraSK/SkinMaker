using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace SkinMaker
{
    public class AddFileLoader
    {
        public class MenuContentFile
        {
            public string[] OsuStdFiles { get; set; }
        }

        private static readonly string menuContentFilename = "MenuContent.json";
        public static MenuContentFile content;

        public static void LoadMenuContentFile()
        {
            content = JsonSerializer.Deserialize<MenuContentFile>(File.ReadAllText(menuContentFilename));
        }



    }
}
