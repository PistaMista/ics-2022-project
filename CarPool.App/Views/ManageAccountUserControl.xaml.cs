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

namespace CarPool.App.Views
{
    /// <summary>
    /// Interakční logika pro ManageAccountUserControl.xaml
    /// </summary>
    public partial class ManageAccountUserControl : UserControl
    {
        public ManageAccountUserControl()
        {
            InitializeComponent();
        }
        private void btn_logout_click(object sender, RoutedEventArgs e)
        {
            MainWindow.myRides.Visibility = Visibility.Hidden;

        }
        private void btn_addcar_click(object sender, RoutedEventArgs e)
        {
            AddCar.Visibility = Visibility.Visible;
        }
    }
        

}
