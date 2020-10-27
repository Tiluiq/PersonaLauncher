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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonaLauncher
{
	/// <summary>
	/// Persona.xaml の相互作用ロジック
	/// </summary>
	public partial class Persona : Grid
	{
		public Persona()
		{
			InitializeComponent();
		}

        private bool animateLock = false;

        public void Animate(DataItem dataItem)
		{
            if (animateLock == false)
            {
                animateLock = true;
                if (dataItem.HasPath())
                {
                    Storyboard sb = FindResource("storyboard01") as Storyboard;
                    sb.Completed += animateUnlock;
                    sb.Begin();

                    dataItem.Animate();
                }
                else
                    animateLock = false;
            }
		}

        //animateLock解除用イベント
        private void animateUnlock(object sender, EventArgs e)
        {
            animateLock = false;
        }

        public static readonly DependencyProperty Source = DependencyProperty.Register("ImageSource", typeof(string), typeof(Persona));
        public string ImageSource
        {
            get { return (string)this.GetValue(Source); }
            set { this.SetValue(Source, value); }
        }
        public static readonly DependencyProperty Stretch = DependencyProperty.Register("ImageStretch", typeof(string), typeof(Persona));
        public string ImageStretch
        {
            get { return (string)this.GetValue(Stretch); }
            set { this.SetValue(Stretch, value); }
        }

        //ペルソナにファイルの場所を教える
        private void FileSelect(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem menuItem = (MenuItem)sender;
                DataItem dataItem = GetDataItem(menuItem);
                if (dataItem != null)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        dataItem.SetFileName(openFileDialog.FileName);
                        //Unselect有効化
                        MenuItem parentMenu = (MenuItem)menuItem.Parent;
                        if (parentMenu.Items != null && parentMenu.Items.Count >= 2)
                        {
                            MenuItem unselectMenu = (MenuItem)parentMenu.Items[2];
                            if (unselectMenu.Header.ToString() == "選択解除")
                                unselectMenu.IsEnabled = true;
                        }
                    }
                }
            }
        }

        //ペルソナにディレクトリの場所を教える
        private void DirectorySelect(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem menuItem = (MenuItem)sender;
                DataItem dataItem = GetDataItem(menuItem);
                if (dataItem != null)
                {
                    var openDirectoryDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
                    openDirectoryDialog.IsFolderPicker = true;

                    if (openDirectoryDialog.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
                    {
                        dataItem.SetDirectory(openDirectoryDialog.FileName);
                        //Unselect有効化
                        MenuItem parentMenu = (MenuItem)menuItem.Parent;
                        if (parentMenu.Items != null && parentMenu.Items.Count >= 2)
                        {
                            MenuItem unselectMenu = (MenuItem)parentMenu.Items[2];
                            if (unselectMenu.Header.ToString() == "選択解除")
                                unselectMenu.IsEnabled = true;
                        }
                    }
                }
            }
        }

        //ファイル・ディレクトリの場所を忘れさせる
        private void Unselect(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem menuItem = (MenuItem)sender;
                DataItem dataItem = GetDataItem(menuItem);
                if (dataItem != null)
                {
                    //メッセージボックスで確認
                    if (MessageBox.Show("選択を解除しますか？", "選択解除", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        dataItem.Unselect();
                        //Unselect無効化
                        menuItem.IsEnabled = false;
                    }
                }
            }
        }

        private DataItem GetDataItem(MenuItem menuItem)
        {
            if (((MenuItem)(menuItem.Parent)).Header.ToString().StartsWith("データ"))
            {
                string header = ((MenuItem)(menuItem.Parent)).Header.ToString();
                string dataName = "";

                switch (header)
                {
                    case "データ0":
                        dataName = "Data0";
                        break;
                    case "データ1":
                        dataName = "Data1";
                        break;
                    case "データ2":
                        dataName = "Data2";
                        break;
                    case "データ3":
                        dataName = "Data3";
                        break;
                }

                DataItem dataItem = ((MainWindow)((Canvas)this.Parent).Parent).GetDataItem(dataName);
                if (dataItem != null)
                    return dataItem;
            }

            return null;
        }
    }
}
