namespace SMPClientRegistration
{
    partial class MessageRegistrationForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApplicationPortNumber = new System.Windows.Forms.TextBox();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxApplicationPortNumber);
            this.groupBox1.Controls.Add(this.textBoxServerIPAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP Address";
            // 
            // textBoxApplicationPortNumber
            // 
            this.textBoxApplicationPortNumber.Location = new System.Drawing.Point(546, 37);
            this.textBoxApplicationPortNumber.Name = "textBoxApplicationPortNumber";
            this.textBoxApplicationPortNumber.Size = new System.Drawing.Size(89, 26);
            this.textBoxApplicationPortNumber.TabIndex = 1;
            this.textBoxApplicationPortNumber.Text = "50400";
            this.textBoxApplicationPortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxServerIPAddress
            // 
            this.textBoxServerIPAddress.Location = new System.Drawing.Point(160, 37);
            this.textBoxServerIPAddress.Name = "textBoxServerIPAddress";
            this.textBoxServerIPAddress.Size = new System.Drawing.Size(162, 26);
            this.textBoxServerIPAddress.TabIndex = 0;
            this.textBoxServerIPAddress.Text = "127.0.0.1";
            this.textBoxServerIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRegister);
            this.groupBox2.Controls.Add(this.labelPassword);
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.labelUserId);
            this.groupBox2.Controls.Add(this.textBoxUserId);
            this.groupBox2.Location = new System.Drawing.Point(13, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registration";
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(160, 130);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(476, 47);
            this.buttonRegister.TabIndex = 8;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.registrationButton_Click);

            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(17, 87);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(78, 20);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(160, 84);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(476, 26);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(17, 43);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(63, 20);
            this.labelUserId.TabIndex = 4;
            this.labelUserId.Text = "User ID";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(160, 40);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(476, 26);
            this.textBoxUserId.TabIndex = 5;
            this.textBoxUserId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MessageRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageRegistrationForm";
            this.ShowIcon = false;
            this.Text = "Client Registration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApplicationPortNumber;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.TextBox textBoxUserId;
    }
}