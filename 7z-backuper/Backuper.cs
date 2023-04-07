using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7z_backuper
{
    internal class Backuper
    {
        private const string DIFF = "diff";
        private const string FULL = "full";

        public static void Backup(string[] sources, string target, string name, string z7exe, int diffCountOption)
        {
            string exe = z7exe;
            if (exe == null)
            {
                exe = Utils.Get7zExe();
            }
            if (exe == null)
            {
                throw new Exception("No 7z or 7za program in path.");
            }

            if (Utils.CheckDirExist(target) == false)
            {
                throw new Exception("Target directory doesn't exist");
            }

            if (Utils.CheckDirsExist(sources) == false)
            {
                throw new Exception("Source directory doesn't exist");
            }
            
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            string fullBackup = FullBackup(target, name, diffCountOption);
            if (fullBackup == null)
            {
                string args = "u " + target + Path.PathSeparator + name + "_" + timestamp + "_" + FULL + ".7z " + Utils.PrepareSources(sources) + " -up0q0r2x2y2z1w2";
                Exec(exe, args);
            }
            else
            {

            }

            Console.WriteLine("DONE");
        }

        private static string FullBackup(string target, string name, int diffCount)
        {
            var files = Directory.EnumerateFiles(target).OrderByDescending(filename => filename);
            int count = 0;
            foreach (var file in files)
            {
                if (file.Contains(name) && file.Contains(DIFF))
                {
                    count++;
                }
                else
                if (file.Contains(name) && file.Contains(FULL))
                {
                    return file;
                }
                else 
                if (count >= diffCount)
                {
                    return null;
                }

            }
            return null;
        }

        //see https://www.dotnetperls.com/process
        private static void Exec(string exe, string args) 
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.CreateNoWindow = false;
            psi.UseShellExecute = false;
            psi.FileName = exe;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Arguments = args;

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
            }

        }
    }
}
