namespace StorageAccountREST
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
            this.btnPut = new System.Windows.Forms.Button();
            this.btnGet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(1083, 634);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(258, 91);
            this.btnPut.TabIndex = 0;
            this.btnPut.Text = "PUT Blob";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(1083, 784);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(258, 94);
            this.btnGet.TabIndex = 1;
            this.btnGet.Text = "GET Blob";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 980);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnPut);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Button btnGet;
    }
}

