namespace SMPClientConsumer
{
    partial class MessageConsumerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApplicationPortNumber = new System.Windows.Forms.TextBox();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonGetMessage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMessageContent = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonPriorityHigh = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonPriorityLow = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.labelUserId);
            this.groupBox1.Controls.Add(this.textBoxUserId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxApplicationPortNumber);
            this.groupBox1.Controls.Add(this.textBoxServerIPAddress);
            this.groupBox1.Location = new System.Drawing.Point(25, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 130);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(341, 87);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(78, 20);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(425, 84);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(211, 26);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(17, 87);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(63, 20);
            this.labelUserId.TabIndex = 4;
            this.labelUserId.Text = "User ID";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(160, 84);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(162, 26);
            this.textBoxUserId.TabIndex = 5;
            this.textBoxUserId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Application Port Number";
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
            this.groupBox2.Controls.Add(this.buttonGetMessage);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxMessageContent);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(25, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 323);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message";
            // 
            // buttonGetMessage
            // 
            this.buttonGetMessage.Location = new System.Drawing.Point(190, 270);
            this.buttonGetMessage.Name = "buttonGetMessage";
            this.buttonGetMessage.Size = new System.Drawing.Size(462, 47);
            this.buttonGetMessage.TabIndex = 5;
            this.buttonGetMessage.Text = "Get Message";
            this.buttonGetMessage.UseVisualStyleBackColor = true;
            this.buttonGetMessage.Click += new System.EventHandler(this.buttonGetMessage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Message Content";
            // 
            // textBoxMessageContent
            // 
            this.textBoxMessageContent.Location = new System.Drawing.Point(188, 77);
            this.textBoxMessageContent.Multiline = true;
            this.textBoxMessageContent.Name = "textBoxMessageContent";
            this.textBoxMessageContent.ReadOnly = true;
            this.textBoxMessageContent.Size = new System.Drawing.Size(464, 175);
            this.textBoxMessageContent.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonPriorityHigh);
            this.groupBox3.Controls.Add(this.radioButtonPriorityMedium);
            this.groupBox3.Controls.Add(this.radioButtonPriorityLow);
            this.groupBox3.Location = new System.Drawing.Point(30, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 212);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Priority";
            // 
            // radioButtonPriorityHigh
            // 
            this.radioButtonPriorityHigh.AutoSize = true;
            this.radioButtonPriorityHigh.Location = new System.Drawing.Point(10, 147);
            this.radioButtonPriorityHigh.Name = "radioButtonPriorityHigh";
            this.radioButtonPriorityHigh.Size = new System.Drawing.Size(67, 24);
            this.radioButtonPriorityHigh.TabIndex = 2;
            this.radioButtonPriorityHigh.Text = "High";
            this.radioButtonPriorityHigh.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityMedium
            // 
            this.radioButtonPriorityMedium.AutoSize = true;
            this.radioButtonPriorityMedium.Location = new System.Drawing.Point(10, 92);
            this.radioButtonPriorityMedium.Name = "radioButtonPriorityMedium";
            this.radioButtonPriorityMedium.Size = new System.Drawing.Size(90, 24);
            this.radioButtonPriorityMedium.TabIndex = 1;
            this.radioButtonPriorityMedium.Text = "Medium";
            this.radioButtonPriorityMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonPriorityLow
            // 
            this.radioButtonPriorityLow.AutoSize = true;
            this.radioButtonPriorityLow.Checked = true;
            this.radioButtonPriorityLow.Location = new System.Drawing.Point(10, 37);
            this.radioButtonPriorityLow.Name = "radioButtonPriorityLow";
            this.radioButtonPriorityLow.Size = new System.Drawing.Size(63, 24);
            this.radioButtonPriorityLow.TabIndex = 0;
            this.radioButtonPriorityLow.TabStop = true;
            this.radioButtonPriorityLow.Text = "Low";
            this.radioButtonPriorityLow.UseVisualStyleBackColor = true;
            // 
            // MessageConsumerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 520);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageConsumerForm";
            this.ShowIcon = false;
            this.Text = "Message Consumer Version 3.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApplicationPortNumber;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonGetMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMessageContent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonPriorityHigh;
        private System.Windows.Forms.RadioButton radioButtonPriorityMedium;
        private System.Windows.Forms.RadioButton radioButtonPriorityLow;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
    }
}
