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
            Image img = Image.FromFile(path);
            string[] dim = { img.Width.ToString(), img.Height.ToString()};
            return dim;
        }
    }
}
