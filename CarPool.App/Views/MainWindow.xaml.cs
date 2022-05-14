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
using CarPool.App.Views;

namespace CarPool.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MyRidesWindow? myRides;
        public MainWindow()
        {
            InitializeComponent();
            myRides = MyRides;
        }

        private void btn_sign_up_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount.Visibility = Visibility.Visible;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Visible;
        }
    }
}
