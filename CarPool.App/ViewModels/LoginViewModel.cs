using CarPool.App.Extensions;
using CarPool.App.Messages;
using CarPool.App.Services;
using CarPool.App.Wrappers;
using CarPool.BL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CarPool.App.Commands;
using CarPool.BL.Facades;
using System;

namespace CarPool.App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public LoginViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            UserSelectedCommand = new RelayCommand<UserInfoModel>(UserSelected);
            SignInCommand = new RelayCommand(UserSignedIn, () => selectedUserId != null);
            CancelCommand = new RelayCommand(() => Users = null);
        }

        public ObservableCollection<UserInfoModel>? _users = null;
        public ObservableCollection<UserInfoModel>? Users { get => _users; private set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ICommand SignInCommand { get;  }
        public ICommand UserSelectedCommand { get; }
        public ICommand CancelCommand { get; }
        private Guid? selectedUserId;

        private void UserSelected(UserInfoModel? user)
        {
            selectedUserId = user?.Id;
        }

        private void UserSignedIn()
        {
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = selectedUserId });
        }

        //private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();

        //private async void UserDeleted(DeleteMessage<UserWrapper> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Users = new ObservableCollection<UserInfoModel>();
            var users = await _userFacade.GetAsync();
            Users.AddRange(users);
        }

        public override void LoadInDesignMode()
        {
            //Users.Add(new UserInfoModel(
            //    FirstName: "Lubomir",
            //    LastName: "Slanina",
            //    PhotoUrl: "https://images.media-allrecipes.com/userphotos/5716932.jpg"
            //   ));
        }
    }
}
