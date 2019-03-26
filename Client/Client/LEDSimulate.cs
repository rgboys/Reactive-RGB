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
        
        public LEDSimulate(string pathScript, string pathPython, List<Section> sections, int mon)
        {
            InitializeComponent();
            instance = this;
            this.TransparencyKey = (BackColor);
            this.sections = sections;
            var t = new Task(() =>
            {
                Console.WriteLine("here");
                var psi = new ProcessStartInfo();
                psi.FileName = pathPython;

                var script = pathScript;
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
                p.Start();

                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
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
            if(!string.IsNullOrEmpty(e.Data))
            {
                string[] argb = e.Data.Split(' ');
                int a = Int32.Parse(argb[0]);
                int r = Int32.Parse(argb[1]);
                int g = Int32.Parse(argb[2]);
                int b = Int32.Parse(argb[3]);
                //Update
                instance.BackColor = Color.FromArgb(r, g, b);

                //Console.WriteLine(string.Format("ARGB: argb({0}, {1}, {2}, {3})", a, r, g, b));
            }
        }



    }
}
