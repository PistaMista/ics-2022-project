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

namespace CarPool.App.Views
{
    /// <summary>
    /// Interaction logic for MyRidesWindow.xaml
    /// </summary>
    public partial class MyRidesWindow : UserControl
    {
        public MyRidesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateRide.Visibility = Visibility.Visible;
        }
    }
}
