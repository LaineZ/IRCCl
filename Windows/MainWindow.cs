using Eto.Drawing;
using Eto.Forms;
using IRCCl.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IRCCl.Windows
{
    class MainWindow : Form
    {
        TextBox InputMessage = new TextBox();
        Scrollable ScrollLayout = new Scrollable() { MinimumSize = new Size(800, 600) };

        public MainWindow()
        {
            var connect = new ConnectionWindow();
            connect.Show();

            Eto.Style.Add<TableLayout>("padded-table", table => {
                table.Padding = new Padding(5);
            });
            
            Title = "IRCCl 1.0";
            // TODO: load/save window geometry
            ClientSize = new Size(800, 600);
            var layout = new TableLayout() { Style = "padded-table"};
            var messageLayout = new PixelLayout();
            var timer = new UITimer();

            messageLayout.Add(IRC.Messages.MessagesWebView, 0, 0);
            ScrollLayout.Content = messageLayout;

            InputMessage.KeyDown += InputMessage_KeyDown;
            ScrollLayout.Scroll += ScrollLayout_Scroll;

            layout.Rows.Add(new TableRow(ScrollLayout) { ScaleHeight = true });
            layout.Rows.Add(new TableRow(InputMessage) { ScaleHeight = false });
            IRC.Messages.AddSystemMessage("IRCCl by 140bpmdubstep");

            Content = layout;
        }


        private void ScrollLayout_Scroll(object sender, ScrollEventArgs e)
        {
            //Messages.AddSystemMessage(e.ScrollPosition.Y.ToString());
        }

        private async void InputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.IsKeyDown(Keys.Enter))
            {
                var cmdExec = new CommandExecutor();
                var commandRes = await cmdExec.ExecuteCommand(InputMessage.Text);

                if (commandRes == false)
                {
                    await IRC.SendRaw(InputMessage.Text.TrimStart('/'));
                }

                ScrollLayout.ScrollPosition = new Point(ScrollLayout.Size.Width, ScrollLayout.Size.Height);
                InputMessage.Text = "";
            }
        }
    }
}
