namespace ImageProcessingWinFormCoreCSharp
{
    partial class FormHistgramOxyPlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistgramOxyPlot));
            this.chart = new OxyPlot.WindowsForms.PlotView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveCsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart.ContextMenuStrip = this.contextMenu;
            this.chart.Location = new System.Drawing.Point(12, 12);
            this.chart.Name = "chart";
            this.chart.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.chart.Size = new System.Drawing.Size(776, 426);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart";
            this.chart.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.chart.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.chart.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCsvToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(128, 26);
            // 
            // saveCsvToolStripMenuItem
            // 
            this.saveCsvToolStripMenuItem.Name = "saveCsvToolStripMenuItem";
            this.saveCsvToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.saveCsvToolStripMenuItem.Text = "Save Csv...";
            this.saveCsvToolStripMenuItem.Click += new System.EventHandler(this.OnClickMenu);
            // 
            // FormHistgramOxyPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHistgramOxyPlot";
            this.Text = "Histgram";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosingFormHistgramOxyPlot);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView chart;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveCsvToolStripMenuItem;
    }
}