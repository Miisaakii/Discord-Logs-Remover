﻿using System;
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
    public class DiscordPTB
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discordptb\\Local Storage\\leveldb\\";

        public void DELETE()
        {
            foreach (Process process in Process.GetProcessesByName("DiscordPTB"))
            {
                process.Kill();
            }

            Thread.Sleep(100);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                MessageBox.Show("Logs cleaned for DiscordPTB");
            }
            else
            {
                MessageBox.Show("Sorry, but the logs for DiscordPTB is already cleaned !");
            }
        }
    }
}
