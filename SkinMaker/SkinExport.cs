using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SkinMaker
{
    public class SkinExport
    {
        public static void ExportOsk(string skinName)
        {
            if (File.Exists(Path.Join(GetExportFolder(), $"{skinName}.osk")))
            {
                File.Delete(Path.Join(GetExportFolder(), $"{skinName}.osk"));
            }

            if (File.Exists(Path.Join(GetExportFolder(), $"{skinName}.zip")))
            {
                File.Delete(Path.Join(GetExportFolder(), $"{skinName}.zip"));
            }

            ZipFile.CreateFromDirectory(Path.Join(OptionsLoader.options.SkinsFolderPath, skinName), Path.Join(GetExportFolder(), $"{skinName}.zip"));
            File.Move(Path.Join(GetExportFolder(), $"{skinName}.zip"), Path.Join(GetExportFolder(), $"{skinName}.osk"));
        }

        public static void ExportToOsu(string skinName)
        {
            ExportOsk(skinName);
            Process.Start(Path.Join(OptionsLoader.options.OsuFolderPath, "osu!.exe"), Path.Join(GetExportFolder(), $"{skinName}.osk")); 
        }

        private static string GetExportFolder()
        {
            if (Directory.Exists(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Skin Maker", "Export")))
            {
                return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Skin Maker", "Export");
            }
            else
            {
                Directory.CreateDirectory(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Skin Maker", "Export"));
                return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Skin Maker", "Export");
            }
        }
    }
}
