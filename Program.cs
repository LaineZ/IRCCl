using Eto.Forms;
using System;
using IRCCl.Windows;

public class Program
{
	[STAThread]
	static void Main()
	{
		new Application().Run(new MainWindow());
	}
}