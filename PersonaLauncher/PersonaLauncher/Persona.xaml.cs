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
                if (dataItem.IsValidPath())
                {
                    Storyboard sb = FindResource("PersonaThrow") as Storyboard;
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

        // 玲音にファイルの場所を教える
        private void FileSelect(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem menuItem = (MenuItem)sender;
                DataItem dataItem = GetDataItem(menuItem);
                if (dataItem != null)
                {
                    var openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        SetFile(dataItem, openFileDialog.FileName);
                    }
                }
            }
        }
        //Initialize(mainの)とFileSelectで呼び出す用
        public void SetFile(DataItem dataItem, string filePath)
        {
            dataItem.SetFile(filePath);
            //Unselect有効化
            MenuItem parentMenu = GetMenuItemHeader(dataItem);
            unselectEnable(parentMenu);
        }

        //SetFileとSetDirectoryで選択解除を有効化する用 HeaderXを受け取ることを想定
        private void unselectEnable(MenuItem menuItem)
        {
            if (menuItem != null && menuItem.Items != null && menuItem.Items.Count >= 2)
            {
                MenuItem unselectMenu = (MenuItem)menuItem.Items[2];
                if (unselectMenu.Header.ToString() == "選択解除")
                    unselectMenu.IsEnabled = true;
            }
        }

        // 玲音にディレクトリの場所を教える
        private void DirectorySelect(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem menuItem = (MenuItem)sender;
                DataItem dataItem = GetDataItem(menuItem);
                if (dataItem != null)
                {
                    //var openDirectoryDialog = new System.Windows.Forms.OpenFileDialog() { FileName = "ファイル名には何か入っていればOK", Filter = "Folder|.", CheckFileExists = false };
                    var openDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
                    openDirectoryDialog.Description = "ディレクトリを選択";

                    if (openDirectoryDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //SetDirectory(dataItem, System.IO.Path.GetDirectoryName(openDirectoryDialog.FileName));
                        SetDirectory(dataItem, openDirectoryDialog.SelectedPath);
                    }
                }
            }
        }
        //Initialize(mainの)とDirectorySelectで呼び出す用
        public void SetDirectory(DataItem dataItem, string filePath)
        {
            dataItem.SetDirectory(filePath);
            //Unselect有効化
            MenuItem parentMenu = GetMenuItemHeader(dataItem);
            unselectEnable(parentMenu);
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

        private MainWindow GetParentMainWindow()
        {
            return (MainWindow)((Canvas)this.Parent).Parent;
        }

        private DataItem GetDataItem(MenuItem menuItem)
        {
            if (((MenuItem)(menuItem.Parent)).Name.StartsWith("Header"))
            {
                string headerName = ((MenuItem)(menuItem.Parent)).Name;
                string dataName = "";

                switch (headerName)
                {
                    case "Header0":
                        dataName = "Data0";
                        break;
                    case "Header1":
                        dataName = "Data1";
                        break;
                    case "Header2":
                        dataName = "Data2";
                        break;
                    case "Header3":
                        dataName = "Data3";
                        break;
                }

                DataItem dataItem = (DataItem)GetParentMainWindow().FindName(dataName);
                if (dataItem != null)
                    return dataItem;
            }

            return null;
        }

        private MenuItem GetMenuItemHeader(DataItem dataItem)
        {
            if (dataItem.Name.StartsWith("Data"))
            {
                string dataName = dataItem.Name;
                string headerName = "";

                switch (dataName)
                {
                    case "Data0":
                        headerName = "Header0";
                        break;
                    case "Data1":
                        headerName = "Header1";
                        break;
                    case "Data2":
                        headerName = "Header2";
                        break;
                    case "Data3":
                        headerName = "Header3";
                        break;
                }

                MenuItem header = (MenuItem)this.FindName(headerName);//(MenuItem)GetParentMainWindow().FindName(dataName);
                if (header != null)
                    return header;
            }

            return null;
        }
    }
}
