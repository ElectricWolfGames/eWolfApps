namespace MakeLogos
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LayoutName = new System.Windows.Forms.TextBox();
            this.ColorA = new System.Windows.Forms.TextBox();
            this.ColorB = new System.Windows.Forms.TextBox();
            this.Make = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LayoutName
            // 
            this.LayoutName.Location = new System.Drawing.Point(74, 58);
            this.LayoutName.Name = "LayoutName";
            this.LayoutName.Size = new System.Drawing.Size(252, 23);
            this.LayoutName.TabIndex = 0;
            this.LayoutName.Text = "Layout Name";
            // 
            // ColorA
            // 
            this.ColorA.Location = new System.Drawing.Point(74, 103);
            this.ColorA.Name = "ColorA";
            this.ColorA.Size = new System.Drawing.Size(252, 23);
            this.ColorA.TabIndex = 0;
            this.ColorA.Text = "AA66AA";
            // 
            // ColorB
            // 
            this.ColorB.Location = new System.Drawing.Point(74, 155);
            this.ColorB.Name = "ColorB";
            this.ColorB.Size = new System.Drawing.Size(252, 23);
            this.ColorB.TabIndex = 0;
            this.ColorB.Text = "224433";
            // 
            // Make
            // 
            this.Make.Location = new System.Drawing.Point(251, 208);
            this.Make.Name = "Make";
            this.Make.Size = new System.Drawing.Size(75, 23);
            this.Make.TabIndex = 1;
            this.Make.Text = "Make";
            this.Make.UseVisualStyleBackColor = true;
            this.Make.Click += new System.EventHandler(this.Make_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Make);
            this.Controls.Add(this.ColorB);
            this.Controls.Add(this.ColorA);
            this.Controls.Add(this.LayoutName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LayoutName;
        private System.Windows.Forms.TextBox ColorA;
        private System.Windows.Forms.TextBox ColorB;
        private System.Windows.Forms.Button Make;
    }
}

