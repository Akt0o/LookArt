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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LookArt
{
    /// <summary>
    /// Logique d'interaction pour image.xaml
    /// </summary>
    public partial class image : Window
    {
        public image()
        {
            InitializeComponent();
        }
        public image(BitmapImage src)
        {
            InitializeComponent();
            initImg(src);
        }

        public void initImg(BitmapImage src)
        {
            ImgSource.Source = src;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Close image window button method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseImgWin(object sender, RoutedEventArgs e)
        {
            this.Width = 500;
            this.Height = 300;
            this.Visibility = Visibility.Hidden;
        }
    }
}
