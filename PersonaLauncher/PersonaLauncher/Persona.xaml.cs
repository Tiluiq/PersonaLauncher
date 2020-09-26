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

		public void Animate()
		{
			Storyboard sb = FindResource("storyboard01") as Storyboard;
			sb.Begin();
			//this.BeginStoryboard(sb);
		}

        public static readonly DependencyProperty Source = DependencyProperty.Register("ImageSourceP", typeof(string), typeof(DataItem));
        public string ImageSourceP
        {
            get { return (string)this.GetValue(Source); }
            set { this.SetValue(Source, value); }
        }
        public static readonly DependencyProperty Stretch = DependencyProperty.Register("ImageStretchP", typeof(string), typeof(DataItem));
        public string ImageStretchP
        {
            get { return (string)this.GetValue(Stretch); }
            set { this.SetValue(Stretch, value); }
        }
    }
}
