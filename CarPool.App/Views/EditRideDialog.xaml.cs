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
    /// Interaction logic for CreateRideDialog.xaml
    /// </summary>
    public partial class CreateRideDialog : UserControl
    {
        public CreateRideDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_add_ride_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }

        private void CarsListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditRideViewModel viewModel)
            {
                CarsListBox.ItemsSource = viewModel.Cars;
            }
        }
    }
}
