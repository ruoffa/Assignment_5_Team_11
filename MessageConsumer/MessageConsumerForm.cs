using SMP_Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMPClientConsumer
{
    public partial class MessageConsumerForm : Form
    {
        private const string PrivateKeyFilename = "PrivateKey.xml";

        public MessageConsumerForm()
        {
            InitializeComponent();

            MessageConsumer.SMPResponsePacketRecieved += SMPClientConsumer_SMPResponsePacketReceived;
        }

        private void buttonGetMessage_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxServerIPAddress.Enabled = false;
                textBoxApplicationPortNumber.Enabled = false;
                buttonGetMessage.Enabled = false;

                ProcessSmpGetPacket();
            }
            finally
            {
                textBoxServerIPAddress.Enabled = true;
                textBoxApplicationPortNumber.Enabled = true;
                buttonGetMessage.Enabled = true;
            }
        }

        private void ProcessSmpGetPacket()
        {

            // Get the packet fields.
            int priority;
            if (radioButtonPriorityLow.Checked) priority = 1;
            else if (radioButtonPriorityMedium.Checked) priority = 2;
            else if (radioButtonPriorityHigh.Checked) priority = 3;
            else return;
            string plainUserId = textBoxUserId.Text;
            string plainPassword = textBoxPassword.Text;
            string plainMessage = "";
            string serverIpAddress = textBoxServerIPAddress.Text;
            int serverPort = int.Parse(textBoxApplicationPortNumber.Text);

            // Construct the packet.
            SmpPacket smpPacket = new SmpPacket(
                Enumerations.SmpVersion.Version_3_0.ToString(),
                plainUserId,
                plainPassword,
                Enumerations.SmpMessageType.GetMessage.ToString(),
                priority.ToString(),
                DateTime.Now.ToString(),
                plainMessage);

            // Encrypt the packet.
            try
            {
                if (!File.Exists(PrivateKeyFilename))
                {
                    throw new FileNotFoundException(
                        PrivateKeyFilename + " not found!\nPlease start the SMP Server first to generate encryption keys.",
                        PrivateKeyFilename);
                }
                smpPacket.UserId = Encryption.EncryptMessage(smpPacket.UserId, PrivateKeyFilename);
                smpPacket.Password = Encryption.EncryptMessage(smpPacket.Password, PrivateKeyFilename);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show("Message encryption failed: " + ex.Message, "Encryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Send the packet.
            try
            {
                MessageConsumer.SendSmpPacket(serverIpAddress, serverPort, smpPacket);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SMPClientConsumer_SMPResponsePacketReceived(object sender, SMPResponsePacketEventArgs e)
        {
            try
            {
                Invoke(new EventHandler<SMPResponsePacketEventArgs>(SMPResponsePacketReceived), sender, e);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private void SMPResponsePacketReceived(object sender, SMPResponsePacketEventArgs eventArgs)
        {
            try
            {
                string response = eventArgs.ResponseMessage;
                string[] entries = response.Split('\n');

                if (entries.Length <= 2)
                {
                    // Response does not contain a date or a message record. It must be an error.
                    MessageBox.Show(response, "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxMessageContent.Text = string.Empty;
                }
                else
                {
                    // Response contains at least 3 entries (title, date and a message record).
                    try
                    {
                        if (!File.Exists(PrivateKeyFilename))
                        {
                            MessageBox.Show("PrivateKey.xml not found!\n\n" +
                                          "Please start the SMP Server first to generate encryption keys.",
                                "Missing Private Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBoxMessageContent.Text = "Error: Private key file not found.";
                            return;
                        }

                        string title = entries[0];
                        string dateTime = entries[1];
                        string encryptedMessage = entries[2];

                        string decryptedMessage = Encryption.DecryptMessage(encryptedMessage, PrivateKeyFilename);

                        string displayText = title + Environment.NewLine;
                        displayText += dateTime + Environment.NewLine;
                        displayText += decryptedMessage;

                        MessageBox.Show("Message retrieved and decrypted...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxMessageContent.Text = displayText;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Decryption failed: " + ex.Message, "Decryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxMessageContent.Text = "Error: Could not decrypt message." + Environment.NewLine + response;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
    }
}