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
		}

        private bool dataClickLock = false;

		public void Data_Click(object sender, MouseButtonEventArgs e)
		{
            DataItem dataItem = (DataItem)sender;

            //dataItem.Animate(dataItem.Name);
            //dataItem.Animate(dataItem);
            this.Persona.Animate(dataItem);
        }

		//ペルソナをドラッグできるようにする
		private void Persona_Drag(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Storyboard_Completed(object sender, EventArgs e)
		{
			System.Diagnostics.Process process = System.Diagnostics.Process.Start(@"D:\programming\GitHub\PersonaLauncher\PersonaLauncher\PersonaLauncher\関連付け.png");
		}

	}
}
