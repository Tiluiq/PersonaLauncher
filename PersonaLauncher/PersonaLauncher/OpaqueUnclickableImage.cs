using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PersonaLauncher
{
    //透過部分をクリックできないようにしたImage
    //参照：https://stackoverflow.com/questions/4800597/wpf-detect-image-click-only-on-non-transparent-portion
    //HitTestCoreはカーソルが乗ってるだけでも反応するらしい(まあ乗ってるときのイベントとかあるし…)
    class OpaqueUnclickableImage : Image
    {
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            BitmapSource source = (BitmapSource)Source;

            //画像中の位置を取得
            int x = (int)(hitTestParameters.HitPoint.X / ActualWidth * source.PixelWidth);
            int y = (int)(hitTestParameters.HitPoint.Y / ActualHeight * source.PixelHeight);

            //画像の外なら明らかにヒットしないのでnull返す…拡大時にPixelWidthかPixelHeightに一致してしまうのか？(未確認，負になるかも不明)
            if (x < 0 || source.PixelWidth <= x || y < 0 || source.PixelHeight <= y)
                return null;

            //byte配列にそのピクセルをコピー
            byte[] pixel = new byte[4];
            source.CopyPixels(new System.Windows.Int32Rect(x, y, 1, 1), pixel, 4, 0);

            //透過しているかをチェック(閾値は変えれる)
            if (pixel[3] < 1)
                return null;

            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }
    }
}
