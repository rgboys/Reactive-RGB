using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private Screen prevScreen;
        private Screen[] screens;
        public Form1()
        {
            InitializeComponent();
            screens = Screen.AllScreens;
            int i = 0;
            foreach (var screen in screens)
            {
                string text = string.Format("{0}: {1}{2}", ++i, screen.DeviceName, (screen.Primary) ? " - Primary" : "");
                combo_monitor.Items.Add(text);
            }
            prevScreen = screens[0];
            combo_monitor.SelectedIndex = 0;
        }

        private void link_previewMonitor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Bitmap memoryImage;
            Graphics myGraphics = this.CreateGraphics();
            Size s = prevScreen.Bounds.Size;
            memoryImage = new Bitmap(prevScreen.Bounds.Width, prevScreen.Bounds.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(prevScreen.Bounds.Left, prevScreen.Bounds.Top, prevScreen.Bounds.Right, prevScreen.Bounds.Bottom, prevScreen.Bounds.Size);
            
        }

        private void combo_monitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            prevScreen = screens[combo_monitor.SelectedIndex];
        }
    }
}
