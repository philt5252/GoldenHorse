namespace TestApp
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
            this.addBtn = new System.Windows.Forms.Button();
            this.input1Tbx = new System.Windows.Forms.TextBox();
            this.input2Tbx = new System.Windows.Forms.TextBox();
            this.sumTbx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(12, 64);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 0;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // input1Tbx
            // 
            this.input1Tbx.Location = new System.Drawing.Point(12, 12);
            this.input1Tbx.Name = "input1Tbx";
            this.input1Tbx.Size = new System.Drawing.Size(100, 20);
            this.input1Tbx.TabIndex = 3;
            // 
            // input2Tbx
            // 
            this.input2Tbx.Location = new System.Drawing.Point(12, 38);
            this.input2Tbx.Name = "input2Tbx";
            this.input2Tbx.Size = new System.Drawing.Size(100, 20);
            this.input2Tbx.TabIndex = 4;
            // 
            // sumTbx
            // 
            this.sumTbx.Location = new System.Drawing.Point(12, 120);
            this.sumTbx.Name = "sumTbx";
            this.sumTbx.Size = new System.Drawing.Size(100, 20);
            this.sumTbx.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.sumTbx);
            this.Controls.Add(this.input2Tbx);
            this.Controls.Add(this.input1Tbx);
            this.Controls.Add(this.addBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox input1Tbx;
        private System.Windows.Forms.TextBox input2Tbx;
        private System.Windows.Forms.TextBox sumTbx;
    }
}

