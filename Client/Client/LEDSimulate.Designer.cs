namespace Client
{
    partial class LEDSimulate
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
            this.timer_updatePaint = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_updatePaint
            // 
            this.timer_updatePaint.Tick += new System.EventHandler(this.timer_updatePaint_Tick);
            // 
            // LEDSimulate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 620);
            this.Name = "LEDSimulate";
            this.Text = "LEDSimulate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LEDSimulate_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.LEDSimulate_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.LEDSimulate_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LEDSimulate_Paint);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer_updatePaint;
    }
}