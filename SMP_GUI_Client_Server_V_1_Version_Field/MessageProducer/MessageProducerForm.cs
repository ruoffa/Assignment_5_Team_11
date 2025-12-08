using SMP_Library;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SMPClientProducer
{
    public partial class MessageProducerForm : Form
    {
        private const string PublicKeyFilename = "PublicKey.xml";

        public MessageProducerForm()
        {
            InitializeComponent();

            MessageProducer.SMPResponsePacketRecieved += SMPClientProducer_SMPResponsePacketReceived;
        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxServerIPAddress.Enabled = false;
                textBoxApplicationPortNumber.Enabled = false;
                buttonSendMessage.Enabled = false;

                ProcessSmpPutPacket();
            }
            finally
            {
                textBoxServerIPAddress.Enabled = true;
                textBoxApplicationPortNumber.Enabled = true;
                buttonSendMessage.Enabled = true;
            }
        }

        private void ProcessSmpPutPacket()
        {
            // Get the packet fields.
            int priority;
            if (radioButtonPriorityLow.Checked) priority = 1;
            else if (radioButtonPriorityMedium.Checked) priority = 2;
            else if (radioButtonPriorityHigh.Checked) priority = 3;
            else return;
            string plainUserId = textBoxUserId.Text;
            string plainPassword = textBoxPassword.Text;
            string plainMessage = textBoxMessageContent.Text;
            string serverIpAddress = textBoxServerIPAddress.Text;
            int serverPort = int.Parse(textBoxApplicationPortNumber.Text);

            // Construct the packet.
            SmpPacket smpPacket = new SmpPacket(
                Enumerations.SmpVersion.Version_3_0.ToString(),
                plainUserId,
                plainPassword,
                Enumerations.SmpMessageType.PutMessage.ToString(),
                priority.ToString(),
                DateTime.Now.ToString(),
                plainMessage);

            // Encrypt the packet.
            try
            {
                if (!File.Exists(PublicKeyFilename))
                {
                    throw new FileNotFoundException(
                        PublicKeyFilename + " not found!\nPlease start the SMP Server first to generate encryption keys.",
                        PublicKeyFilename);
                }
                smpPacket.UserId = Encryption.EncryptMessage(smpPacket.UserId, PublicKeyFilename);
                smpPacket.Password = Encryption.EncryptMessage(smpPacket.Password, PublicKeyFilename);
                smpPacket.Message = Encryption.EncryptMessage(smpPacket.Message, PublicKeyFilename);
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
                MessageProducer.SendSmpPacket(serverIpAddress, serverPort, smpPacket);
                MessageBox.Show("Message sent...", "Message Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SMPClientProducer_SMPResponsePacketReceived(object sender, SMPResponsePacketEventArgs e)
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