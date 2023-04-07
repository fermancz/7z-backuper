using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace _7z_backuper
{
    internal class Utils
    {
        public static bool CheckDirExist(string path)
        {
            bool result = Directory.Exists(path);
            return result;
        }

        public static bool CheckDirsExist(string[] paths)
        {
            foreach (string? directory in paths)
            {
                if (CheckDirExist(directory) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static string PrepareSources(string[] sources)
        {
            string result = String.Join(" ", sources);
            return result;
        }

        public static string Get7zExe()
        {
            const string z7 = "7z.exe";
            const string za7 = "7za.exe";

            if (CheckProgramExistsOnPath(z7))
            {
                return z7;
            } 
            else if (CheckProgramExistsOnPath(za7))
            {
                return za7;
            }
            return null;
        }

        public static bool CheckProgramExistsOnPath(string fileName)
        {
            return GetFullPath(fileName) != null;
        }

        public static string GetFullPath(string fileName)
        {
            if (File.Exists(fileName))
            {
                return Path.GetFullPath(fileName);
            }

            var paths = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in paths.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }
            return null;
        }
    }
}
