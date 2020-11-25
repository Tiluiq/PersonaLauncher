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
        public string PathStr { get; set; } = "";

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

        public void SetFile(string filePath)
        {
            PathStr = filePath;
            Storyboard sb = null;
            sb = FindResource("ShowForFile") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this.image);
                sb.Begin();
            }
            sb = null;
            sb = FindResource("DataVisible") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this);
                sb.Begin();
            }
        }

        public void SetDirectory(string directoryPath)
        {
            PathStr = directoryPath;
            Storyboard sb = null;
            sb = FindResource("ShowForDirectory") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this.image);
                sb.Begin();
            }
            sb = null;
            sb = FindResource("DataVisible") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this);
                sb.Begin();
            }
        }

        public void Unselect()
        {
            PathStr = "";
            Storyboard sb = null;
            sb = FindResource("DataHidden") as Storyboard;
            if (sb != null)
            {
                Storyboard.SetTarget(sb, this);
                sb.Begin();
            }
        }


        private void DataLeftClick(object sender, MouseButtonEventArgs e)
		{
			
		}

        public void Animate()
        {
            Storyboard sbParent = new Storyboard();
            Storyboard sb = null;
            try
            {
                //移動前半
                switch (this.Name)
                {
                    case "Data0":
                        sb = FindResource("RightLowerMoveCatch") as Storyboard;
                        goto default;
                    case "Data1":
                        sb = FindResource("LeftLowerMoveCatch") as Storyboard;
                        goto default;
                    case "Data2":
                        sb = FindResource("RightUpperMoveCatch") as Storyboard;
                        goto default;
                    case "Data3":
                        sb = FindResource("LeftUpperMoveCatch") as Storyboard;
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

                //移動終了(元の位置に戻すことを想定)
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
                sbParent.Begin();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("アニメーションでエラーが出ました\n" + e.ToString(), "エラー", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
        
        //選択したファイル・ディレクトリを関連付けで実行
        private void StartWithAssociation(object sender, EventArgs e)
        {
            if (PathStr != "" && (File.Exists(PathStr) || Directory.Exists(PathStr)))
            {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start(PathStr);
            }
        }

        public bool HasPath()
        {
            return PathStr != "";
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
