using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace SkinMaker
{
    public class InitCheck
    {
        public void Init()
        {
            if (!File.Exists("options.json"))
            {
                File.Create("options.json");
                CreateConfig();
            }

        }
        
        private void CreateConfig()
        {
            
        }
    }
}
