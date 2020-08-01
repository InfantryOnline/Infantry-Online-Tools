namespace Tools.InfantryStudio.UserControls
{
    partial class MapUserControl
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
            this.vScrollbar = new System.Windows.Forms.VScrollBar();
            this.hScrollbar = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // vScrollbar
            // 
            this.vScrollbar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollbar.Location = new System.Drawing.Point(493, 0);
            this.vScrollbar.Name = "vScrollbar";
            this.vScrollbar.Size = new System.Drawing.Size(17, 376);
            this.vScrollbar.TabIndex = 0;
            // 
            // hScrollbar
            // 
            this.hScrollbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollbar.Location = new System.Drawing.Point(0, 359);
            this.hScrollbar.Name = "hScrollbar";
            this.hScrollbar.Size = new System.Drawing.Size(493, 17);
            this.hScrollbar.TabIndex = 1;
            // 
            // MapUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollbar);
            this.Controls.Add(this.vScrollbar);
            this.Name = "MapUserControl";
            this.Size = new System.Drawing.Size(510, 376);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollbar;
        private System.Windows.Forms.HScrollBar hScrollbar;
    }
}
