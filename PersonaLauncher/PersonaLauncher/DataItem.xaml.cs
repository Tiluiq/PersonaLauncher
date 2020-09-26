using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
				//fileOrDirectoryName = "NO DATA"
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
			get { return (string)this.GetValue(Stretch); }
			set { this.SetValue(Stretch, value); }
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

		public void Animate(string dataName)
		{
			Storyboard sb = null;
			switch (dataName)
			{
				case "Data0":
					sb = FindResource("RightLowerMove") as Storyboard;
					goto default;
				case "Data1":
					sb = FindResource("LeftLowerMove") as Storyboard;
					goto default;
				case "Data2":
					sb = FindResource("RightUpperMove") as Storyboard;
					goto default;
				case "Data3":
					sb = FindResource("LeftUpperMove") as Storyboard;
					goto default;
				default:
					sb.Begin();
					break;
			}
			
			//this.BeginStoryboard(sb);
		}

		//画像サイズに合わせたフォントサイズを取得
		//private int FontSize(string str, System.Drawing.Size size)
		//{
		//	double height, width;

		//	using (Font f = new Font(System.Windows.Forms.Control.DefaultFont.Name, 1))
		//	{
		//		height = this.image.Height;
		//		width = this.image.Width;

		//		int heightSize = (int)(size.Height / height);
		//		int widthSize = (int)(size.Width / width);


		//	}
		//}
	}
}
