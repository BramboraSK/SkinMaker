using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using IniParser.Parser;

namespace SkinMaker
{
    public class SkinIniEditor
    {
        IniDataParser parser;
        IniData data;
        string skinName;
        public void SkinIniChanged(string skinName, string section, string key, string value)
        {
            this.skinName = skinName;
            parser = new IniDataParser();

            IniParser.Model.Configuration.IniParserConfiguration config = parser.Configuration;

            config.CommentString = "/";
            config.KeyValueAssigmentChar = ':';
            config.AssigmentSpacer = "";
            config.AllowDuplicateSections = true;
            config.AllowDuplicateKeys = true;

            data = parser.Parse(File.ReadAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini"));

            ChangeIniValue(section, key, value);
        }

        private void ChangeIniValue(string section, string key, string value)
        {
            data[section][key] = value;
            SaveSkinIni();
        }


        private void SaveSkinIni()
        {
            File.WriteAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini", data.ToString());
        }

        public string GetIniData(string skinName, string section, string key, bool returnNull)
        {
            this.skinName = skinName;
            parser = new IniDataParser();
            string value;

            IniParser.Model.Configuration.IniParserConfiguration config = parser.Configuration;

            config.CommentString = "/";
            config.KeyValueAssigmentChar = ':';
            config.AssigmentSpacer = "";
            config.AllowDuplicateSections = true;
            config.AllowDuplicateKeys = true;

            try
            {
                data = parser.Parse(File.ReadAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini"));
            }
            catch (IOException)
            {
                System.Threading.Thread.Sleep(1000);
                data = parser.Parse(File.ReadAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skinName}\skin.ini"));
            }

            if (data[section][key] != null)
            {
                return data[section][key];
            }
            else
            {
                if (returnNull)
                {
                    return null;
                }
                else
                {
                    IniData defaultData = parser.Parse(File.ReadAllText(@"Templates\skin.ini"));;
                    data[section][key] = defaultData[section][key];

                    SaveSkinIni();

                    return data[section][key];
                }
            }
        }
        
    }
}
