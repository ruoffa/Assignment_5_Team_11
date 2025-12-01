using SMP_Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMPClientConsumer
{
    public partial class MessageConsumerForm : Form
    {
        private const string PRIVATE_KEY_FILENAME = "PrivateKey.xml";

        public MessageConsumerForm()
        {
            InitializeComponent();

            MessageConsumer.SMPResponsePacketRecieved += SMPClientConsumer_SMPResponsePacketRecieved;
        }

        private void buttonGetMessage_Click(object sender, EventArgs e)
        {
            textBoxServerIPAddress.Enabled = false;
            textBoxApplicationPortNumber.Enabled = false;
            buttonGetMessage.Enabled = false;

            int priority;

            //Get the message priority
            if (radioButtonPriorityLow.Checked == true)
            {
                priority = 1;
            }
            else if (radioButtonPriorityMedium.Checked == true)
            {
                priority = 2;
            }
            else
            {
                priority = 3;
            }

            string userId = textBoxUserId.Text;
            string password = textBoxPassword.Text;

            SmpPacket smpPacket = new SmpPacket(
                Enumerations.SmpVersion.Version_2_0.ToString(),
                userId,
                password,
                Enumerations.SmpMessageType.GetMessage.ToString(),
                priority.ToString(),
                null,
                null);

            try
            {
                //Send the packet
                MessageConsumer.SendSmpPacket(textBoxServerIPAddress.Text,
                    int.Parse(textBoxApplicationPortNumber.Text), smpPacket);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textBoxServerIPAddress.Enabled = true;
                textBoxApplicationPortNumber.Enabled = true;
                buttonGetMessage.Enabled = true;
            }
        }

        private void SMPClientConsumer_SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs e)
        {
            try
            {
                Invoke(new EventHandler<SMPResponsePacketEventArgs>(SMPResponsePacketRecieved), sender, e);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
        private void SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs eventArgs)
        {
            try
            {
                string response = eventArgs.ResponseMessage;
                string[] entries = response.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

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
                        if (!File.Exists(PRIVATE_KEY_FILENAME))
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

                        string decryptedMessage = Encryption.DecryptMessage(encryptedMessage, PRIVATE_KEY_FILENAME);

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