namespace PipelineBuilderPkg
{
    partial class PipelineConfiguration
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.b_ok = new System.Windows.Forms.Button();
            this.b_cancel = new System.Windows.Forms.Button();
            this.c_projects = new System.Windows.Forms.ComboBox();
            this.t_sourceOutput = new System.Windows.Forms.TextBox();
            this.b_projectOutput = new System.Windows.Forms.Button();
            this.b_buildOutput = new System.Windows.Forms.Button();
            this.t_binaryOutput = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(11, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(120, 13);
            label1.TabIndex = 3;
            label1.Text = "Contract Source Project";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 66);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 13);
            label2.TabIndex = 6;
            label2.Text = "Project Location";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(11, 113);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(74, 13);
            label3.TabIndex = 9;
            label3.Text = "Build Location";
            // 
            // b_ok
            // 
            this.b_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_ok.Location = new System.Drawing.Point(257, 170);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 0;
            this.b_ok.Text = "OK";
            this.b_ok.UseVisualStyleBackColor = true;
            // 
            // b_cancel
            // 
            this.b_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_cancel.Location = new System.Drawing.Point(338, 170);
            this.b_cancel.Name = "b_cancel";
            this.b_cancel.Size = new System.Drawing.Size(75, 23);
            this.b_cancel.TabIndex = 1;
            this.b_cancel.Text = "Cancel";
            this.b_cancel.UseVisualStyleBackColor = true;
            // 
            // c_projects
            // 
            this.c_projects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.c_projects.FormattingEnabled = true;
            this.c_projects.Location = new System.Drawing.Point(14, 35);
            this.c_projects.Name = "c_projects";
            this.c_projects.Size = new System.Drawing.Size(399, 21);
            this.c_projects.TabIndex = 2;
            // 
            // t_sourceOutput
            // 
            this.t_sourceOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.t_sourceOutput.Location = new System.Drawing.Point(14, 82);
            this.t_sourceOutput.Name = "t_sourceOutput";
            this.t_sourceOutput.Size = new System.Drawing.Size(318, 20);
            this.t_sourceOutput.TabIndex = 4;
            // 
            // b_projectOutput
            // 
            this.b_projectOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_projectOutput.Location = new System.Drawing.Point(338, 79);
            this.b_projectOutput.Name = "b_projectOutput";
            this.b_projectOutput.Size = new System.Drawing.Size(75, 23);
            this.b_projectOutput.TabIndex = 5;
            this.b_projectOutput.Text = "Browse";
            this.b_projectOutput.UseVisualStyleBackColor = true;
            this.b_projectOutput.Click += new System.EventHandler(this.b_projectOutput_Click);
            // 
            // b_buildOutput
            // 
            this.b_buildOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_buildOutput.Location = new System.Drawing.Point(338, 126);
            this.b_buildOutput.Name = "b_buildOutput";
            this.b_buildOutput.Size = new System.Drawing.Size(75, 23);
            this.b_buildOutput.TabIndex = 8;
            this.b_buildOutput.Text = "Browse";
            this.b_buildOutput.UseVisualStyleBackColor = true;
            this.b_buildOutput.Click += new System.EventHandler(this.b_buildOutput_Click);
            // 
            // t_binaryOutput
            // 
            this.t_binaryOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.t_binaryOutput.Location = new System.Drawing.Point(13, 129);
            this.t_binaryOutput.Name = "t_binaryOutput";
            this.t_binaryOutput.Size = new System.Drawing.Size(319, 20);
            this.t_binaryOutput.TabIndex = 7;
            // 
            // PipelineConfiguration
            // 
            this.AcceptButton = this.b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelButton = this.b_cancel;
            this.ClientSize = new System.Drawing.Size(425, 199);
            this.Controls.Add(label3);
            this.Controls.Add(this.b_buildOutput);
            this.Controls.Add(this.t_binaryOutput);
            this.Controls.Add(label2);
            this.Controls.Add(this.b_projectOutput);
            this.Controls.Add(this.t_sourceOutput);
            this.Controls.Add(label1);
            this.Controls.Add(this.c_projects);
            this.Controls.Add(this.b_cancel);
            this.Controls.Add(this.b_ok);
            this.MinimumSize = new System.Drawing.Size(441, 235);
            this.Name = "PipelineConfiguration";
            this.Text = "PipelineConfiguration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_cancel;
        private System.Windows.Forms.ComboBox c_projects;
        private System.Windows.Forms.TextBox t_sourceOutput;
        private System.Windows.Forms.Button b_projectOutput;
        private System.Windows.Forms.Button b_buildOutput;
        private System.Windows.Forms.TextBox t_binaryOutput;
    }
}