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
        private const string PrivateKeyFilename = "PrivateKey.xml";

        public static event EventHandler<PacketEventArgs> PacketReceived;

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
                string response;
                try
                {
                    // Attempt to read a request from the network stream.
                    SmpPacket request = ReceiveSmpRequestPacket(networkStreamReader);

                    // Update the UI.
                    PacketReceived?.Invoke(null, new PacketEventArgs(request));

                    // Process and get the response string based on the request message type.
                    if (request.MessageType == Enumerations.SmpMessageType.PutMessage.ToString())
                        response = ProcessSmpPutPacket(request);
                    else if (request.MessageType == Enumerations.SmpMessageType.GetMessage.ToString())
                        response = ProcessSmpGetPacket(request);
                    else if (request.MessageType == Enumerations.SmpMessageType.Registration.ToString())
                        response = ProcessSmpRegistrationPacket(request);
                    else
                        response = "Unsupported message type";
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                }

                // Send the response string.
                SendSmpResponsePacket(response, networkStreamWriter);
            }
        }

        private static string ProcessSmpPutPacket(SmpPacket request)
        {
            // Try to decrypt the credentials.
            if (!DecryptCredentials(request, out string plainUserId, out string plainPassword))
                return "Decryption error.";

            // Check if user is registered and authenticated
            if (!RegistrationsRecordManager.Contains(plainUserId, plainPassword))
                return "Authentication failed. Invalid user ID or password.";

            // Verify the priority is valid.
            if (!(request.Priority == "0" || request.Priority == "1" || request.Priority == "2" || request.Priority == "3"))
                return "Invalid priority";
            
            // Try to save the message.
            if (!MessageRecordManager.Append(request))
                return "An unexpected error occurred saving the message.";

            // Success: Return a confirmation.
            return "Received Message: " + DateTime.Now;
        }

        private static string ProcessSmpGetPacket(SmpPacket request)
        {
            // Try to decrypt the credentials.
            if (!DecryptCredentials(request, out string plainUserId, out string plainPassword))
                return "Decryption error.";

            // Check if user is registered and authenticated.
            if (!RegistrationsRecordManager.Contains(plainUserId, plainPassword))
                return "Authentication failed. Invalid user ID or password.";

            // Try to get a matching message.
            SmpPacket removed = MessageRecordManager.Remove(plainUserId, plainPassword, request.Priority);
            if (removed == null)
                return "No messages found. Please check your credentials and try again.";

            // Success: Return the message.
            return "Message Information: " + "\n" + removed.DateTime + "\n" + removed.Message;
        }

        private static string ProcessSmpRegistrationPacket(SmpPacket request)
        {
            // Try to decrypt the credentials.
            if (!DecryptCredentials(request, out string plainUserId, out string plainPassword))
                return "Decryption error.";

            // Verify the credentials are valid.
            if (string.IsNullOrWhiteSpace(plainUserId) || string.IsNullOrWhiteSpace(plainPassword))
                return "User ID and password must not be empty.";

            // Try to register the credentials.
            if (RegistrationsRecordManager.Contains(plainUserId))
                return "User ID already registered.";

            // Try to register the credentials.
            if (!RegistrationsRecordManager.Append(request))
                return "An unexpected error occurred registering user.";

            // Success: Return a confirmation.
            return "User registered.";
        }

        private static bool DecryptCredentials(SmpPacket packet, out string plainUserId, out string plainPassword)
        {
            try
            {
                plainUserId = Encryption.DecryptMessage(packet.UserId, PrivateKeyFilename);
                plainPassword = Encryption.DecryptMessage(packet.Password, PrivateKeyFilename);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
                plainUserId = null;
                plainPassword = null;
                return false;
            }
        }

        private static SmpPacket ReceiveSmpRequestPacket(StreamReader reader)
        {
            return SmpPacket.Read(reader);
        }

        private static void SendSmpResponsePacket(string responsePacket, StreamWriter writer)
        {
            writer.WriteLine(responsePacket);
        }

        private static class MessageRecordManager
        {
            private const string Filename = "Messages.txt";
            private const int RecordSize = 8;

            public static bool Append(SmpPacket record)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(Filename, true))
                    {
                        record.Write(writer);
                        writer.WriteLine();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogExeption(ex);
                }
                return false;
            }

            public static SmpPacket Remove(string plainUserId, string plainPassword, string plainPriority)
            {
                if (!File.Exists(Filename)) return null;

                try
                {
                    var lines = new List<string>(File.ReadAllLines(Filename));

                    int i = 0;
                    while (i <= lines.Count - RecordSize)
                    {
                        string smpVersion = lines[i++];
                        // Stop parsing if an incompatible record is found.
                        if (smpVersion != Enumerations.SmpVersion.Version_3_0.ToString())
                            break;
                        string userId = lines[i++];
                        string password = lines[i++];
                        string messageType = lines[i++];
                        string priority = lines[i++];
                        string dateTime = lines[i++];
                        string message = lines[i++];
                        string emptyLine = lines[i++];

                        // Compare with query
                        if (plainPriority == priority &&
                            plainUserId == Encryption.DecryptMessage(userId, PrivateKeyFilename) &&
                            plainPassword == Encryption.DecryptMessage(password, PrivateKeyFilename))
                        {
                            // Remove the record lines.
                            lines.RemoveRange(i - RecordSize, RecordSize);

                            // Update the messages file with the matching record removed.
                            File.WriteAllLines("Messages.txt", lines);

                            // Return the matched record.
                            return new SmpPacket(smpVersion, userId, password, messageType, priority, dateTime, message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogExeption(ex);
                }

                return null;
            }
        }

        private static class RegistrationsRecordManager
        {
            private const string Filename = "Registrations.txt";
            
            public static bool Append(SmpPacket credentials)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(Filename, true))
                    {
                        writer.WriteLine(credentials.UserId);
                        writer.WriteLine(credentials.Password);
                        writer.WriteLine();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogExeption(ex);
                }
                return false;
            }

            public static bool Contains(string plainUserId = null, string plainPassword = null)
            {
                if (!File.Exists(Filename)) return false;

                try
                {
                    using (StreamReader reader = new StreamReader(Filename))
                    {
                        while (!reader.EndOfStream)
                        {
                            string recordUserId = reader.ReadLine();
                            string recordPassword = reader.ReadLine();
                            string recordEmptyLine = reader.ReadLine();

                            // Stop reading at end of stream.
                            if (recordUserId == null || recordPassword == null || recordEmptyLine == null) break;

                            // Jump to next record if user ID does not match.
                            if (plainUserId != null && plainUserId != Encryption.DecryptMessage(recordUserId, PrivateKeyFilename)) continue;

                            // Jump to next record if password does not match.
                            if (plainPassword != null && plainPassword != Encryption.DecryptMessage(recordPassword, PrivateKeyFilename)) continue;

                            // Record matches, return true.
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLogger.LogExeption(ex);
                }

                // No records matched, return false.
                return false;
            }
        }
    }
}