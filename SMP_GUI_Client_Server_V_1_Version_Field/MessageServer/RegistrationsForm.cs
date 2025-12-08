using System;
using System.Collections.Generic;
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
            const int recordSize = 3;

            buttonShowRegistrations.Text = "Loading";
            buttonShowRegistrations.Enabled = false;

            textBoxRegistrations.Clear();

            try
            {
                if (File.Exists("Registrations.txt"))
                {
                    var lines = new List<string>(File.ReadAllLines("Registrations.txt"));

                    int i = 0;
                    while (i <= lines.Count - recordSize)
                    {
                        string userId = lines[i++];
                        string password = lines[i++];
                        string emptyLine = lines[i++];

                        textBoxRegistrations.AppendText("User Id: " +
                                                        Encryption.DecryptMessage(userId, PRIVATE_KEY_FILENAME) +
                                                        Environment.NewLine);

                        if (radioButtonUserIdsAndPasswords.Checked)
                        {
                            textBoxRegistrations.AppendText("Password: " +
                                                            Encryption.DecryptMessage(password, PRIVATE_KEY_FILENAME) +
                                                            Environment.NewLine);
                        }

                        textBoxRegistrations.AppendText(emptyLine + Environment.NewLine);

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
                MessageBox.Show("Error loading registrations: " + ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonShowRegistrations.Text = "Show Registrations";
                buttonShowRegistrations.Enabled = true;
            }
        }

    }
}