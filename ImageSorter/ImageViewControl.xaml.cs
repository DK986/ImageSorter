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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageSorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ImageViewControl : Window
    {
        private ImageViewViewModel _model;

        public ImageViewControl()
        {
            InitializeComponent();
            DataContext = _model = new ImageViewViewModel();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            _model.setup();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            _model.changeImage(e);
        }
    }
}
