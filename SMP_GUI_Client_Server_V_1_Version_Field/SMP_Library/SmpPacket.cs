using System;

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

        public override string ToString()
        {
            string packet = Version + Environment.NewLine;
            packet += UserId + Environment.NewLine;
            packet += Password + Environment.NewLine;
            packet += MessageType + Environment.NewLine;
            packet += Priority + Environment.NewLine;
            packet += DateTime + Environment.NewLine;
            packet += Message + Environment.NewLine;

            return packet;
        }
    }
}
