namespace Tools.BlobEditor.UserControls
{
    partial class CfsPreviewControl
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
            this.cfsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.btnPreviousFrame = new System.Windows.Forms.Button();
            this.btnNextFrame = new System.Windows.Forms.Button();
            this.lblFrameCount = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.btnPlayStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cfsSplitContainer)).BeginInit();
            this.cfsSplitContainer.Panel1.SuspendLayout();
            this.cfsSplitContainer.Panel2.SuspendLayout();
            this.cfsSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // cfsSplitContainer
            // 
            this.cfsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cfsSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.cfsSplitContainer.IsSplitterFixed = true;
            this.cfsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.cfsSplitContainer.Name = "cfsSplitContainer";
            this.cfsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // cfsSplitContainer.Panel1
            // 
            this.cfsSplitContainer.Panel1.Controls.Add(this.lblFileName);
            // 
            // cfsSplitContainer.Panel2
            // 
            this.cfsSplitContainer.Panel2.Controls.Add(this.lblFrameCount);
            this.cfsSplitContainer.Panel2.Controls.Add(this.btnPlayStop);
            this.cfsSplitContainer.Panel2.Controls.Add(this.btnNextFrame);
            this.cfsSplitContainer.Panel2.Controls.Add(this.btnPreviousFrame);
            this.cfsSplitContainer.Panel2.Controls.Add(this.pictureBoxPreview);
            this.cfsSplitContainer.Size = new System.Drawing.Size(294, 435);
            this.cfsSplitContainer.SplitterDistance = 43;
            this.cfsSplitContainer.TabIndex = 0;
            // 
            // btnPreviousFrame
            // 
            this.btnPreviousFrame.Location = new System.Drawing.Point(18, 278);
            this.btnPreviousFrame.Name = "btnPreviousFrame";
            this.btnPreviousFrame.Size = new System.Drawing.Size(32, 23);
            this.btnPreviousFrame.TabIndex = 1;
            this.btnPreviousFrame.Text = "<";
            this.btnPreviousFrame.UseVisualStyleBackColor = true;
            this.btnPreviousFrame.Click += new System.EventHandler(this.btnPreviousFrame_Click);
            // 
            // btnNextFrame
            // 
            this.btnNextFrame.Location = new System.Drawing.Point(117, 278);
            this.btnNextFrame.Name = "btnNextFrame";
            this.btnNextFrame.Size = new System.Drawing.Size(30, 23);
            this.btnNextFrame.TabIndex = 1;
            this.btnNextFrame.Text = ">";
            this.btnNextFrame.UseVisualStyleBackColor = true;
            this.btnNextFrame.Click += new System.EventHandler(this.btnNextFrame_Click);
            // 
            // lblFrameCount
            // 
            this.lblFrameCount.AutoSize = true;
            this.lblFrameCount.Location = new System.Drawing.Point(63, 283);
            this.lblFrameCount.Name = "lblFrameCount";
            this.lblFrameCount.Size = new System.Drawing.Size(35, 13);
            this.lblFrameCount.TabIndex = 1;
            this.lblFrameCount.Text = "label1";
            this.lblFrameCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(15, 17);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(35, 13);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "label1";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxPreview.Location = new System.Drawing.Point(18, 16);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxPreview.TabIndex = 0;
            this.pictureBoxPreview.TabStop = false;
            // 
            // btnPlayStop
            // 
            this.btnPlayStop.Location = new System.Drawing.Point(206, 278);
            this.btnPlayStop.Name = "btnPlayStop";
            this.btnPlayStop.Size = new System.Drawing.Size(68, 23);
            this.btnPlayStop.TabIndex = 2;
            this.btnPlayStop.Text = "Play/Stop";
            this.btnPlayStop.UseVisualStyleBackColor = true;
            this.btnPlayStop.Click += new System.EventHandler(this.btnPlayStop_Click);
            // 
            // CfsPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cfsSplitContainer);
            this.Name = "CfsPreviewControl";
            this.Size = new System.Drawing.Size(294, 435);
            this.cfsSplitContainer.Panel1.ResumeLayout(false);
            this.cfsSplitContainer.Panel1.PerformLayout();
            this.cfsSplitContainer.Panel2.ResumeLayout(false);
            this.cfsSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cfsSplitContainer)).EndInit();
            this.cfsSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer cfsSplitContainer;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Label lblFrameCount;
        private System.Windows.Forms.Button btnPreviousFrame;
        private System.Windows.Forms.Button btnNextFrame;
        private System.Windows.Forms.Button btnPlayStop;
    }
}
