namespace AppInsightsAnalytics
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.RichTextBox();
            this.txtResponse = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCallAPI = new System.Windows.Forms.Button();
            this.btnParse = new System.Windows.Forms.Button();
            this.txtTransformed = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analytics Query";
            // 
            // txtQuery
            // 
            this.txtQuery.AccessibleDescription = "";
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Location = new System.Drawing.Point(13, 49);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(2918, 323);
            this.txtQuery.TabIndex = 1;
            this.txtQuery.Tag = "";
            this.txtQuery.Text = resources.GetString("txtQuery.Text");
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.BackColor = System.Drawing.Color.White;
            this.txtResponse.Location = new System.Drawing.Point(12, 492);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.Size = new System.Drawing.Size(1500, 612);
            this.txtResponse.TabIndex = 3;
            this.txtResponse.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 457);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Json Response";
            // 
            // btnCallAPI
            // 
            this.btnCallAPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCallAPI.Location = new System.Drawing.Point(904, 399);
            this.btnCallAPI.Name = "btnCallAPI";
            this.btnCallAPI.Size = new System.Drawing.Size(1140, 61);
            this.btnCallAPI.TabIndex = 4;
            this.btnCallAPI.Text = "Call API";
            this.btnCallAPI.UseVisualStyleBackColor = true;
            this.btnCallAPI.Click += new System.EventHandler(this.btnCallAPI_Click);
            // 
            // btnParse
            // 
            this.btnParse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParse.Location = new System.Drawing.Point(869, 1117);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(1212, 61);
            this.btnParse.TabIndex = 5;
            this.btnParse.Text = "Parse Response";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // txtTransformed
            // 
            this.txtTransformed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTransformed.Location = new System.Drawing.Point(1479, 492);
            this.txtTransformed.Name = "txtTransformed";
            this.txtTransformed.ReadOnly = true;
            this.txtTransformed.Size = new System.Drawing.Size(1500, 612);
            this.txtTransformed.TabIndex = 6;
            this.txtTransformed.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2943, 1187);
            this.Controls.Add(this.txtTransformed);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.btnCallAPI);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Insights Analytics Json Retriever";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtQuery;
        private System.Windows.Forms.RichTextBox txtResponse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCallAPI;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.RichTextBox txtTransformed;
    }
}

