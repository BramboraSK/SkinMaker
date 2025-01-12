﻿using System;
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
        public static bool CreateSkin(Skin skin, string template)
        {
            if (!CheckSkinExists(skin))
            {
                if (template == "(Empty)")
                {
                    Directory.CreateDirectory($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}");
                    CreateIniFile(skin, template);
                }
                else
                {
                    Directory.CreateDirectory($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}");
                    CopyFromTemplate($@"{OptionsLoader.options.SkinsFolderPath}\{template}", $@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}");
                    CreateIniFile(skin, template);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private static bool CheckSkinExists(Skin skin)
        {
            return Directory.Exists($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}");
        }

        private static void CreateIniFile(Skin skin, string template)
        {
            var parser = new IniDataParser();

            IniParser.Model.Configuration.IniParserConfiguration config = parser.Configuration;

            config.CommentString = "/";
            config.KeyValueAssigmentChar = ':';
            config.AssigmentSpacer = "";
            config.AllowDuplicateSections = true;
            config.AllowDuplicateKeys = true;

            if (template == "(Empty)" || !File.Exists($@"{OptionsLoader.options.SkinsFolderPath}\{template}\skin.ini"))
            {
                IniData data = parser.Parse(File.ReadAllText(@"Templates\skin.ini"));

                data["General"]["Name"] = skin.name;
                data["General"]["Author"] = skin.author;

                File.WriteAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}\skin.ini", data.ToString());
            }
            else
            {
                IniData data = parser.Parse(File.ReadAllText($@"{OptionsLoader.options.SkinsFolderPath}\{template}\skin.ini"));

                data["General"]["Name"] = skin.name;
                data["General"]["Author"] = skin.author;

                File.WriteAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}\skin.ini", "// Created with Skin Maker: https://github.com/BramboraSK/SkinMaker \n\n");
                File.AppendAllText($@"{OptionsLoader.options.SkinsFolderPath}\{skin.name}\skin.ini", data.ToString());
            }
        }

        private static void CopyFromTemplate(string templatePath, string newSkinPath)
        {
            foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(templatePath, newSkinPath));
            }

            foreach (string newPath in Directory.GetFiles(templatePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(templatePath, newSkinPath), true);
            }
        }
    }
}
