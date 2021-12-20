using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SkinMaker
{
    public class EditImages
    {
        public static string[] GetImageDim(string path)
        {
            using Bitmap img = new(path);
            string[] dim = { img.Width.ToString(), img.Height.ToString() };
            img.Dispose();
            return dim;
        }
    }
}
