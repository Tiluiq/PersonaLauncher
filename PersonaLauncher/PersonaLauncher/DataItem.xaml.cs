using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
		string pathStr = "";

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

        public static readonly DependencyProperty translateX = DependencyProperty.Register("TranslateX", typeof(string), typeof(DataItem));
        public string TranslateX
        {
            get { return (string)this.GetValue(translateX); }
            set { this.SetValue(translateX, value); }
        }
        public static readonly DependencyProperty translateY = DependencyProperty.Register("TranslateY", typeof(string), typeof(DataItem));
        public string TranslateY
        {
            get { return (string)this.GetValue(translateY); }
            set { this.SetValue(translateY, value); }
        }



        public void FileSelect(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() == true)
			{
				pathStr = openFileDialog.FileName;
                //this.DataContext = new
                //{
                //    fileOrDirectoryName = System.IO.Path.GetFileName(filePath)
				//};
                this.ImageSource = "ファイル.png";
                this.Visibility = Visibility.Visible;
            }
        }

		public void DirectorySelect(object sender, RoutedEventArgs e)
		{
			var openDirectoryDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
			openDirectoryDialog.IsFolderPicker = true;

			if (openDirectoryDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
			{
                pathStr = openDirectoryDialog.FileName;
				//this.DataContext = new
				//{
				//	fileOrDirectoryName = System.IO.Path.GetFileName(pathStr)
				//};
                this.ImageSource = "空.png";
                this.Visibility = Visibility.Visible;
            }
		}

        public void Unselect(object sender, RoutedEventArgs e)
        {
            this.DataContext = new
            {
                fileOrDirectoryName = ""
            };
            this.ImageSource = "空.png";
            this.Visibility = Visibility.Hidden;
        }


        private void DataLeftClick(object sender, MouseButtonEventArgs e)
		{
			
		}

        public void Animate()
        {
            Storyboard sbParent = new Storyboard();
            Storyboard sb = null;

            //移動前半
            switch (this.Name)
            {
                case "Data0":
                    sb = FindResource("RightLowerMoveBegin") as Storyboard;
                    goto default;
                case "Data1":
                    sb = FindResource("LeftLowerMoveBegin") as Storyboard;
                    goto default;
                case "Data2":
                    sb = FindResource("RightUpperMoveBegin") as Storyboard;
                    goto default;
                case "Data3":
                    sb = FindResource("LeftUpperMoveBegin") as Storyboard;
                    goto default;
                default:
                    //sb.Completed += ResetTranslate;
                    //sb.Begin();
                    sbParent.Children.Add(sb);
                    break;
            }

            //キャッチ時の縮小
            sb = FindResource("ShrinkForCatch") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this);
                sbParent.Children.Add(sb);
            }

            //投げ動作
            sb = FindResource("DataThrown") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this);
                sbParent.Children.Add(sb);
            }

            //投げ回転
            sb = FindResource("DataThrownRotate") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this.image);
                sbParent.Children.Add(sb);
            }

            //移動後半(元の位置に戻すことを想定)
            switch (this.Name)
            {
                case "Data0":
                    sb = FindResource("RightLowerMoveEnd") as Storyboard;
                    goto default;
                case "Data1":
                    sb = FindResource("LeftLowerMoveEnd") as Storyboard;
                    goto default;
                case "Data2":
                    sb = FindResource("RightUpperMoveEnd") as Storyboard;
                    goto default;
                case "Data3":
                    sb = FindResource("LeftUpperMoveEnd") as Storyboard;
                    goto default;
                default:
                    sbParent.Children.Add(sb);
                    break;
            }

            sbParent.Completed += StartWithAssociation;
            try { sbParent.Begin(); }
            catch(Exception e)
            {
                Console.WriteLine(e);
                this.imageRotate.Angle = 40;
            }
            
        }
        
        //選択したファイル・ディレクトリを関連付けで実行
        private void StartWithAssociation(object sender, EventArgs e)
        {
            if (pathStr != "" && (File.Exists(pathStr) || Directory.Exists(pathStr)))
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(pathStr);
            }
        }

        public bool HasPath()
        {
            return pathStr != "";
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
