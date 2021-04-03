using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRCCl.Windows
{
    public class ConnectionWindow : Form
    {
        private TextBox serverName = new TextBox() { Text = "irc.esper.net:6667", Size = new Size(240, -1) };
        private TextBox nickName = new TextBox() { Text = "CoolUser1345", Size = new Size(240, -1) };
        private TextBox userName = new TextBox() { Text = "IRCCl", Size = new Size(240, -1) };

        public ConnectionWindow()
        {
            Eto.Style.Add<StackLayout>("padded-stack", stack => {
                stack.Padding = new Padding(5);
            });

            Title = "IRCCl Client";
            ClientSize = new Size(260, 500);
            WindowStyle = WindowStyle.Utility;
            Resizable = false;

            var layout = new StackLayout() { Style = "padded-stack" };
            //var image = new ImageView() { Image = new Bitmap("Assets/logo.png"), Size = new Size(250, 250) };
            var welcomeLabel = new Label() { Text = "Welcome! To IRCCl", Font = new Font("sans-serif", 16) };
            var descLabel = new Label() { Text = "To continue, input IRC network data below:", Size = new Size(-1, 20) };
            var serverLabel = new Label() { Text = "Server name:" };
            var nickLabel = new Label() { Text = "Nick:" };
            var userLabel = new Label() { Text = "Username:" };

            var checkAutoconnect = new CheckBox() { Text = "Auto-connect on next startup" };
            var connectButton = new Button() { Text = "Connect!" };

            connectButton.Click += ConnectButton_Click;

            //layout.Items.Add(new StackLayoutItem(image));
            layout.Items.Add(new StackLayoutItem(welcomeLabel));
            layout.Items.Add(new StackLayoutItem(descLabel));
            layout.Items.Add(new StackLayoutItem(serverLabel));
            layout.Items.Add(new StackLayoutItem(serverName));
            layout.Items.Add(new StackLayoutItem(nickLabel));
            layout.Items.Add(new StackLayoutItem(nickName));
            layout.Items.Add(new StackLayoutItem(userLabel));
            layout.Items.Add(new StackLayoutItem(userName));
            layout.Items.Add(new StackLayoutItem(checkAutoconnect));
            layout.Items.Add(new StackLayoutItem(connectButton));

            Content = layout;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Core.IRC.NickName = nickName.Text;
            Core.IRC.Serverhost = serverName.Text;
            Core.IRC.Username = userName.Text;
            Core.IRC.ConnectIRC();
            Close();
        }
    }
}
