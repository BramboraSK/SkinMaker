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
        public class OsuStdFilesContent
        {
            public string Name { get; set; }
            public int DefWidth { get; set; }
            public int DefHeight { get; set; }
            public string Desc { get; set; }
        }
        public class MenuContentFile
        {
            public List<OsuStdFilesContent> OsuStdFiles { get; set; }
        }

        private static readonly string menuContentFilename = "ImageContent.json";

        public static MenuContentFile content = JsonSerializer.Deserialize<MenuContentFile>(File.ReadAllText(menuContentFilename));

        public static string GetFileDesc(string file)
        {
           return content.OsuStdFiles.Find(e => e.Name == file).Desc;
        }
    }
}
