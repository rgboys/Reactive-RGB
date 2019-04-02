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
            foreach(Section s in sections)
            {
                var t = new Task(() =>
                {
                    var psi = new ProcessStartInfo();
                    psi.FileName = pathPython;

                    var script = pathScript;
                    int startX = 0, startY = 0, width = 0, height = 0;

                    if (!s.isVert)
                    {
                        startX = s.x;
                        startY = 0;
                        width = s.sep;
                        height = s.y;
                    }
                    else
                    {
                        startX = s.x;
                        startY = 0;
                        width = s.sep;
                        height = s.y;
                    }
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

                    procs.Add(p);

                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
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
