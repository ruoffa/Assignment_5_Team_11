namespace SMPServer
{
    partial class FormSmpServer
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
            this.buttonStartServer = new System.Windows.Forms.Button();
            this.buttonRegistrations = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioAll = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityHigh = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityLow = new System.Windows.Forms.RadioButton();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.buttonShowMessages = new System.Windows.Forms.Button();
            this.textBoxMessageType = new System.Windows.Forms.TextBox();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPortNumber = new System.Windows.Forms.TextBox();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMessagePriority = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartServer
            // 
            this.buttonStartServer.Location = new System.Drawing.Point(15, 8);
            this.buttonStartServer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStartServer.Name = "buttonStartServer";
            this.buttonStartServer.Size = new System.Drawing.Size(98, 34);
            this.buttonStartServer.TabIndex = 3;
            this.buttonStartServer.Text = "Start Server";
            this.buttonStartServer.UseVisualStyleBackColor = true;
            this.buttonStartServer.Click += new System.EventHandler(this.buttonStartServer_Click);
            // 
            // buttonRegistrations
            // 
            this.buttonRegistrations.Location = new System.Drawing.Point(15, 49);
            this.buttonRegistrations.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRegistrations.Name = "buttonRegistrations";
            this.buttonRegistrations.Size = new System.Drawing.Size(98, 34);
            this.buttonRegistrations.TabIndex = 13;
            this.buttonRegistrations.Text = "Registrations";
            this.buttonRegistrations.UseVisualStyleBackColor = true;
            this.buttonRegistrations.Click += new System.EventHandler(this.buttonRegistrations_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.textBoxMessages);
            this.groupBox1.Controls.Add(this.buttonShowMessages);
            this.groupBox1.Location = new System.Drawing.Point(8, 195);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(383, 222);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioAll);
            this.groupBox3.Controls.Add(this.radioButtonPriorityHigh);
            this.groupBox3.Controls.Add(this.radioButtonPriorityMedium);
            this.groupBox3.Controls.Add(this.radioButtonPriorityLow);
            this.groupBox3.Location = new System.Drawing.Point(5, 19);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(93, 154);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Priority";
            // 
            // radioAll
            // 
            this.radioAll.AutoSize = true;
            this.radioAll.Checked = true;
            this.radioAll.Location = new System.Drawing.Point(7, 123);
            this.radioAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioAll.Name = "radioAll";
            this.radioAll.Size = new System.Drawing.Size(36, 17);
            this.radioAll.TabIndex = 3;
            this.radioAll.TabStop = true;
            this.radioAll.Text = "All";
            this.radioAll.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityHigh
            // 
            this.radioButtonPriorityHigh.AutoSize = true;
            this.radioButtonPriorityHigh.Location = new System.Drawing.Point(7, 90);
            this.radioButtonPriorityHigh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonPriorityHigh.Name = "radioButtonPriorityHigh";
            this.radioButtonPriorityHigh.Size = new System.Drawing.Size(47, 17);
            this.radioButtonPriorityHigh.TabIndex = 2;
            this.radioButtonPriorityHigh.Text = "High";
            this.radioButtonPriorityHigh.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityMedium
            // 
            this.radioButtonPriorityMedium.AutoSize = true;
            this.radioButtonPriorityMedium.Location = new System.Drawing.Point(7, 57);
            this.radioButtonPriorityMedium.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonPriorityMedium.Name = "radioButtonPriorityMedium";
            this.radioButtonPriorityMedium.Size = new System.Drawing.Size(62, 17);
            this.radioButtonPriorityMedium.TabIndex = 1;
            this.radioButtonPriorityMedium.Text = "Medium";
            this.radioButtonPriorityMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityLow
            // 
            this.radioButtonPriorityLow.AutoSize = true;
            this.radioButtonPriorityLow.Location = new System.Drawing.Point(7, 24);
            this.radioButtonPriorityLow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonPriorityLow.Name = "radioButtonPriorityLow";
            this.radioButtonPriorityLow.Size = new System.Drawing.Size(45, 17);
            this.radioButtonPriorityLow.TabIndex = 0;
            this.radioButtonPriorityLow.Text = "Low";
            this.radioButtonPriorityLow.UseVisualStyleBackColor = true;
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new System.Drawing.Point(118, 29);
            this.textBoxMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessages.Size = new System.Drawing.Size(251, 137);
            this.textBoxMessages.TabIndex = 6;
            this.textBoxMessages.WordWrap = false;
            // 
            // buttonShowMessages
            // 
            this.buttonShowMessages.Location = new System.Drawing.Point(180, 179);
            this.buttonShowMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonShowMessages.Name = "buttonShowMessages";
            this.buttonShowMessages.Size = new System.Drawing.Size(137, 31);
            this.buttonShowMessages.TabIndex = 5;
            this.buttonShowMessages.Text = "Show Messages";
            this.buttonShowMessages.UseVisualStyleBackColor = true;
            this.buttonShowMessages.Click += new System.EventHandler(this.buttonShowMessages_Click);
            // 
            // textBoxMessageType
            // 
            this.textBoxMessageType.Location = new System.Drawing.Point(95, 49);
            this.textBoxMessageType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMessageType.Multiline = true;
            this.textBoxMessageType.Name = "textBoxMessageType";
            this.textBoxMessageType.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxMessageType.Size = new System.Drawing.Size(85, 21);
            this.textBoxMessageType.TabIndex = 9;
            this.textBoxMessageType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(95, 20);
            this.textBoxUserId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxUserId.Multiline = true;
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxUserId.Size = new System.Drawing.Size(85, 21);
            this.textBoxUserId.TabIndex = 13;
            this.textBoxUserId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(9, 23);
            this.labelUserId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(43, 13);
            this.labelUserId.TabIndex = 14;
            this.labelUserId.Text = "User ID";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(279, 20);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxPassword.Size = new System.Drawing.Size(89, 21);
            this.textBoxPassword.TabIndex = 15;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(183, 23);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 16;
            this.labelPassword.Text = "Password";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxPortNumber);
            this.groupBox2.Controls.Add(this.textBoxServerIPAddress);
            this.groupBox2.Location = new System.Drawing.Point(126, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(265, 75);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP Address";
            // 
            // textBoxPortNumber
            // 
            this.textBoxPortNumber.Location = new System.Drawing.Point(107, 51);
            this.textBoxPortNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPortNumber.Name = "textBoxPortNumber";
            this.textBoxPortNumber.Size = new System.Drawing.Size(144, 20);
            this.textBoxPortNumber.TabIndex = 1;
            this.textBoxPortNumber.Text = "50400";
            this.textBoxPortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxServerIPAddress
            // 
            this.textBoxServerIPAddress.Location = new System.Drawing.Point(107, 24);
            this.textBoxServerIPAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxServerIPAddress.Name = "textBoxServerIPAddress";
            this.textBoxServerIPAddress.Size = new System.Drawing.Size(144, 20);
            this.textBoxServerIPAddress.TabIndex = 0;
            this.textBoxServerIPAddress.Text = "0.0.0.0";
            this.textBoxServerIPAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.textBoxMessagePriority);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.textBoxMessageType);
            this.groupBox4.Controls.Add(this.textBoxUserId);
            this.groupBox4.Controls.Add(this.labelUserId);
            this.groupBox4.Controls.Add(this.textBoxPassword);
            this.groupBox4.Controls.Add(this.labelPassword);
            this.groupBox4.Location = new System.Drawing.Point(8, 94);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(383, 84);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Last Received Message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Message Priority";
            // 
            // textBoxMessagePriority
            // 
            this.textBoxMessagePriority.Location = new System.Drawing.Point(279, 49);
            this.textBoxMessagePriority.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMessagePriority.Multiline = true;
            this.textBoxMessagePriority.Name = "textBoxMessagePriority";
            this.textBoxMessagePriority.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxMessagePriority.Size = new System.Drawing.Size(89, 21);
            this.textBoxMessagePriority.TabIndex = 17;
            this.textBoxMessagePriority.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message Type";
            // 
            // FormSmpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 422);
            this.Controls.Add(this.buttonRegistrations);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStartServer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSmpServer";
            this.ShowIcon = false;
            this.Text = "Message Server Version 3.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStartServer;
        private System.Windows.Forms.Button buttonRegistrations;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioAll;
        private System.Windows.Forms.RadioButton radioButtonPriorityHigh;
        private System.Windows.Forms.RadioButton radioButtonPriorityMedium;
        private System.Windows.Forms.RadioButton radioButtonPriorityLow;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Button buttonShowMessages;
        private System.Windows.Forms.TextBox textBoxMessageType;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPortNumber;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMessagePriority;
    }
}