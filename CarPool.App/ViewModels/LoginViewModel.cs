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

namespace CarPool.App.ViewModels
{
    public class LoginViewModel : ViewModelBase, IListViewModel
    {
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public LoginViewModel(UserFacade userFacade, IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            UserSelectedCommand = new RelayCommand<UserInfoModel>(UserSelected);
            SignInCommand = new RelayCommand(() => { }, () => isUserSelected);


            mediator.Register<UpdateMessage<UserWrapper>>(UserUpdated);
            mediator.Register<DeleteMessage<UserWrapper>>(UserDeleted);
        }

        public ObservableCollection<UserInfoModel> Users { get; set; } = new();

        public ICommand SignInCommand { get;  }
        public ICommand UserSelectedCommand { get; }

        private bool isUserSelected = false;
        private void UserSelected(UserInfoModel? user)
        {
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = user?.Id });
            isUserSelected = true;
        }
        private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();

        private async void UserDeleted(DeleteMessage<UserWrapper> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            Users.Clear();
            var users = await _userFacade.GetAsync();
            Users.AddRange(users);
        }

        public override void LoadInDesignMode()
        {
            Users.Add(new UserInfoModel(
                FirstName: "Lubomir",
                LastName: "Slanina",
                PhotoUrl: "https://images.media-allrecipes.com/userphotos/5716932.jpg"
               ));
        }
    }
}
