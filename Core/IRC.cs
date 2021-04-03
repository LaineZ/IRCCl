using NetIRC;
using NetIRC.Connection;
using NetIRC.Messages;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IRCCl.Core
{
    static class IRC
    {
        public static string NickName;
        public static string Username;
        public static string Serverhost;
        public static Client client;
        public static MessageView Messages = new MessageView();
        public static bool Connected = false;

        public static void ConnectIRC()
        {
            var hostname = Serverhost.Split(":");
            var user = new User(NickName, Username);

            client = new Client(user, new TcpClientConnection());
            client.OnRawDataReceived += Client_RawDataReceived;
            client.RegistrationCompleted += Client_RegistrationCompleted;
            client.OnIRCMessageParsed += Client_OnIRCMessageParsed;

            Task.Run(() => client.ConnectAsync(hostname[0], int.Parse(hostname[1])));

            Messages.AddSystemMessage($"Done");

        }

        private static void Client_OnIRCMessageParsed(Client client, ParsedIRCMessage message)
        {
            if (message.IRCCommand == IRCCommand.ERROR)
            {
                Connected = false;
            }
        }

        private static void Client_RegistrationCompleted(object sender, EventArgs e)
        {
            Messages.AddSystemMessage("Register complete!");
            Connected = true;
        }

        private static void Client_RawDataReceived(Client client, string rawData)
        {
            Messages.AddSystemMessage(rawData);
        }

        public static async void SendMessage(string channel, string message)
        {
            await client.SendAsync(new PrivMsgMessage(channel, message));
        }

        public static async Task SendRaw(string message)
        {
            if (Connected)
            {
                await client.SendRaw(message);
            }
            else
            {
                Messages.AddSystemMessage("Cannot send messages on unconnected buffer...");
            }
        }
    }
}
