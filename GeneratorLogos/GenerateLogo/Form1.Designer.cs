namespace GenerateLogo
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
            this.LogoName = new System.Windows.Forms.TextBox();
            this.ColorA = new System.Windows.Forms.TextBox();
            this.ColorB = new System.Windows.Forms.TextBox();
            this.Make = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LogoName
            // 
            this.LogoName.Location = new System.Drawing.Point(53, 52);
            this.LogoName.Name = "LogoName";
            this.LogoName.Size = new System.Drawing.Size(256, 20);
            this.LogoName.TabIndex = 0;
            this.LogoName.Text = "Logo Name";
            // 
            // ColorA
            // 
            this.ColorA.Location = new System.Drawing.Point(53, 102);
            this.ColorA.Name = "ColorA";
            this.ColorA.Size = new System.Drawing.Size(256, 20);
            this.ColorA.TabIndex = 1;
            this.ColorA.Text = "AA66AA";
            // 
            // ColorB
            // 
            this.ColorB.Location = new System.Drawing.Point(53, 151);
            this.ColorB.Name = "ColorB";
            this.ColorB.Size = new System.Drawing.Size(256, 20);
            this.ColorB.TabIndex = 2;
            this.ColorB.Text = "224433";
            // 
            // Make
            // 
            this.Make.Location = new System.Drawing.Point(234, 197);
            this.Make.Name = "Make";
            this.Make.Size = new System.Drawing.Size(75, 23);
            this.Make.TabIndex = 3;
            this.Make.Text = "Make";
            this.Make.UseVisualStyleBackColor = true;
            this.Make.Click += new System.EventHandler(this.Make_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Make);
            this.Controls.Add(this.ColorB);
            this.Controls.Add(this.ColorA);
            this.Controls.Add(this.LogoName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogoName;
        private System.Windows.Forms.TextBox ColorA;
        private System.Windows.Forms.TextBox ColorB;
        private System.Windows.Forms.Button Make;
    }
}

