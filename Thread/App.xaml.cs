using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Diagnostics;

namespace Thread
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static int i = 0;
		public static MainWindow mainWindow = null;

		public static void Fun1()
		{
			while (true)
			{
				//mainWindow.TB1.Text = Convert.ToString(i);
				i++;
				Trace.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId + " | " + i);
			}
		}

		public static async void MakeTask()
		{
			for (int j = 0; j < 16; j++)
			{
				await Task.Run(() => Fun1());
			}
		}

		public static void SearchMainWindow()
		{
			foreach (Window window in App.Current.Windows)
			{
				Trace.WriteLine(window.Name);
				if (window.Name == "MainWindow1")
				{
					mainWindow = (MainWindow)window;
				}
				if (mainWindow != null)
				{
					mainWindow.TB1.Text = Convert.ToString(25);
				}
			}
		}

		public static void OpenMainWindow()
		{
			MainWindow window = new MainWindow();
			window.Show();
			mainWindow = window;
		}
		public App()
		{
			OpenMainWindow();
		}
	}
}
