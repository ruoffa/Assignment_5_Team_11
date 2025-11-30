using SMP_Library;
using System;
using System.Windows.Forms;

namespace SMPClientConsumer
{
    public partial class MessageConsumerForm : Form
    {
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

                if (response.IndexOf('\n') == response.LastIndexOf('\n'))
                {
                    // Response does not contain a date or a message record. It must be an error.
                    MessageBox.Show(response, "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxMessageContent.Text = string.Empty;
                }
                else
                {
                    // Response contains a date and a message record
                    string[] lines = response.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    if (lines.Length >= 3)
                    {
                        try
                        {
                            string dateTime = lines[1];
                            string encryptedMessage = lines[2];

                            string decryptedMessage = Encryption.DecryptMessage(encryptedMessage, "PrivateKey.xml");

                            string displayText = "Message Information:" + Environment.NewLine;
                            displayText += dateTime + Environment.NewLine;
                            displayText += decryptedMessage;

                            MessageBox.Show("Message retrieved and decrypted...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBoxMessageContent.Text = displayText;
                        }
                        catch (Exception decEx)
                        {
                            MessageBox.Show("Decryption failed: " + decEx.Message, "Decryption Error", MessageBoxButtons.OK);
                            textBoxMessageContent.Text = "Error: Could not decrypt message. \n" + response;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Message retrieved...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxMessageContent.Text = response;
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
