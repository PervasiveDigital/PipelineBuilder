namespace VSPipelineBuilder
{
    partial class DisplayError
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
            this.t_errorText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // t_errorText
            // 
            this.t_errorText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t_errorText.Location = new System.Drawing.Point(0, 0);
            this.t_errorText.Multiline = true;
            this.t_errorText.Name = "t_errorText";
            this.t_errorText.Size = new System.Drawing.Size(624, 444);
            this.t_errorText.TabIndex = 1;
            // 
            // DisplayError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 444);
            this.Controls.Add(this.t_errorText);
            this.Name = "DisplayError";
            this.Text = "DisplayError";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t_errorText;

    }
}