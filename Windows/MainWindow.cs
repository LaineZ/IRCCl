using Eto.Drawing;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace IRCCl.Windows
{
	class MainWindow : Form
    {
		public MainWindow()
		{
			Eto.Style.Add<TableLayout>("padded-table", table => {
				table.Padding = new Padding(5);
			});

			Title = "winit window";
			// TODO: load/save window geometry
			ClientSize = new Size(800, 600);

			var connect = new ConnectionWindow();

			connect.Show();

			var layout = new TableLayout() { Style = "padded-table"};
			var messages = new Core.MessageView();
			var messagesView = new WebView();

            for (int i = 0; i < 100; i++)
            {
				messages.AddSystemMessage("hello world");
			}

			messagesView.LoadHtml(messages.Html.ToString());

			messages.Html.WriteToFile("debug.html");

			layout.Rows.Add(new TableRow(messagesView));
			Content = layout;
		}
	}
}
