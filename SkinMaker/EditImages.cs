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
        public static int[] GetImageDim(string path)
        {
            using Bitmap img = new(path);
            int[] dim = { img.Width, img.Height };
            img.Dispose();
            return dim;
        }
    }
}
