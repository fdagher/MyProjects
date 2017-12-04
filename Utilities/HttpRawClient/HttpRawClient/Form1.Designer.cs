namespace HttpRawClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtInput = new System.Windows.Forms.RichTextBox();
            this.txtResponse = new System.Windows.Forms.RichTextBox();
            this.btnExecuteRaw = new System.Windows.Forms.Button();
            this.btnParseExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInput.Location = new System.Drawing.Point(13, 13);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(1345, 1163);
            this.txtInput.TabIndex = 0;
            this.txtInput.Text = resources.GetString("txtInput.Text");
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Location = new System.Drawing.Point(1522, 13);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(1424, 1163);
            this.txtResponse.TabIndex = 2;
            this.txtResponse.Text = "";
            // 
            // btnExecuteRaw
            // 
            this.btnExecuteRaw.Location = new System.Drawing.Point(1364, 540);
            this.btnExecuteRaw.Name = "btnExecuteRaw";
            this.btnExecuteRaw.Size = new System.Drawing.Size(152, 141);
            this.btnExecuteRaw.TabIndex = 3;
            this.btnExecuteRaw.Text = "Execute Raw";
            this.btnExecuteRaw.UseVisualStyleBackColor = true;
            this.btnExecuteRaw.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnParseExecute
            // 
            this.btnParseExecute.Location = new System.Drawing.Point(1364, 393);
            this.btnParseExecute.Name = "btnParseExecute";
            this.btnParseExecute.Size = new System.Drawing.Size(152, 141);
            this.btnParseExecute.TabIndex = 4;
            this.btnParseExecute.Text = "Parse and Execute";
            this.btnParseExecute.UseVisualStyleBackColor = true;
            this.btnParseExecute.Click += new System.EventHandler(this.btnParseExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2949, 1181);
            this.Controls.Add(this.btnParseExecute);
            this.Controls.Add(this.btnExecuteRaw);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInput;
        private System.Windows.Forms.RichTextBox txtResponse;
        private System.Windows.Forms.Button btnExecuteRaw;
        private System.Windows.Forms.Button btnParseExecute;
    }
}

