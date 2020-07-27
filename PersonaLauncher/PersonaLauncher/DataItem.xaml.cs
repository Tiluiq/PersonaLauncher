using Microsoft.Win32;
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

namespace PersonaLauncher
{
	/// <summary>
	/// DataItem.xaml の相互作用ロジック
	/// </summary>
	public partial class DataItem : Grid
	{
		string filePath = "";
		string directoryPath = "";


		public DataItem()
		{
			InitializeComponent();
			this.DataContext = new
			{
				fileOrDirectoryName = "NO DATA"
			};
		}

		public static readonly DependencyProperty Source = DependencyProperty.Register("ImageSource", typeof(string), typeof(DataItem));
		public string ImageSource
		{
			get { return (string)this.GetValue(Source); }
			set { this.SetValue(Source, value); }
		}
		public static readonly DependencyProperty Stretch = DependencyProperty.Register("ImageStretch", typeof(string), typeof(DataItem));
		public string ImageStretch
		{
			get { return (string)this.GetValue(Source); }
			set { this.SetValue(Source, value); }
		}

		

		private void FileSelect(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() == true)
			{
				filePath = openFileDialog.FileName;
				this.DataContext = new
				{
					fileOrDirectoryName = System.IO.Path.GetFileName(filePath)
				};
			}
		}

		private void DirectorySelect(object sender, RoutedEventArgs e)
		{
			var openDirectoryDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
			openDirectoryDialog.IsFolderPicker = true;

			if (openDirectoryDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
			{
				directoryPath = openDirectoryDialog.FileName;
				this.DataContext = new
				{
					fileOrDirectoryName = System.IO.Path.GetFileName(directoryPath)
				};
			}
		}

		private void DataLeftClick(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
