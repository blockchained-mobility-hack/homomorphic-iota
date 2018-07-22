namespace Homomorphic_Iota
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFound = new System.Windows.Forms.Label();
            this.search = new System.Windows.Forms.Button();
            this.MapZoomLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MapCenterLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MyWebBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MyWebBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(1274, 928);
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFound);
            this.panel1.Controls.Add(this.search);
            this.panel1.Controls.Add(this.MapZoomLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.MapCenterLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1274, 50);
            this.panel1.TabIndex = 0;
            // 
            // lblFound
            // 
            this.lblFound.Location = new System.Drawing.Point(196, 5);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(232, 36);
            this.lblFound.TabIndex = 6;
            this.lblFound.Text = "found";
            this.lblFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(12, 5);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(124, 36);
            this.search.TabIndex = 5;
            this.search.Text = "Search";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // MapZoomLabel
            // 
            this.MapZoomLabel.AutoSize = true;
            this.MapZoomLabel.Location = new System.Drawing.Point(1057, 19);
            this.MapZoomLabel.Name = "MapZoomLabel";
            this.MapZoomLabel.Size = new System.Drawing.Size(51, 20);
            this.MapZoomLabel.TabIndex = 4;
            this.MapZoomLabel.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(917, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Map Zoom:";
            // 
            // MapCenterLabel
            // 
            this.MapCenterLabel.AutoSize = true;
            this.MapCenterLabel.Location = new System.Drawing.Point(744, 19);
            this.MapCenterLabel.Name = "MapCenterLabel";
            this.MapCenterLabel.Size = new System.Drawing.Size(51, 20);
            this.MapCenterLabel.TabIndex = 3;
            this.MapCenterLabel.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(617, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Map Center:";
            // 
            // MyWebBrowser
            // 
            this.MyWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.MyWebBrowser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MyWebBrowser.MinimumSize = new System.Drawing.Size(22, 25);
            this.MyWebBrowser.Name = "MyWebBrowser";
            this.MyWebBrowser.Size = new System.Drawing.Size(1274, 873);
            this.MyWebBrowser.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 928);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Bing Maps V8 WinForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser MyWebBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MapZoomLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MapCenterLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Label lblFound;
    }
}

