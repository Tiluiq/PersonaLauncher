using System;
using System.Collections.Generic;
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
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

            //this.sb2.Completed += Storyboard_Completed;
            //どこかを持てばドラッグして移動できるようにする
            //this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            Initialize();
		}

        private void Initialize()
        {
            //リソースディクショナリ設定
            //this.Resources.Source = new Uri("/AnimDic.xaml", UriKind.Relative);

            //ファイル・ディレクトリパス読み取り・設定
            //全ての(DataItemの)プロパティを取得・設定
            System.Configuration.SettingsPropertyCollection settings = Properties.Settings.Default.Properties;  //全てのプロパティ取得
            foreach (System.Configuration.SettingsProperty settingsProperty in settings)
            {
                string propertyName = settingsProperty.Name;
                DataItem dataItem = GetDataItem(propertyName);
                if (dataItem != null)
                {
                    string pathStr = (string)Properties.Settings.Default[propertyName];
                    if (File.Exists(pathStr))
                    {
                        dataItem.SetFileName(pathStr);
                    }
                    else if (Directory.Exists(pathStr))
                    {
                        dataItem.SetDirectory(pathStr);
                    }
                    else
                    {
                        //無効なパス(失敗アクションもあるので一応)
                    }
                }
            }

            //開始時アニメーション実行
            try
            {
                Storyboard sb = FindResource("Initialize") as Storyboard;
                if (sb != null)
                {
                    sb.Begin();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("アニメーションInitializeでエラーが発生しました\n" + e, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private bool dataClickLock = false;

		public void Data_Click(object sender, MouseButtonEventArgs e)
		{
            DataItem dataItem = (DataItem)sender;

            //dataItem.Animate(dataItem.Name);
            //dataItem.Animate(dataItem);
            this.Persona.Animate(dataItem);
        }

		// 玲音をドラッグできるようにする
		private void Persona_Drag(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

        public DataItem GetDataItem(string dataName)
        {
            DataItem dataItem = this.FindName(dataName) as DataItem;

            return dataItem;
        }

        //終了時処理
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //全ての(DataItemの)プロパティを保存
            System.Configuration.SettingsPropertyCollection settings = Properties.Settings.Default.Properties;  //全てのプロパティ取得
            foreach (System.Configuration.SettingsProperty settingsProperty in settings)
            {
                string propertyName = settingsProperty.Name;
                DataItem dataItem = GetDataItem(propertyName);
                if (dataItem != null)
                {
                    Properties.Settings.Default[propertyName] = dataItem.PathStr;
                }
            }
            Properties.Settings.Default.Save();
        }
    }
}
