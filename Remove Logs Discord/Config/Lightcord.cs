using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remove_Logs_Discord.Config
{
    public class Lightcord
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lightcord\\Local Storage\\leveldb\\";

        public void DELETE()
        {
            foreach (Process process in Process.GetProcessesByName("Lightcord"))
            {
                process.Kill();
            }

            Thread.Sleep(100);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                MessageBox.Show("Logs cleaned for Lightcord");
            }
            else
            {
                MessageBox.Show("Sorry, but the logs for Lightcord is already cleaned !");
            }
        }
    }
}
