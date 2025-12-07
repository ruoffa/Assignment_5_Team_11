namespace SMPServer
{
    partial class FormRegistrations
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.textBoxRegistrations = new System.Windows.Forms.TextBox();
            this.radioButtonUserIdsOnly = new System.Windows.Forms.RadioButton();
            this.radioButtonUserIdsAndPasswords = new System.Windows.Forms.RadioButton();
            this.buttonShowRegistrations = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxRegistrations
            // 
            this.textBoxRegistrations.Location = new System.Drawing.Point(20, 20);
            this.textBoxRegistrations.Multiline = true;
            this.textBoxRegistrations.Name = "textBoxRegistrations";
            this.textBoxRegistrations.ReadOnly = true;
            this.textBoxRegistrations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRegistrations.Size = new System.Drawing.Size(570, 520);
            this.textBoxRegistrations.TabIndex = 0;
            // 
            // radioButtonUserIdsOnly
            // 
            this.radioButtonUserIdsOnly.AutoSize = true;
            this.radioButtonUserIdsOnly.Checked = true;
            this.radioButtonUserIdsOnly.Location = new System.Drawing.Point(80, 560);
            this.radioButtonUserIdsOnly.Name = "radioButtonUserIdsOnly";
            this.radioButtonUserIdsOnly.Size = new System.Drawing.Size(93, 24);
            this.radioButtonUserIdsOnly.TabIndex = 1;
            this.radioButtonUserIdsOnly.TabStop = true;
            this.radioButtonUserIdsOnly.Text = "User IDs";
            this.radioButtonUserIdsOnly.UseVisualStyleBackColor = true;
            // 
            // radioButtonUserIdsAndPasswords
            // 
            this.radioButtonUserIdsAndPasswords.AutoSize = true;
            this.radioButtonUserIdsAndPasswords.Location = new System.Drawing.Point(280, 560);
            this.radioButtonUserIdsAndPasswords.Name = "radioButtonUserIdsAndPasswords";
            this.radioButtonUserIdsAndPasswords.Size = new System.Drawing.Size(206, 24);
            this.radioButtonUserIdsAndPasswords.TabIndex = 2;
            this.radioButtonUserIdsAndPasswords.Text = "User IDs and Passwords";
            this.radioButtonUserIdsAndPasswords.UseVisualStyleBackColor = true;
            // 
            // buttonShowRegistrations
            // 
            this.buttonShowRegistrations.Location = new System.Drawing.Point(160, 600);
            this.buttonShowRegistrations.Name = "buttonShowRegistrations";
            this.buttonShowRegistrations.Size = new System.Drawing.Size(295, 35);
            this.buttonShowRegistrations.TabIndex = 3;
            this.buttonShowRegistrations.Text = "Show Registrations";
            this.buttonShowRegistrations.UseVisualStyleBackColor = true;
            this.buttonShowRegistrations.Click += new System.EventHandler(this.buttonShowRegistrations_Click);
            // 
            // FormRegistrations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 650);
            this.Controls.Add(this.buttonShowRegistrations);
            this.Controls.Add(this.radioButtonUserIdsAndPasswords);
            this.Controls.Add(this.radioButtonUserIdsOnly);
            this.Controls.Add(this.textBoxRegistrations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRegistrations";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Registrations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxRegistrations;
        private System.Windows.Forms.RadioButton radioButtonUserIdsOnly;
        private System.Windows.Forms.RadioButton radioButtonUserIdsAndPasswords;
        private System.Windows.Forms.Button buttonShowRegistrations;
    }
}