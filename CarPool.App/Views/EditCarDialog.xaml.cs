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
using CarPool.App.ViewModels;

namespace CarPool.App.Views
{
    /// <summary>
    /// Interaction logic for AddCarDialog.xaml
    /// </summary>
    public partial class AddCarDialog : UserControl
    {
        public AddCarDialog()
        {
            InitializeComponent();
        }
        private void btn_x_click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}
