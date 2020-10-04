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
    }
}
