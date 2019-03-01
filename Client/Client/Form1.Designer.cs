namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_monitor = new System.Windows.Forms.ComboBox();
            this.link_previewMonitor = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numeric_verticalLEDs = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label_sizeOfPartition = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numeric_horizontalLEDs = new System.Windows.Forms.NumericUpDown();
            this.button_start = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.numeric_threads = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_verticalLEDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_horizontalLEDs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threads)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Monitor";
            // 
            // combo_monitor
            // 
            this.combo_monitor.FormattingEnabled = true;
            this.combo_monitor.Location = new System.Drawing.Point(103, 12);
            this.combo_monitor.Name = "combo_monitor";
            this.combo_monitor.Size = new System.Drawing.Size(192, 21);
            this.combo_monitor.TabIndex = 1;
            this.combo_monitor.SelectedIndexChanged += new System.EventHandler(this.combo_monitor_SelectedIndexChanged);
            // 
            // link_previewMonitor
            // 
            this.link_previewMonitor.AutoSize = true;
            this.link_previewMonitor.Location = new System.Drawing.Point(212, 36);
            this.link_previewMonitor.Name = "link_previewMonitor";
            this.link_previewMonitor.Size = new System.Drawing.Size(83, 13);
            this.link_previewMonitor.TabIndex = 2;
            this.link_previewMonitor.TabStop = true;
            this.link_previewMonitor.Text = "Preview Monitor";
            this.link_previewMonitor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_previewMonitor_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "How many LEDs";
            // 
            // numeric_verticalLEDs
            // 
            this.numeric_verticalLEDs.Location = new System.Drawing.Point(104, 112);
            this.numeric_verticalLEDs.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_verticalLEDs.Name = "numeric_verticalLEDs";
            this.numeric_verticalLEDs.Size = new System.Drawing.Size(74, 20);
            this.numeric_verticalLEDs.TabIndex = 4;
            this.numeric_verticalLEDs.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Vertical";
            // 
            // label_sizeOfPartition
            // 
            this.label_sizeOfPartition.AutoSize = true;
            this.label_sizeOfPartition.Location = new System.Drawing.Point(123, 114);
            this.label_sizeOfPartition.Name = "label_sizeOfPartition";
            this.label_sizeOfPartition.Size = new System.Drawing.Size(0, 13);
            this.label_sizeOfPartition.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Horizontal";
            // 
            // numeric_horizontalLEDs
            // 
            this.numeric_horizontalLEDs.Location = new System.Drawing.Point(104, 138);
            this.numeric_horizontalLEDs.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_horizontalLEDs.Name = "numeric_horizontalLEDs";
            this.numeric_horizontalLEDs.Size = new System.Drawing.Size(74, 20);
            this.numeric_horizontalLEDs.TabIndex = 5;
            this.numeric_horizontalLEDs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(103, 190);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 6;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Max Threads";
            // 
            // numeric_threads
            // 
            this.numeric_threads.Location = new System.Drawing.Point(104, 58);
            this.numeric_threads.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numeric_threads.Name = "numeric_threads";
            this.numeric_threads.Size = new System.Drawing.Size(74, 20);
            this.numeric_threads.TabIndex = 3;
            this.numeric_threads.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 232);
            this.Controls.Add(this.numeric_threads);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.numeric_horizontalLEDs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_sizeOfPartition);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numeric_verticalLEDs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.link_previewMonitor);
            this.Controls.Add(this.combo_monitor);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "RGBoys";
            ((System.ComponentModel.ISupportInitialize)(this.numeric_verticalLEDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_horizontalLEDs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_monitor;
        private System.Windows.Forms.LinkLabel link_previewMonitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numeric_verticalLEDs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_sizeOfPartition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numeric_horizontalLEDs;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numeric_threads;
    }
}

