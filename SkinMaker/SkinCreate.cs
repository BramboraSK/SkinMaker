using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static SkinMaker.NewSkinContent;
using IniParser;
using IniParser.Model;
using IniParser.Parser;

namespace SkinMaker
{
    public class SkinCreate
    {
        public static void CreateSkin(Skin skin)
        {
            Directory.CreateDirectory($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}");
            CopyIniTemplate(skin);
        }
        
        private static void CopyIniTemplate(Skin skin)
        {
            var parser = new IniDataParser();

            IniParser.Model.Configuration.IniParserConfiguration config = parser.Configuration;

            config.CommentString = "/";
            config.KeyValueAssigmentChar = ':';
            config.AssigmentSpacer = "";

            IniData data = parser.Parse(File.ReadAllText(@"Templates\skin.ini"));

            data["General"]["Name"] = skin.name;
            data["General"]["Author"] = skin.author;

            File.WriteAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}\skin.ini", data.ToString());
        }


    }
}
