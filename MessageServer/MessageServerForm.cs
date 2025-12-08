using SMP_Library;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SMPServer
{
    public partial class FormSmpServer : Form
    {
        public string IpAddress;
        public int Port;

        public FormSmpServer()
        {
            InitializeComponent();
        }

        private void buttonStartServer_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateAndDistributeKeys();

                IpAddress = textBoxServerIPAddress.Text;
                Port = int.Parse(textBoxPortNumber.Text);

                MessageServer.PacketReceived += SmpServerPacketReceived;

                ThreadPool.QueueUserWorkItem(MessageServer.Start, this);

                MessageBox.Show("Server started...", "Server Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                buttonStartServer.Enabled = false;
                textBoxServerIPAddress.Enabled = false;
                textBoxPortNumber.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void buttonRegistrations_Click(object sender, EventArgs e)
        {
            try
            {
                FormRegistrations registrationsForm = new FormRegistrations();
                registrationsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show("Error opening registrations form: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateAndDistributeKeys()
        {
            try
            {
                string serverDirectory = Directory.GetCurrentDirectory();
                string publicKeyPath = Path.Combine(serverDirectory, "PublicKey.xml");
                string privateKeyPath = Path.Combine(serverDirectory, "PrivateKey.xml");

                // Only generate new keys if they don't already exist
                if (!File.Exists(publicKeyPath) || !File.Exists(privateKeyPath))
                {
                    Encryption.GeneratePublicPrivateKeyPair(publicKeyPath, privateKeyPath);
                }

                string parentDirectory = Directory.GetParent(serverDirectory).FullName;

                string producerDirectory = Path.Combine(parentDirectory, "MessageProducer");
                string consumerDirectory = Path.Combine(parentDirectory, "MessageConsumer");
                string registrationDirectory = Path.Combine(parentDirectory, "MessageRegistration");

                if (Directory.Exists(producerDirectory))
                {
                    string producerPublicKeyPath = Path.Combine(producerDirectory, "PublicKey.xml");
                    File.Copy(publicKeyPath, producerPublicKeyPath, true);
                }
                else
                {
                    MessageBox.Show("Producer directory not found at: " + producerDirectory, 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Directory.Exists(consumerDirectory))
                {
                    string consumerPrivateKeyPath = Path.Combine(consumerDirectory, "PrivateKey.xml");
                    File.Copy(privateKeyPath, consumerPrivateKeyPath, true);
                }
                else
                {
                    MessageBox.Show("Consumer directory not found at: " + consumerDirectory, 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Directory.Exists(registrationDirectory))
                {
                    string registrationPublicKeyPath = Path.Combine(registrationDirectory, "PublicKey.xml");
                    File.Copy(publicKeyPath, registrationPublicKeyPath, true);
                }
                else
                {
                    MessageBox.Show("Registration directory not found at: " + registrationDirectory, 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to generate or distribute keys: " + ex.Message, 
                    "Key Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void SmpServerPacketReceived(object sender, PacketEventArgs eventArgs)
        {
            try
            {
                Invoke(new EventHandler<PacketEventArgs>(SMPPacketReceived), sender, eventArgs);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void SMPPacketReceived(object sender, PacketEventArgs eventArgs)
        {
            if (eventArgs == null) return;
            
            try
            {
                string userId = eventArgs.SmpPacket.UserId;
                string password = eventArgs.SmpPacket.Password;
                string messageType = eventArgs.SmpPacket.MessageType;
                string messagePriority = eventArgs.SmpPacket.Priority;

                textBoxUserId.Text = userId;
                textBoxPassword.Text = password;
                textBoxMessageType.Text = messageType;
                textBoxMessagePriority.Text = messagePriority;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void buttonShowMessages_Click(object sender, EventArgs e)
        {
            textBoxMessages.Clear();

            if (!File.Exists("Messages.txt")) return;

            StreamReader reader = new StreamReader("Messages.txt");

            string version = reader.ReadLine();

            while (version != null)
            {
                string userId = reader.ReadLine();
                string password = reader.ReadLine();
                string messageType = reader.ReadLine();
                string priority = reader.ReadLine();
                string dateTime = reader.ReadLine();
                string message = reader.ReadLine();
                string emptyLine = reader.ReadLine();

                if (radioAll.Checked ||
                    (radioButtonPriorityLow.Checked && priority == "1") ||
                    (radioButtonPriorityMedium.Checked && priority == "2") ||
                    (radioButtonPriorityHigh.Checked && priority == "3"))
                {
                    string record = "Version: " + version + Environment.NewLine;
                    record += "User ID: " + userId + Environment.NewLine;
                    record += "Password: " + password + Environment.NewLine;
                    record += "Priority: " + priority + Environment.NewLine;
                    record += "Date/Time: " + dateTime + Environment.NewLine;
                    record += "Message: " + message + Environment.NewLine;
                    record += emptyLine + Environment.NewLine;

                    textBoxMessages.AppendText(record);
                }

                version = reader.ReadLine();
            }

            reader.Close();
        }
    }
}