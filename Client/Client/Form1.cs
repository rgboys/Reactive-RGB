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

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            Directory.CreateDirectory(Path.GetTempPath() + "\\RGBoys\\Client");
            screens = Screen.AllScreens;

            Array.Sort(screens, delegate (Screen x, Screen y) { return x.DeviceName.CompareTo(y.DeviceName); });

            int i = 0;
            foreach (var screen in screens)
            {
                string text = string.Format("{0}: {1}", ++i, screen.DeviceName);
                combo_monitor.Items.Add(text);
            }
            prevScreen = screens[0];
            combo_monitor.SelectedIndex = 0;
        }

        //Executes python script to grab a preview of the monitor that will be worked with
        private void link_previewMonitor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\lichc\AppData\Local\Programs\Python\Python37\python.exe";

            var script = @"C:\Users\lichc\Desktop\RGBoys\Reactive - RGB\Python\ClientCapture\clientcapture.py";
            int monitorIndex = combo_monitor.SelectedIndex+1;

            psi.Arguments = $"\"{script}\" \"{"0"}\" \"{monitorIndex}\"";

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

        private void combo_monitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            prevScreen = screens[combo_monitor.SelectedIndex];
        }

        bool button_startIndicator = false;

        //Executes python scripts
        private void button_start_Click(object sender, EventArgs e)
        {
            button_startIndicator = !button_startIndicator;
            if(button_startIndicator)
            {
                int numThreads = Int32.Parse(numeric_threads.Value.ToString());

                //Width per section
                int bottom_widthPerSection = (prevScreen.Bounds.Width / Int32.Parse(numeric_horizontalLEDs.Value.ToString()));
                int left_widthPerSection = (prevScreen.Bounds.Height / Int32.Parse(numeric_verticalLEDs.Value.ToString()));

                //Number of sections
                int bottom_numSections = prevScreen.Bounds.Width / bottom_widthPerSection;
                int left_numSections = prevScreen.Bounds.Height / left_widthPerSection;

                int inc = 0;

                List<Section> sections = new List<Section>();
                for (int i = 0; i < bottom_numSections; i++)
                {
                    sections.Add(new Section(inc, 100, false));
                    inc += bottom_widthPerSection;
                }
                inc = 0;
                for (int i = 0; i < left_numSections; i++)
                {
                    sections.Add(new Section(100, inc, true));
                    inc += left_widthPerSection;
                }
                LEDSimulate dial = new LEDSimulate(sections, combo_monitor.SelectedIndex);
                dial.Show();
            }
            else
            {
                //Kill all the threads
            }
        }
    }
}
