﻿using System;
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

        public static void CreateSelectedFile(string file, string skinName)
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
    }
}
