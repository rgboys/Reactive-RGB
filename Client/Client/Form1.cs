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
        public static bool init = false;

        private static string pathPython = @"C:\Users\lichc\AppData\Local\Programs\Python\Python36\python.exe";
        private static string pathScript = @"C:\Users\lichc\Desktop\ReactiveRGB\Reactive-RGB\Python\ClientCapture\clientcapture.py";

        public Screen prevScreen;
        private Screen[] screens;

        private LEDSimulate dial;

        private Form1 formInst;

        //Flag to determine toggling "start" and "stop" text on button, and logic
        private bool button_startIndicator = false;
        //Flag to communicate to LEDSimulate that the 'stop' button was pressed on this form, rather than forced closed on the other form
        public bool button_flag = false;

        public Form1()
        {
            InitializeComponent();
            init = true;

            this.formInst = this;

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
            psi.FileName = pathPython;

            var script = pathScript;
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

        //Computes sections
        private void button_start_Click(object sender, EventArgs e)
        {
            if(toggleButton())
            {
                int numThreads = Int32.Parse(numeric_threads.Value.ToString());

                
                List<Section> sections = new List<Section>();
                sections.Add(new Section(0, 0, prevScreen.Bounds.Width, 100, checkbox_UseAudio.Checked, false, Int32.Parse(numeric_horizontalLEDs.Value.ToString()))); //Top
                sections.Add(new Section(0, prevScreen.Bounds.Height - 100, prevScreen.Bounds.Width, 100, false, false, Int32.Parse(numeric_horizontalLEDs.Value.ToString()))); //Bottom
                sections.Add(new Section(0, 0, 100, prevScreen.Bounds.Height, false, true, Int32.Parse(numeric_verticalLEDs.Value.ToString()))); //Left
                sections.Add(new Section(prevScreen.Bounds.Width - 100, 0, 100, prevScreen.Bounds.Height, false, true, Int32.Parse(numeric_verticalLEDs.Value.ToString())));//  Right
                dial = new LEDSimulate(pathScript, pathPython, formInst, sections, combo_monitor.SelectedIndex, numThreads);
                dial.Show();
            }
            else
            {
                button_flag = true;
                //Kill all the threads by closing
                dial.Close();
            }
        }

        public bool toggleButton()
        {
            button_startIndicator = !button_startIndicator;
            button_start.Text = (button_startIndicator) ? "Stop" : "Start";
            return button_startIndicator;
        }
    }
}
