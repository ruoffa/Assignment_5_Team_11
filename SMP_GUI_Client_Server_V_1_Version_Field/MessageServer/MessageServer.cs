using SMP_Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SMPServer
{
    internal class MessageServer
    {
        public static event EventHandler<PacketEventArgs> PacketRecieved;

        public static void Start(object o)
        {
            if (!(o is FormSmpServer form)) return;

            IPAddress iPAddress = IPAddress.Parse(form.IpAddress);
            int port = form.Port;

            TcpListener server = new TcpListener(iPAddress, port);

            server.Start();

            while (true)
            {
                TcpClient connection = server.AcceptTcpClient();

                ProcessConnection(connection);
            }
        }

        public static void ProcessConnection(TcpClient connection)
        {
            using (NetworkStream networkStream = connection.GetStream())
            using (StreamReader networkStreamReader = new StreamReader(networkStream))
            using (StreamWriter networkStreamWriter = new StreamWriter(networkStream))
            {
                // Attempt to read a request from the network stream.
                SmpPacket request;

                try
                {
                    request = ReceiveSmpRequestPacket(networkStreamReader);
                }
                catch (Exception ex)
                {
                    SendSmpResponsePacket(ex.Message, networkStreamWriter);
                    return;
                }

                // Update the UI.
                PacketRecieved?.Invoke(null, new PacketEventArgs(request));

                // Process and get the response string based on the request message type.
                string response;

                if (request.MessageType == Enumerations.SmpMessageType.PutMessage.ToString())
                {
                    // Check authentication before processing
                    if (IsUserAuthenticated(request.UserId, request.Password))
                    {
                        response = ProcessSmpPutPacket(request);
                    }
                    else
                    {
                        response = "Authentication failed. Invalid user ID or password.";
                    }
                }
                else if (request.MessageType == Enumerations.SmpMessageType.GetMessage.ToString())
                {
                    // Check authentication before processing
                    if (IsUserAuthenticated(request.UserId, request.Password))
                    {
                        response = ProcessSmpGetPacket(request.UserId, request.Password, request.Priority);
                    }
                    else
                    {
                        response = "Authentication failed. Invalid user ID or password.";
                    }
                }
                else if (request.MessageType == Enumerations.SmpMessageType.Registration.ToString())
                {
                    response = ProcessSmpRegistrationPacket(request);
                }
                else
                {
                    response = "Unsupported message type";
                }

                // Send the response string.
                SendSmpResponsePacket(response, networkStreamWriter);
            }
        }

        private static bool IsUserAuthenticated(string encryptedUserId, string encryptedPassword)
        {
            const string REGISTRATIONS_FILE = "Registrations.txt";
            const string PRIVATE_KEY_FILENAME = "PrivateKey.xml";

            if (!File.Exists(REGISTRATIONS_FILE))
            {
                return false;
            }

            if (!File.Exists(PRIVATE_KEY_FILENAME))
            {
                return false;
            }

            try
            {
                // Decrypt the incoming credentials
                string plainUserId = Encryption.DecryptMessage(encryptedUserId, PRIVATE_KEY_FILENAME);
                string plainPassword = Encryption.DecryptMessage(encryptedPassword, PRIVATE_KEY_FILENAME);

                using (StreamReader reader = new StreamReader(REGISTRATIONS_FILE))
                {
                    bool isUserId = true;
                    string currentUserId = "";
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            if (isUserId)
                            {
                                // Decrypt stored userId
                                currentUserId = Encryption.DecryptMessage(line, PRIVATE_KEY_FILENAME);
                                isUserId = false;
                            }
                            else
                            {
                                // Decrypt stored password
                                string storedPassword = Encryption.DecryptMessage(line, PRIVATE_KEY_FILENAME);
                                
                                // Compare decrypted values
                                if (currentUserId == plainUserId && storedPassword == plainPassword)
                                {
                                    return true;
                                }
                                isUserId = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return false;
        }

        private static string ProcessSmpPutPacket(SmpPacket smpPacket)
        {
            const string PRIVATE_KEY_FILENAME = "PrivateKey.xml";

            try
            {
                string priority = smpPacket.Priority;
                if (priority == "0" || priority == "1" || priority == "2" || priority == "3")
                {
                    // Decrypt userId and password before storing
                    string decryptedUserId = Encryption.DecryptMessage(smpPacket.UserId, PRIVATE_KEY_FILENAME);
                    string decryptedPassword = Encryption.DecryptMessage(smpPacket.Password, PRIVATE_KEY_FILENAME);

                    // Create a new packet with decrypted credentials
                    SmpPacket decryptedPacket = new SmpPacket(
                        smpPacket.Version,
                        decryptedUserId,
                        decryptedPassword,
                        smpPacket.MessageType,
                        smpPacket.Priority,
                        smpPacket.DateTime,
                        smpPacket.Message);

                    using (StreamWriter writer = new StreamWriter("Messages.txt", true))
                    {
                        decryptedPacket.Write(writer);
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return "Received Packet: " + DateTime.Now;
        }

        private static string ProcessSmpGetPacket(string requestedEncryptedUserId, string requestedEncryptedPassword, string requestedPriority)
        {
            const string PRIVATE_KEY_FILENAME = "PrivateKey.xml";
            const int recordSize = 8;

            SmpPacket smpPacket = null;

            try
            {
                string requestedUserId = Encryption.DecryptMessage(requestedEncryptedUserId, PRIVATE_KEY_FILENAME);
                string requestedPassword = Encryption.DecryptMessage(requestedEncryptedPassword, PRIVATE_KEY_FILENAME);

                if (File.Exists("Messages.txt"))
                {
                    var lines = new List<string>(File.ReadAllLines("Messages.txt"));

                    int i = 0;
                    while (i <= lines.Count - recordSize)
                    {
                        string smpVersion = lines[i++];
                        // Stop parsing if an incompatible record is found.
                        if (smpVersion != Enumerations.SmpVersion.Version_3_0.ToString()) break;

                        string userId = lines[i++];
                        string password = lines[i++];
                        string messageType = lines[i++];
                        string priority = lines[i++];
                        string dateTime = lines[i++];
                        string message = lines[i++];
                        string emptyLine = lines[i++];

                        // Compare with decrypted credentials (Messages.txt now has plaintext)
                        if (userId == requestedUserId && password == requestedPassword && priority == requestedPriority)
                        {
                            smpPacket = new SmpPacket(smpVersion, userId, password, messageType, priority, dateTime, message);
                            lines.RemoveRange(i - recordSize, recordSize);
                            break;
                        }
                    }

                    // Update the messages file with the matching record removed.
                    File.WriteAllLines("Messages.txt", lines);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return smpPacket != null
                ? "Message Information: " + "\n" + smpPacket.DateTime + "\n" + smpPacket.Message
                : "No messages found. Please check your credentials and try again.";
        }

        private static string ProcessSmpRegistrationPacket(SmpPacket smpPacket)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Registrations.txt", true))
                {
                    smpPacket.WriteCredentials(writer);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
            return "Received Packet: " + DateTime.Now;
        }

        private static SmpPacket ReceiveSmpRequestPacket(StreamReader reader)
        {
            return SmpPacket.Read(reader);
        }

        private static void SendSmpResponsePacket(string responsePacket, StreamWriter writer)
        {
            writer.WriteLine(responsePacket);
        }
    }
}