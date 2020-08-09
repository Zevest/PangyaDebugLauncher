namespace PangyaDebugLauncher
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.optionButton = new System.Windows.Forms.Button();
            this.loadingBar = new SmoothProgressBar.SmoothProgressBar();
            this.stepLoadingBar = new SmoothProgressBar.SmoothProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::PangyaDebugLauncher.Properties.Resources.pangyaDebugLogo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 165);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(12, 445);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(144, 48);
            this.updateButton.TabIndex = 1;
            this.updateButton.TabStop = false;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(162, 445);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(144, 48);
            this.playButton.TabIndex = 2;
            this.playButton.TabStop = false;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(63, 499);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(93, 33);
            this.downloadButton.TabIndex = 4;
            this.downloadButton.TabStop = false;
            this.downloadButton.Text = "download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // optionButton
            // 
            this.optionButton.Location = new System.Drawing.Point(162, 499);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(93, 33);
            this.optionButton.TabIndex = 5;
            this.optionButton.TabStop = false;
            this.optionButton.Text = "option";
            this.optionButton.UseVisualStyleBackColor = true;
            this.optionButton.Click += new System.EventHandler(this.OptionButton_Click);
            // 
            // loadingBar
            // 
            this.loadingBar.Location = new System.Drawing.Point(350, 445);
            this.loadingBar.Maximum = 100;
            this.loadingBar.Minimum = 0;
            this.loadingBar.Name = "loadingBar";
            this.loadingBar.ProgressBarColor = System.Drawing.Color.Purple;
            this.loadingBar.Size = new System.Drawing.Size(567, 48);
            this.loadingBar.TabIndex = 7;
            this.loadingBar.Value = 0;
            // 
            // stepLoadingBar
            // 
            this.stepLoadingBar.Location = new System.Drawing.Point(350, 499);
            this.stepLoadingBar.Maximum = 100;
            this.stepLoadingBar.Minimum = 0;
            this.stepLoadingBar.Name = "stepLoadingBar";
            this.stepLoadingBar.ProgressBarColor = System.Drawing.Color.Blue;
            this.stepLoadingBar.Size = new System.Drawing.Size(567, 17);
            this.stepLoadingBar.TabIndex = 8;
            this.stepLoadingBar.Value = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PangyaDebugLauncher.Properties.Resources.Pangya_full;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(928, 552);
            this.Controls.Add(this.stepLoadingBar);
            this.Controls.Add(this.loadingBar);
            this.Controls.Add(this.optionButton);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(944, 591);
            this.MinimumSize = new System.Drawing.Size(944, 591);
            this.Name = "MainForm";
            this.Text = "Pangya Debug Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Button optionButton;
        private SmoothProgressBar.SmoothProgressBar loadingBar;
        private SmoothProgressBar.SmoothProgressBar stepLoadingBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}

