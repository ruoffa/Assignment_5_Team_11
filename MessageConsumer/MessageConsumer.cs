using System;
using System.IO;
using System.Net.Sockets;
using SMP_Library;

namespace SMPClientConsumer
{
    internal class MessageConsumer
    {
        public static event EventHandler<SMPResponsePacketEventArgs> SMPResponsePacketRecieved;

        public static void SendSmpPacket(string serverIpAddress, int port, SmpPacket smpPacket)
        {
            using (TcpClient client = new TcpClient(serverIpAddress, port))
            {
                using (NetworkStream networkStream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(networkStream))
                using (StreamReader reader = new StreamReader(networkStream))
                {
                    // Send the SMP packet
                    smpPacket.Write(writer);
                    writer.Flush();

                    // Receive SMP Response from server
                    string responsePacket = reader.ReadToEnd();

                    // Process the received response packet
                    ProcessSmpResponsePacket(responsePacket);
                }
            }
        }
        private static void ProcessSmpResponsePacket(string responsePacket)
        {
            SMPResponsePacketEventArgs eventArgs = new SMPResponsePacketEventArgs(responsePacket);

            SMPResponsePacketRecieved?.Invoke(null, eventArgs);
        }
    }
}
