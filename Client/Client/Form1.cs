using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

            Directory.CreateDirectory(Path.GetTempPath() + "\\RGBoys\\Client");
            screens = Screen.AllScreens;

            Array.Sort(screens, delegate (Screen x, Screen y) { return x.DeviceName.CompareTo(y.DeviceName); });

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
            memoryGraphics.CopyFromScreen(prevScreen.Bounds.Left, prevScreen.Bounds.Top, 0,0, prevScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            Console.WriteLine(Path.GetTempPath());
            string path = @"" + Path.GetTempPath() + "\\RGBoys\\Client\\temp.png";
            
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    memoryImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            

            Process photoViewer = new Process();
            photoViewer.StartInfo.FileName = path;
            photoViewer.StartInfo.Arguments = path;
            photoViewer.Start();
        }

        private void combo_monitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            prevScreen = screens[combo_monitor.SelectedIndex];
        }

        //Executes python scripts
        private void button_start_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\lichc\AppData\Local\Programs\Python\Python37\python.exe";

            var script = @"C:\Users\lichc\Desktop\ReactiveRGB\Reactive-RGB\Python\ClientCapture\clientcapture.py";
            int startX = 0;
            int startY = 0;
            int width = 200;
            int height = 200;
            int monitor = combo_monitor.SelectedIndex;
            psi.Arguments = $"\"{script}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{monitor}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            Console.WriteLine(errors);
            Console.WriteLine(results);
        }
    }
}
