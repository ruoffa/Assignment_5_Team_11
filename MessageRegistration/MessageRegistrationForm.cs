using SMP_Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMPClientRegistration
{
    public partial class MessageRegistrationForm : Form
    {
        private const string PublicKeyFilename = "PublicKey.xml";

        public MessageRegistrationForm()
        {
            InitializeComponent();

            MessageRegistration.SMPResponsePacketReceived += SMPClientRegistration_SMPResponsePacketReceived;
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxServerIPAddress.Enabled = false;
                textBoxApplicationPortNumber.Enabled = false;
                textBoxUserId.Enabled = false;
                textBoxPassword.Enabled = false;
                buttonRegister.Enabled = false;

                ProcessSmpRegisterPacket();
            }
            finally
            {
                textBoxServerIPAddress.Enabled = true;
                textBoxApplicationPortNumber.Enabled = true;
                textBoxUserId.Enabled = true;
                textBoxPassword.Enabled = true;
                buttonRegister.Enabled = true;
            }
        }

        private void ProcessSmpRegisterPacket()
        {
            // Get the packet fields.
            string plainUserId = textBoxUserId.Text;
            string plainPassword = textBoxPassword.Text;
            string priority = "";
            string message = "";
            string serverIpAddress = textBoxServerIPAddress.Text;
            int serverPort = int.Parse(textBoxApplicationPortNumber.Text);

            // Verify form is filled.
            if (string.IsNullOrWhiteSpace(plainUserId) || string.IsNullOrWhiteSpace(plainPassword))
            {
                MessageBox.Show("Please enter both User ID and Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Construct the packet.
            SmpPacket smpPacket = new SmpPacket(
                Enumerations.SmpVersion.Version_3_0.ToString(),
                plainUserId,
                plainPassword,
                Enumerations.SmpMessageType.Registration.ToString(),
                priority,
                DateTime.Now.ToString(),
                message);

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
                MessageRegistration.SendSmpPacket(serverIpAddress, serverPort, smpPacket);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SMPClientRegistration_SMPResponsePacketReceived(object sender, SMPResponsePacketEventArgs e)
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
                MessageBox.Show("Server Response: " + eventArgs.ResponseMessage.Trim(), "Registration Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }
    }
}