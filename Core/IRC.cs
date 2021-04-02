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
        public static bool NewMessage = false;

        public static void ConnectIRC()
        {
            var hostname = Serverhost.Split(":");
            var user = new User(NickName, Username);

            client = new Client(user, new TcpClientConnection());
            client.OnRawDataReceived += Client_RawDataReceived;

            Task.Run(() => client.ConnectAsync(hostname[0], int.Parse(hostname[1])));

            Messages.AddSystemMessage($"Done");

        }
        private static void Client_RawDataReceived(Client client, string rawData)
        {
            Messages.AddSystemMessage(rawData);
        }



        private static async void EventHub_RegistrationCompleted(object sender, EventArgs e)
        {

        }

        public static async void SendMessage(string channel, string message)
        {
            await client.SendAsync(new PrivMsgMessage(channel, message));
        }
    }
}
