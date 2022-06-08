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
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(CreateAccountViewModel m,
            LoginViewModel l,
            ManageAccountViewModel manageAccountViewModel,
            EditCarViewModel editCarViewModel,
            RidesViewModel ridesViewModel,
            MyRidesViewModel myRidesViewModel,
            RideDriverViewModel rideDriverViewModel,
            RidePassengerViewModel ridePassengerViewModel,
            IMediator mediator)
        {
            CreateAccountViewModel = m;
            LoginViewModel = l;
            ManageAccountViewModel = manageAccountViewModel;
            EditCarViewModel = editCarViewModel;
            RidesViewModel = ridesViewModel;
            MyRidesViewModel = myRidesViewModel;
            RideDriverViewModel = rideDriverViewModel;
            RidePassengerViewModel = ridePassengerViewModel;

            mediator.Register<UserSignedInMessage<UserWrapper>>(x => isUserSignedIn = true);
            mediator.Register<UserSignedOutMessage<UserWrapper>>(x => isUserSignedIn = false);

            SignInCommand = new AsyncRelayCommand(LoginViewModel.LoadAsync);
            RegisterCommand = new RelayCommand(CreateAccountViewModel.StartRegistration);
        }

        private bool _isUserSignedIn = false;
        public bool isUserSignedIn { get => _isUserSignedIn; private set
            {
                _isUserSignedIn = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand { get; }
        public ICommand RegisterCommand { get; }

        public CreateAccountViewModel CreateAccountViewModel { get; }
        public LoginViewModel LoginViewModel { get;  }
        public ManageAccountViewModel ManageAccountViewModel { get;  }
        public EditCarViewModel EditCarViewModel { get; }
        public RidesViewModel RidesViewModel { get; }
        public MyRidesViewModel MyRidesViewModel { get; }
        public RideDriverViewModel RideDriverViewModel { get; }
        public RidePassengerViewModel RidePassengerViewModel { get; }
    }
}
