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
using CarPool.App.ViewModels;


namespace CarPool.App.Views
{
    /// <summary>
    /// Interakční logika pro RideUserControl.xaml
    /// </summary>
    public partial class RidesTab : UserControl
    {
        public RidesTab()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RidesViewModel viewModel)
            {
                AllRidesListBox.ItemsSource = viewModel.Rides;
            }
        }
    }
}
