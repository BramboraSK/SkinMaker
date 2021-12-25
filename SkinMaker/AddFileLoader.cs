using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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
        public class GameplayFilesContent
        {
            public string Name { get; set; }
            public int DefWidth { get; set; }
            public int DefHeight { get; set; }
            public string Desc { get; set; }
        }
        public class MenuContentFile
        {
            public List<OsuStdFilesContent> OsuStdFiles { get; set; }
            public List<GameplayFilesContent> GameplayFiles { get; set; }
        }

        private static readonly string menuContentFilename = "ImageContent.json";

        public static MenuContentFile content = JsonSerializer.Deserialize<MenuContentFile>(File.ReadAllText(menuContentFilename));

        public static string GetFileDesc(string file, string parent)
        {
            switch (parent)
            {
                case "OsuStdFiles":
                    return content.OsuStdFiles.Find(e => e.Name == file).Desc;

                case "GameplayFiles":
                    return content.GameplayFiles.Find(e => e.Name == file).Desc;

                default:
                    return null;
            }

        }

        public static void CreateSelectedFile(string file, string skinName, string parent)
        {
            switch (parent)
            {
                case "OsuStdFiles":
                    CreateOsuStdFile(file, skinName);
                    break;

                case "GameplayFiles":
                    CreateGameplayFile(file, skinName);
                    break;

                default:
                    break;
            }
        }

        private static void CreateOsuStdFile(string file, string skinName)
        {
            int imageWidth = content.OsuStdFiles.Find(e => e.Name == file).DefWidth;
            int imageHeigth = content.OsuStdFiles.Find(e => e.Name == file).DefHeight;

            using (Bitmap image = new Bitmap(imageWidth, imageHeigth))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.Transparent);
                }
                image.Save(@$"{OptionsLoader.options.SkinsFolderPath}\{skinName}\{file}@2x.png", ImageFormat.Png);
            }
        }

        private static void CreateGameplayFile(string file, string skinName)
        {
            int imageWidth = content.GameplayFiles.Find(e => e.Name == file).DefWidth;
            int imageHeigth = content.GameplayFiles.Find(e => e.Name == file).DefHeight;

            using (Bitmap image = new Bitmap(imageWidth, imageHeigth))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.Transparent);
                }
                image.Save(@$"{OptionsLoader.options.SkinsFolderPath}\{skinName}\{file}@2x.png", ImageFormat.Png);
            }
        }
    }
}
