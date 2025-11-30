using SMP_Library;
using System;
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
            NetworkStream networkStream = connection.GetStream();

            StreamReader networkStreamReader = new StreamReader(networkStream);

            // Read in the order that ToString() sends them:
            // Version, UserId, Password, MessageType, Priority, DateTime, Message
            string version = networkStreamReader.ReadLine();

            if (version == Enumerations.SmpVersion.Version_2_0.ToString())
            {
                string userId = networkStreamReader.ReadLine();
                string password = networkStreamReader.ReadLine();
                string messageType = networkStreamReader.ReadLine();
                string priority = networkStreamReader.ReadLine();
                string dateTime = networkStreamReader.ReadLine();
                string message = networkStreamReader.ReadLine();

                if (messageType == Enumerations.SmpMessageType.PutMessage.ToString())
                {
                    SmpPacket smpPacket = new SmpPacket(version, userId, password, messageType, priority, dateTime, message);

                    ProcessSmpPutPacket(smpPacket);

                    string responsePacket = "Received Packet: " + DateTime.Now + Environment.NewLine;

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);

                    PacketRecieved?.Invoke(null, eventArgs);
                }
                else if (messageType == Enumerations.SmpMessageType.GetMessage.ToString())
                {
                    SmpPacket smpPacket = ProcessSmpGetPacket(userId, password, priority);
                    string responsePacket;

                    if (smpPacket != null)
                    {
                        string record = smpPacket.DateTime + Environment.NewLine;
                        record += smpPacket.Message + Environment.NewLine;

                        responsePacket = "Message Information: " + Environment.NewLine + record;
                    }
                    else
                    {
                        responsePacket = "No messages found. Please check your credentials and try again." + Environment.NewLine;
                    }

                    SendSmpResponsePacket(responsePacket, networkStream);

                    networkStreamReader.Close();

                    if (smpPacket != null)
                    {
                        PacketEventArgs eventArgs = new PacketEventArgs(smpPacket);
                        PacketRecieved?.Invoke(null, eventArgs);
                    }
                }
            }
            else
            {
                string responsePacket = "Unsupported Version: " + version + Environment.NewLine;

                SendSmpResponsePacket(responsePacket, networkStream);

                networkStreamReader.Close();
            }
        }

        private static void ProcessSmpPutPacket(SmpPacket smpPacket)
        {
            try
            {
                if (smpPacket == null) return;

                string record = smpPacket.Version + Environment.NewLine;
                record += smpPacket.UserId + Environment.NewLine;
                record += smpPacket.Password + Environment.NewLine;
                record += smpPacket.Priority + Environment.NewLine;
                record += smpPacket.DateTime + Environment.NewLine;
                record += smpPacket.Message + Environment.NewLine;

                StreamWriter writer = new StreamWriter("Messages.txt", true);

                writer.Write(record);
                writer.Flush();

                writer.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }
        }

        private static SmpPacket ProcessSmpGetPacket(string requestedUserId, string requestedPassword, string requestedPriority)
        {
            SmpPacket smpPacket = null;

            try
            {
                if (!File.Exists("Messages.txt")) return null;

                StreamReader reader = new StreamReader("Messages.txt");

                string smpVersion = reader.ReadLine();

                // Loop through all messages to find the first one with matching priority
                while (smpVersion != null)
                {
                    string userId = reader.ReadLine();
                    string password = reader.ReadLine();
                    string priority = reader.ReadLine();
                    string dateTime = reader.ReadLine();
                    string message = reader.ReadLine();
                    string emptyLine = reader.ReadLine();

                    if (userId == requestedUserId && password == requestedPassword && priority == requestedPriority)
                    {
                        smpPacket = new SmpPacket(smpVersion, userId, password,
                            Enumerations.SmpMessageType.GetMessage.ToString(),
                            priority, dateTime, message);
                        break;
                    }

                    smpVersion = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogExeption(ex);
            }

            return smpPacket;
        }

        private static void SendSmpResponsePacket(string responsePacket, Stream dataStream)
        {
            StreamWriter writer = new StreamWriter(dataStream);

            writer.Write(responsePacket);
            writer.Flush();

            writer.Close();
        }
    }
}
