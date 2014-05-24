namespace SynotePPT2013Converter
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button_BrowsePPTXfile = new System.Windows.Forms.Button();
            this.textBox_PPTXPath = new System.Windows.Forms.TextBox();
            this.button_Go = new System.Windows.Forms.Button();
            this.textBox_Log = new System.Windows.Forms.TextBox();
            this.label_Log = new System.Windows.Forms.Label();
            this.backgroundWorker_Converter = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // button_BrowsePPTXfile
            // 
            this.button_BrowsePPTXfile.Location = new System.Drawing.Point(449, 28);
            this.button_BrowsePPTXfile.Name = "button_BrowsePPTXfile";
            this.button_BrowsePPTXfile.Size = new System.Drawing.Size(108, 23);
            this.button_BrowsePPTXfile.TabIndex = 0;
            this.button_BrowsePPTXfile.Text = "Browse PPTX File";
            this.button_BrowsePPTXfile.UseVisualStyleBackColor = true;
            this.button_BrowsePPTXfile.Click += new System.EventHandler(this.button_BrowsePPTXfile_Click);
            // 
            // textBox_PPTXPath
            // 
            this.textBox_PPTXPath.Location = new System.Drawing.Point(22, 28);
            this.textBox_PPTXPath.Name = "textBox_PPTXPath";
            this.textBox_PPTXPath.ReadOnly = true;
            this.textBox_PPTXPath.Size = new System.Drawing.Size(420, 20);
            this.textBox_PPTXPath.TabIndex = 1;
            this.textBox_PPTXPath.Text = "None";
            // 
            // button_Go
            // 
            this.button_Go.Location = new System.Drawing.Point(449, 67);
            this.button_Go.Name = "button_Go";
            this.button_Go.Size = new System.Drawing.Size(108, 47);
            this.button_Go.TabIndex = 4;
            this.button_Go.Text = "Go";
            this.button_Go.UseVisualStyleBackColor = true;
            this.button_Go.Click += new System.EventHandler(this.button_Go_Click);
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(22, 157);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Log.Size = new System.Drawing.Size(535, 247);
            this.textBox_Log.TabIndex = 5;
            this.textBox_Log.TextChanged += new System.EventHandler(this.textBox_Log_TextChanged);
            // 
            // label_Log
            // 
            this.label_Log.AutoSize = true;
            this.label_Log.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Log.Location = new System.Drawing.Point(19, 128);
            this.label_Log.Name = "label_Log";
            this.label_Log.Size = new System.Drawing.Size(31, 16);
            this.label_Log.TabIndex = 6;
            this.label_Log.Text = "Log";
            // 
            // backgroundWorker_Converter
            // 
            this.backgroundWorker_Converter.WorkerReportsProgress = true;
            this.backgroundWorker_Converter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Converter_DoWork);
            this.backgroundWorker_Converter.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_Converter_ProgressChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(449, 128);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(108, 23);
            this.progressBar.TabIndex = 7;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 416);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label_Log);
            this.Controls.Add(this.textBox_Log);
            this.Controls.Add(this.button_Go);
            this.Controls.Add(this.textBox_PPTXPath);
            this.Controls.Add(this.button_BrowsePPTXfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Synote PPTX 2013 Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_BrowsePPTXfile;
        private System.Windows.Forms.TextBox textBox_PPTXPath;
        private System.Windows.Forms.Button button_Go;
        private System.Windows.Forms.TextBox textBox_Log;
        private System.Windows.Forms.Label label_Log;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Converter;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

