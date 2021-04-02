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
		MessageView Messages = new MessageView();
		TextBox InputMessage = new TextBox();


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
			var messageLayout = new StackLayout();
			var scrollLayout = new Scrollable() { MinimumSize = new Size(800, 600) };
			messageLayout.Items.Add(new StackLayoutItem(Messages.MessagesWebView));
			scrollLayout.Content = messageLayout;
			InputMessage.KeyDown += InputMessage_KeyDown;

			layout.Rows.Add(new TableRow(scrollLayout) { ScaleHeight = true });
			layout.Rows.Add(new TableRow(InputMessage) { ScaleHeight = false });
			Content = layout;
		}

        private void InputMessage_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.IsKeyDown(Keys.Enter))
			{
				if (InputMessage.Text.StartsWith("https://"))
				{
					Messages.AddImage(InputMessage.Text);
				}
				else
				{
					Messages.AddSystemMessage(InputMessage.Text);
				}
				InputMessage.Text = "";
			}
        }
    }
}
