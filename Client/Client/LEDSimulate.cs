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

        private List<PaintForm> paintList = new List<PaintForm>();
        /* PaintList
        Order: (Left * Right) + Right (left is Section, right is SubSectionInd)
        0	0	0
	        1	1
	        2	2
	        3	3
	        4	4
        1	5	0
	        6	1
	        7	2
	        8	3
	        9	4
        2	10	0
	        11	1
	        12	2
	        13	3
	        14	4

        section_ind * len(sub_section_ind) + i
        Can access exact paint ID in O(1)

            TODO -- Add a thread that calls this.Update() at 30fps
        */
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

            //Initialize timer to tick every 30 seconds to update UI
            timer_updatePaint.Interval = 33;

            //Load with dummy data to update later
            Pen dummy_pen = new Pen(Color.FromArgb(0, 0, 0, 0));
            foreach (Section s in sections)
            {
                for(int i = 0; i<s.subSections; i++)
                {
                    paintList.Add(new PaintForm(dummy_pen, 0, 0, 0, 0));
                }
            }

            //initialize for all sections
            var t = new Task(() =>
            {
                Section s = sections[0];
                var psi = new ProcessStartInfo();
                psi.FileName = pathPython;

                var script = pathScript;
                int startX = s.x, startY = s.y, width = s.width, height = s.height;

                int monitor = mon + 1;
                psi.Arguments = $"\"{script}\" \"{((s.useAudio) ? "1" : "-2")}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{0}\"  \"{s.subSections}\"";

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
                Console.WriteLine("Added process");
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            });

            t.Start();
            var t2 = new Task(() =>
            {
                Section s = sections[1];
                var psi = new ProcessStartInfo();
                psi.FileName = pathPython;

                var script = pathScript;
                int startX = s.x, startY = s.y, width = s.width, height = s.height;

                int monitor = mon + 1;
                psi.Arguments = $"\"{script}\" \"{((s.useAudio) ? "1" : "-2")}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{1}\"  \"{s.subSections}\"";

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
                Console.WriteLine("Added process");
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            });

            t2.Start();

            var t3 = new Task(() =>
            {
                Section s = sections[2];
                var psi = new ProcessStartInfo();
                psi.FileName = pathPython;

                var script = pathScript;
                int startX = s.x, startY = s.y, width = s.width, height = s.height;

                int monitor = mon + 1;
                psi.Arguments = $"\"{script}\" \"{((s.useAudio) ? "1" : "-2")}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{2}\"  \"{s.subSections}\"";

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
                Console.WriteLine("Added process");
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            });

            t3.Start();

            var t4 = new Task(() =>
            {
                Section s = sections[3];
                var psi = new ProcessStartInfo();
                psi.FileName = pathPython;

                var script = pathScript;
                int startX = s.x, startY = s.y, width = s.width, height = s.height;
                int monitor = mon + 1;

                Console.WriteLine($"\"{script}\" \"{((s.useAudio) ? "1" : "-2")}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{3}\"  \"{s.subSections}\"");

                psi.Arguments = $"\"{script}\" \"{((s.useAudio) ? "1" : "-2")}\" \"{monitor}\" \"{startX}\" \"{startY}\" \"{width}\" \"{height}\" \"{3}\"  \"{s.subSections}\"";

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
                Console.WriteLine("Added process");
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
            });

            t4.Start();
            Console.WriteLine("Finished adding and starting");
            timer_updatePaint.Start();
        }

        static void proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error: {0}", e.Data);
        }


        static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string[] argb_init = e.Data.Split(new string[] { "--" }, StringSplitOptions.None);
                int index = Int32.Parse(argb_init[1]);
                Section s = instance.sections[index];
                instance.ColorSection(s, argb_init[0].Split('|'), index);
            }
        }

        //Preserve the Alpha value, as ARGB only gets outputted once
        int a = 255;
        private void ColorSection(Section s, string[] argb_arr, int index)
        {
            float r_width = (float)instance.Width / instance.formInst.prevScreen.Bounds.Width;
            float r_height = (float)instance.Height / instance.formInst.prevScreen.Bounds.Height;
            int num_sections = argb_arr.Length;
            float pixelsPerSection = (float)((s.isVert) ? (float)((s.height / num_sections) * r_height) : (float)((s.width / num_sections) * r_width));
            float currX = (float)(s.x * r_width);
            float currY = (float)(s.y * r_height);
            if(index == 0)
            {
                Console.WriteLine(currX + " " + currY + " " + pixelsPerSection + " " + instance.Width + " " + instance.formInst.prevScreen.Bounds.Width);
            }

            //Changing values are based off s.isVert

            //Update
            
            for (int i = 0; i < num_sections; i++)
            {
                string [] argb = argb_arr[i].Split(' ');
                int r = 0, g = 0, b = 0;

                if (s.useAudio)
                {
                    //Only the first ARGB  output will contain sound, the rest is only RGB in the string
                    if(i == 0)
                    {
                        a = Int32.Parse(argb[0]);
                        r = Int32.Parse(argb[1]);
                        g = Int32.Parse(argb[2]);
                        b = Int32.Parse(argb[3]);
                    }
                    else
                    {
                        r = Int32.Parse(argb[0]);
                        g = Int32.Parse(argb[1]);
                        b = Int32.Parse(argb[2]);
                    }
                }
                else
                {
                    r = Int32.Parse(argb[0]);
                    g = Int32.Parse(argb[1]);
                    b = Int32.Parse(argb[2]);
                }

                Pen p = new Pen(Color.FromArgb(a, r, g, b));
                PaintForm tp = paintList[index * argb_arr.Length + i];
                if (s.isVert)
                {
                    tp.p = p;
                    tp.x = currX;
                    tp.y = currY;
                    tp.width = s.width * r_width;
                    tp.height = currY + pixelsPerSection;

                    currY += pixelsPerSection;
                }
                else
                {
                    tp.p = p;
                    tp.x = currX;
                    tp.y = currY;
                    tp.width = currX + pixelsPerSection;
                    tp.height = s.height * r_height;

                    currX += pixelsPerSection;
                }
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


        private void LEDSimulate_Paint(object sender, PaintEventArgs e)
        {
            
            //Test drawing on bitmap first, then draw the bitmap
            Bitmap tmp = new Bitmap(instance.Width, instance.Height);
            using (Graphics g = Graphics.FromImage(tmp))
            {
                // Create solid brush.
                foreach (PaintForm f in paintList)
                {
                    g.FillRectangle(f.p.Brush, f.x, f.y, f.width, f.height);
                }
                using (var gfx = e.Graphics)
                {
                    gfx.DrawImage(tmp, 0,0);
                }
            }
            
        }

        private void LEDSimulate_ResizeEnd(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void LEDSimulate_ResizeBegin(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void timer_updatePaint_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
