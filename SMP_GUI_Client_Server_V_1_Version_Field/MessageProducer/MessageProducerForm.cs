using SMP_Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMPClientProducer
{
    public partial class MessageProducerForm : Form
    {
        private const string PUBLIC_KEY_FILENAME = "PublicKey.xml";

        public MessageProducerForm()
        {
            InitializeComponent();

            MessageProducer.SMPResponsePacketRecieved += SMPClientProducer_SMPResponsePacketRecieved;
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            textBoxServerIPAddress.Enabled = false;
            textBoxApplicationPortNumber.Enabled = false;
            buttonSendMessage.Enabled = false;

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
            string plainMessage = textBoxMessageContent.Text;
            string encryptedMessage;

            try
            {
                if (!File.Exists(PUBLIC_KEY_FILENAME))
                {
                    MessageBox.Show("PublicKey.xml not found!\n\n" +
                                  "Please start the SMP Server first to generate encryption keys.", 
                        "Missing Public Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxServerIPAddress.Enabled = true;
                    textBoxApplicationPortNumber.Enabled = true;
                    buttonSendMessage.Enabled = true;
                    return;
                }

                encryptedMessage = Encryption.EncryptMessage(plainMessage, PUBLIC_KEY_FILENAME);
            }
            catch (Exception encEx)
            {
                MessageBox.Show("Message encryption failed: " + encEx.Message, "Encryption Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxServerIPAddress.Enabled = true;
                textBoxApplicationPortNumber.Enabled = true;
                buttonSendMessage.Enabled = true;
                return;
            }

            string encryptedUserId = Encryption.EncryptMessage(userId, PUBLIC_KEY_FILENAME);
            string encryptedPassword = Encryption.EncryptMessage(password, PUBLIC_KEY_FILENAME);

            SmpPacket smpPacket = new SmpPacket(
                Enumerations.SmpVersion.Version_3_0.ToString(),
                encryptedUserId,
                encryptedPassword,
                Enumerations.SmpMessageType.PutMessage.ToString(),
                priority.ToString(),
                DateTime.Now.ToString(),
                encryptedMessage);

            try
            {
                //Send the packet
                MessageProducer.SendSmpPacket(textBoxServerIPAddress.Text,
                    int.Parse(textBoxApplicationPortNumber.Text), smpPacket);

                MessageBox.Show("Message sent...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                buttonSendMessage.Enabled = true;
            }
        }

        private void SMPClientProducer_SMPResponsePacketRecieved(object sender, SMPResponsePacketEventArgs e)
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
                textBoxServerResponse.Text = eventArgs.ResponseMessage.Trim();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
    }
}