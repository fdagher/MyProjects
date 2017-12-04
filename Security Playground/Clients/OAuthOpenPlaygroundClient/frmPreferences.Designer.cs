namespace OAuthOpenPlaygroundClient
{
    partial class frmPreferences
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
            this.txtAuthEndpoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTokenEndpoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserInfoEndpoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAuthEndpoint
            // 
            this.txtAuthEndpoint.Location = new System.Drawing.Point(124, 5);
            this.txtAuthEndpoint.Name = "txtAuthEndpoint";
            this.txtAuthEndpoint.Size = new System.Drawing.Size(259, 20);
            this.txtAuthEndpoint.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Authorization Endpoint";
            // 
            // txtTokenEndpoint
            // 
            this.txtTokenEndpoint.Location = new System.Drawing.Point(124, 31);
            this.txtTokenEndpoint.Name = "txtTokenEndpoint";
            this.txtTokenEndpoint.Size = new System.Drawing.Size(259, 20);
            this.txtTokenEndpoint.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Token Endpoint";
            // 
            // txtUserInfoEndpoint
            // 
            this.txtUserInfoEndpoint.Location = new System.Drawing.Point(124, 57);
            this.txtUserInfoEndpoint.Name = "txtUserInfoEndpoint";
            this.txtUserInfoEndpoint.Size = new System.Drawing.Size(259, 20);
            this.txtUserInfoEndpoint.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "UserInfo Endpoint";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(308, 86);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 118);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtUserInfoEndpoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTokenEndpoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAuthEndpoint);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPreferences";
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAuthEndpoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTokenEndpoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserInfoEndpoint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
    }
}