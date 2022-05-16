using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CarPool.App.Commands;
using CarPool.App.Messages;
using CarPool.App.Services;
using CarPool.App.Services.MessageDialog;
using CarPool.App.Wrappers;
using CarPool.BL.Facades;
using CarPool.BL.Models;

namespace CarPool.App.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(CreateAccountViewModel m, LoginViewModel l, ManageAccountViewModel manageAccountViewModel, EditCarViewModel editCarViewModel, RidesViewModel ridesViewModel, MyRidesViewModel myRidesViewModel, EditRideViewModel editRideViewModel)
        {
            CreateAccountViewModel = m;
            LoginViewModel = l;
            ManageAccountViewModel = manageAccountViewModel;
            EditCarViewModel = editCarViewModel;
            RidesViewModel = ridesViewModel;
            MyRidesViewModel = myRidesViewModel;
            EditRideViewModel = editRideViewModel;
        }


        public CreateAccountViewModel CreateAccountViewModel { get; }
        public LoginViewModel LoginViewModel { get;  }
        public ManageAccountViewModel ManageAccountViewModel { get;  }
        public EditCarViewModel EditCarViewModel { get; }
        public RidesViewModel RidesViewModel { get; }
        public MyRidesViewModel MyRidesViewModel { get; }
        public EditRideViewModel EditRideViewModel { get; }
    }
}
