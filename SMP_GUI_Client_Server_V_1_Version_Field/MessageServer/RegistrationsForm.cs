using System;
using System.IO;
using System.Windows.Forms;
using SMP_Library;

namespace SMPServer
{
    public partial class FormRegistrations : Form
    {
        private const string PRIVATE_KEY_FILENAME = "PrivateKey.xml";

        public FormRegistrations()
        {
            InitializeComponent();
        }

        private void buttonShowRegistrations_Click(object sender, EventArgs e)
        {
            LoadRegistrations();
        }

        private void LoadRegistrations()
        {
            textBoxRegistrations.Clear();

            if (!File.Exists("Registrations.txt"))
            {
                textBoxRegistrations.Text = "No registrations found.";
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader("Registrations.txt"))
                {
                    bool isUserId = true;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                      if (!string.IsNullOrWhiteSpace(line))
                      {
                        if (isUserId)
                        {
                          textBoxRegistrations.AppendText("User Id: " + Encryption.DecryptMessage(line, PRIVATE_KEY_FILENAME) + Environment.NewLine);
                          isUserId = false;
                        } else
                        {
                          if (radioButtonUserIdsAndPasswords.Checked)
                          {
                            textBoxRegistrations.AppendText("Password: " + Encryption.DecryptMessage(line, PRIVATE_KEY_FILENAME) + Environment.NewLine);
                          }
                          isUserId = true;
                        }
                      } else
                      {
                          textBoxRegistrations.AppendText(Environment.NewLine);
                        
                      }

                    }
                }

                if (string.IsNullOrWhiteSpace(textBoxRegistrations.Text))
                {
                    textBoxRegistrations.Text = "No registrations found.";
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                MessageBox.Show("Error loading registrations: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}