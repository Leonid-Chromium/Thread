using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;
using System.Diagnostics;

namespace Thread
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static int i = 0;
		public static MainWindow mainWindow = null;

		public static void Fun1()
		{
			while (true)
			{
				//mainWindow.TB1.Text = Convert.ToString(i);
				Task.Delay(1000).Wait();
				i++;
				Trace.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId + " | " + i);
			}
		}

		public static int Fun2(IProgress<int> progress)
		{
			while (true)
			{
				//mainWindow.TB1.Text = Convert.ToString(i);
				Task.Delay(10).Wait();
				i++;
				Trace.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId + " | " + i);
				progress.Report(i);
			}
		}

		public static async void MakeTask()
		{
			for (int j = 0; j < 1; j++)
			{
				await Task.Run(() => Fun1());
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			/*
			System.Threading.Thread myThread1 = new System.Threading.Thread(() => Fun1());
			myThread1.Name = "myThread1";
			System.Threading.Thread myThread2 = new System.Threading.Thread(() => Fun1());
			myThread2.Name = "myThread2";
			System.Threading.Thread myThread3 = new System.Threading.Thread(() => Fun1());
			myThread3.Name = "myThread3";
			System.Threading.Thread myThread4 = new System.Threading.Thread(() => Fun1());
			myThread4.Name = "myThread4";
			System.Threading.Thread myThread5 = new System.Threading.Thread(() => Fun1());
			myThread5.Name = "myThread5";
			System.Threading.Thread myThread6 = new System.Threading.Thread(() => Fun1());
			myThread6.Name = "myThread6";
			System.Threading.Thread myThread7 = new System.Threading.Thread(() => Fun1());
			myThread7.Name = "myThread7";
			System.Threading.Thread myThread8 = new System.Threading.Thread(() => Fun1());
			myThread8.Name = "myThread8";
			System.Threading.Thread[] myTrheadArr = { myThread1, myThread2, myThread3, myThread4, myThread5, myThread6, myThread7, myThread8 };
			foreach (System.Threading.Thread myThread in myTrheadArr)
			{
				myThread.Start();
			}
			*/
		}

		private void MakeTaskButton_Click(object sender, RoutedEventArgs e)
		{
			MakeTask();
		}

		private void SearchMainWindowButton_Click(object sender, RoutedEventArgs e)
		{
			App.SearchMainWindow();
		}

		public void AutoUpdate()
		{
			while (AutoUpdateButton.IsChecked == true)
			{
				TB1.Text = Convert.ToString(App.i);
			}
		}

		private async void AutoUpdateButton_Click(object sender, RoutedEventArgs e)
		{
			await Task.Run(() => AutoUpdate());
		}

		private async void SOFButton_Click(object sender, RoutedEventArgs e)
		{
			var progress = new Progress<string>(s => label1.Content = s);

			string result = await Task.Run(() => Worker.SomeLongOperation(progress));

			this.label1.Content = result;

			int intResult = 0;
			bool success = int.TryParse(result, out intResult);

			Trace.WriteLine(result + "|" + success + "|" + intResult);

			if(success)
			{
				Trace.WriteLine(success);
				this.ProgressBar1.Value = intResult;
			}
			else
			{
				Trace.WriteLine(success);
			}

			try
			{
				this.ProgressBar1.Value = Convert.ToInt32(result);
				Trace.WriteLine("У меня получилость");
			}
			catch
			{
				Trace.WriteLine("У меня не получилость");
			}
		}

		private async void MyAsyncButton_Click(object sender, RoutedEventArgs e)
		{
			Progress<int> progress = new Progress<int>(s => TB2.Text = s.ToString());

			int result = await Task.Run(() => Fun2(progress));

			TB2.Text = result.ToString();
		}
	}

	class Worker
	{
		public static string SomeLongOperation(IProgress<string> progress)
		{
			// Perform a long running work...
			for (var i = 0; i < 10; i++)
			{
				Task.Delay(500).Wait();
				progress.Report(i.ToString());
			}
			return "результат";
		}
	}
}
