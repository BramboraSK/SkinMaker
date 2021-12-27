using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;

namespace SkinMaker
{
    public class AddFileLoader
    {

        public class FilesContent
        {
            public string Name { get; set; }
            public int DefWidth { get; set; }
            public int DefHeight { get; set; }
            public string Desc { get; set; }
        }

        public class MenuContentFile
        {
            public List<FilesContent> OsuStdFiles { get; set; }
            public List<FilesContent> GameplayFiles { get; set; }

            public object this[string name]
            {
                get
                {
                    var properties = typeof(MenuContentFile)
                            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var property in properties)
                    {
                        if (property.Name == name && property.CanRead)
                            return property.GetValue(this, null);
                    }

                    throw new ArgumentException("Can't find property");

                }
                set
                {
                    return;
                }
            }
        }

        private static readonly string menuContentFilename = "ImageContent.json";

        public static MenuContentFile content = JsonSerializer.Deserialize<MenuContentFile>(File.ReadAllText(menuContentFilename));

        public static string GetFileDesc(string file, string parent = null)
        {
            if (parent != null)
            {
                return ((List<FilesContent>)content[parent]).Find(e => e.Name == file).Desc;
            }
            else
            {
                return SearchFileDesc(file);
            }

        }

        public static void CreateSelectedFile(string file, string skinName, string parent)
        {
            List<FilesContent> list = (List<FilesContent>)content[parent];

            int imageWidth = list.Find(e => e.Name == file).DefWidth;
            int imageHeigth = list.Find(e => e.Name == file).DefHeight;

            using (Bitmap image = new Bitmap(imageWidth, imageHeigth))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.Transparent);
                }
                image.Save(@$"{OptionsLoader.options.SkinsFolderPath}\{skinName}\{file}@2x.png", ImageFormat.Png);
            }
        }

        private static string SearchFileDesc(string file)
        {
            foreach (PropertyInfo property in content.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(List<FilesContent>)) continue;

                List<FilesContent> list = (List<FilesContent>)content[property.Name];

                if (list.Any(item => item.Name == file))
                {
                    return list.Find(e => e.Name == file).Desc;
                }
                else
                {
                    continue;
                }

            }

            return "No description available.";
        }
    }
}
