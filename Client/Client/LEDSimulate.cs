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

        private List<Process> procs;

        //Requires this instance just in case the user closes from the form instead of hitting the 'stop' button on the main form
        private Form1 formInst;

        public LEDSimulate(string pathScript, string pathPython, Form1 formInst, List<Section> sections, int mon, int numThreads)
        {
            InitializeComponent();
            instance = this;
            this.TransparencyKey = (BackColor);
            this.sections = sections;
            this.procs = new List<Process>();

            this.formInst = formInst;

            //initialize for all sections
            for(int i = 0; i<numThreads; i++)
            {
                var t = new Task(() =>
                {
                    for(int j = 0+(i*numThreads); j<Math.Ceiling((double)sections.Count/numThreads); j++)
                    {
                        Section s = sections[j];
                        var psi = new ProcessStartInfo();
                        psi.FileName = pathPython;

                        var script = pathScript;
                        int startX = s.x, startY = s.y, width = s.width, height = s.height;

                        int monitor = mon + 1;
                        psi.Arguments = $"\"{script}\" \"{"1"}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{j}\"";

                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        psi.RedirectStandardOutput = true;
                        psi.RedirectStandardError = true;

                        Process p = new Process();
                        p.OutputDataReceived += new DataReceivedEventHandler(proc_OutputDataReceived);
                        p.ErrorDataReceived += new DataReceivedEventHandler(proc_ErrorDataReceived);
                        p.StartInfo = psi;
                        p.Start();

                        procs.Add(p);

                        p.BeginOutputReadLine();
                        p.BeginErrorReadLine();
                    }
                });
                t.Start();
            }
        }

        static void proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error: {0}", e.Data);
        }


        static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
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

        //Kill processes
        private void LEDSimulate_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(Process p in procs)
            {
                p.Kill();
            }
            //Flag exsits to determine whether or not the close button was pressed within Form1, or hard closed on this form
            if (!formInst.button_flag)
            {
                formInst.toggleButton();
            }
            formInst.button_flag = false;
        }
    }
}
