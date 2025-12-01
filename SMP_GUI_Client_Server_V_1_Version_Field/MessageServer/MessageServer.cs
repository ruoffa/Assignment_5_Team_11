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
                    response = ProcessSmpPutPacket(request);
                }
                else if (request.MessageType == Enumerations.SmpMessageType.GetMessage.ToString())
                {
                    response = ProcessSmpGetPacket(request.UserId, request.Password, request.Priority);
                }
                else
                {
                    response = "Unsupported message type";
                }

                // Send the response string.
                SendSmpResponsePacket(response, networkStreamWriter);
            }
        }

        private static string ProcessSmpPutPacket(SmpPacket smpPacket)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Messages.txt", true))
                {
                    smpPacket.Write(writer);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return "Received Packet: " + DateTime.Now;
        }

        private static string ProcessSmpGetPacket(string requestedUserId, string requestedPassword, string requestedPriority)
        {
            // The number of lines in a single message record.
            const int recordSize = 8;

            SmpPacket smpPacket = null;

            try
            {
                if (File.Exists("Messages.txt"))
                {
                    var lines = new List<string>(File.ReadAllLines("Messages.txt"));

                    int i = 0;
                    while (i <= lines.Count - recordSize)
                    {
                        string smpVersion = lines[i++];
                        // Stop parsing if an incompatible record is found.
                        if (smpVersion != Enumerations.SmpVersion.Version_2_0.ToString()) break;

                        string userId = lines[i++];
                        string password = lines[i++];
                        string messageType = lines[i++];
                        string priority = lines[i++];
                        string dateTime = lines[i++];
                        string message = lines[i++];
                        string emptyLine = lines[i++];

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
