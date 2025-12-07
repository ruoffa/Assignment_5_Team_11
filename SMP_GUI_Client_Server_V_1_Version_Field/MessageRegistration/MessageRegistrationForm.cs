using SMP_Library;
using System;
using System.IO;
using System.Windows.Forms;

namespace SMPClientRegistration
{
    public partial class MessageRegistrationForm : Form
    {
        public MessageRegistrationForm()
        {
            InitializeComponent();
        }

        private void registrationButtonClick(object sender, EventArgs e)
        {
            try
            {
                textBoxServerIPAddress.Enabled = false;
                textBoxApplicationPortNumber.Enabled = false;
                textBoxUserId.Enabled = false;
                textBoxPassword.Enabled = false;
                buttonRegister.Enabled = false;

                string userId = textBoxUserId.Text;
                string password = textBoxPassword.Text;

                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please enter both User ID and Password.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                const string PUBLIC_KEY_FILENAME = "PublicKey.xml";
                if (!File.Exists(PUBLIC_KEY_FILENAME))
                {
                    MessageBox.Show("PublicKey.xml not found!\n\n" +
                                  "Please start the SMP Server first to generate encryption keys.", 
                        "Missing Public Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string encryptedUserId = Encryption.EncryptMessage(userId, PUBLIC_KEY_FILENAME);
                string encryptedPassword = Encryption.EncryptMessage(password, PUBLIC_KEY_FILENAME);

                // Create SMP packet for registration
                SmpPacket smpPacket = new SmpPacket(
                    Enumerations.SmpVersion.Version_3_0.ToString(),
                    encryptedUserId,
                    encryptedPassword,
                    Enumerations.SmpMessageType.Registration.ToString(),
                    "",
                    DateTime.Now.ToString(),
                    ""); 

                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(
                    textBoxServerIPAddress.Text, 
                    int.Parse(textBoxApplicationPortNumber.Text)))
                {
                    using (System.Net.Sockets.NetworkStream networkStream = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(networkStream))
                    using (StreamReader reader = new StreamReader(networkStream))
                    {
                        smpPacket.Write(writer);
                        writer.Flush();

                        string responsePacket = reader.ReadToEnd();

                        MessageBox.Show("Server Response: " + responsePacket.Trim(), 
                            "Registration Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show($"Error: {ex.Message}", ex.GetType().Name, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}