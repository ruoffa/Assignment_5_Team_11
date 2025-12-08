using System;
using System.IO;

namespace SMP_Library
{
    public class SmpPacket
    {
        public string Version;
        public string MessageType;
        public string Priority;
        public string DateTime;
        public string Message;
        public string Password;
        public string UserId;

        public SmpPacket(string version, string userId, string password, string messageType, string priority, string dateTime, string message)
        {
            Version = version;
            Password = password;
            UserId = userId;
            MessageType = messageType;
            Priority = priority;
            DateTime = dateTime;
            Message = message;
        }
        
        /**
         * Write the packet to the writer as a string using the writer's native newline.
         * This will use Environment.NewLine for files and \n for networks.
         */
        public void Write(TextWriter writer)
        {
            writer.WriteLine(Version);
            writer.WriteLine(UserId);
            writer.WriteLine(Password);
            writer.WriteLine(MessageType);
            writer.WriteLine(Priority);
            writer.WriteLine(DateTime);
            writer.WriteLine(Message);
        }

        /**
         * Read the packet from the reader as a string using the reader's native newline.
         * This will use Environment.NewLine for files and \n for networks.
         */
        public static SmpPacket Read(TextReader reader)
        {
            string version = reader.ReadLine();

            if (version != Enumerations.SmpVersion.Version_3_0.ToString())
                // Stop reading if the version is not supported.
                throw new Exception("Unsupported SmpPacket version");

            string userId = reader.ReadLine();
            string password = reader.ReadLine();
            string messageType = reader.ReadLine();
            string priority = reader.ReadLine();
            string dateTime = reader.ReadLine();
            string message = reader.ReadLine();
            return new SmpPacket(version, userId, password, messageType, priority, dateTime, message);
        }
    }
}
