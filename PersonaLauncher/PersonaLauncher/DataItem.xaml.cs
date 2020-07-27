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
	public partial class DataItem : Image
	{
		string filePath = "";
		string directoryPath = "";

		public DataItem()
		{
			InitializeComponent();
		}

		private void FileSelect(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() == true)
			{
				filePath = openFileDialog.FileName;
			}
		}

		private void DirectorySelect(object sender, RoutedEventArgs e)
		{
			var openDirectoryDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
			openDirectoryDialog.IsFolderPicker = true;

			if (openDirectoryDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
			{
				directoryPath = openDirectoryDialog.FileName;
			}
		}

		private void DataLeftClick(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
