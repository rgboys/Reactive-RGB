using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LEDSimulate : Form
    {
        public List<Section> sections;
        public static LEDSimulate instance;
        
        public LEDSimulate(List<Section> sections, int mon)
        {
            InitializeComponent();
            instance = this;
            this.sections = sections;
            var t = new Task(() =>
            {
                Console.WriteLine("here");
                var psi = new ProcessStartInfo();
                psi.FileName = @"C:\Users\lichc\AppData\Local\Programs\Python\Python37\python.exe";

                var script = @"C:\Users\lichc\Desktop\RGBoys\Reactive-RGB\Python\ClientCapture\clientcapture.py";
                int startX = 0;
                int startY = 0;
                int width = 200;
                int height = 200;
                int monitor = mon + 1;
                psi.Arguments = $"\"{script}\" \"{"1"}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\"";

                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                Process p = new Process();
                p.OutputDataReceived += new DataReceivedEventHandler(proc_OutputDataReceived);
                p.ErrorDataReceived += new DataReceivedEventHandler(proc_ErrorDataReceived);
                p.StartInfo = psi;
                Console.WriteLine("here");
                p.Start();
                Console.WriteLine("here");

                p.BeginOutputReadLine();
                Console.WriteLine("here2");
                Console.WriteLine("hereF");
            });

            t.Start();
            t.Wait();
        }

        static void proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error: {0}", e.Data);
        }


        static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            string[] rgb = e.Data.Split(' ');
            int r = Int32.Parse(rgb[0]);
            int g = Int32.Parse(rgb[1]);
            int b = Int32.Parse(rgb[2]);
            //Update
            instance.BackColor = Color.FromArgb(r, g, b);
        }



    }
}
