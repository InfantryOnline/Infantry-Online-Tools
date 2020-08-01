namespace Tools.BlobEditor.UserControls
{
    partial class WavPreviewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlayStopAudio = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlayStopAudio
            // 
            this.btnPlayStopAudio.Location = new System.Drawing.Point(106, 216);
            this.btnPlayStopAudio.Name = "btnPlayStopAudio";
            this.btnPlayStopAudio.Size = new System.Drawing.Size(75, 23);
            this.btnPlayStopAudio.TabIndex = 0;
            this.btnPlayStopAudio.Text = "Play / Stop";
            this.btnPlayStopAudio.UseVisualStyleBackColor = true;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 10);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(35, 13);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "label1";
            // 
            // WavPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnPlayStopAudio);
            this.Name = "WavPreviewControl";
            this.Size = new System.Drawing.Size(294, 435);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlayStopAudio;
        private System.Windows.Forms.Label lblFileName;
    }
}
