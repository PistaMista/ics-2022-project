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
using CarPool.App.Services.MessageDialog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace CarPool.App.ViewModels
{
    public class ManageAccountViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly UserFacade _userFacade;
        private readonly CarFacade _carFacade;

        private readonly IMessageDialogService _messageDialogService;

        public ManageAccountViewModel(UserFacade userFacade,
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator,
            EditCarViewModel editCarViewModel)
        {
            _userFacade = userFacade;
            _carFacade = carFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;
            EditCarViewModel = editCarViewModel;

            SignoutCommand = new RelayCommand(() => _mediator.Send(new UserSignedOutMessage<UserWrapper>()));

            _mediator.Register<UserSignedInMessage<UserWrapper>>(async x => await LoadUserAsync(x.Id ?? default));
            _mediator.Register<UserSignedOutMessage<UserWrapper>>(x => { UserModel = null; });

            CarSelectedCommand = new RelayCommand<CarInfoModel>(CarSelected);

            SaveChangesCommand = new AsyncRelayCommand(SaveUserAsync, CanSaveUser);
            DeleteAccountCommand = new AsyncRelayCommand(DeleteUserAsync);

            NewCarCommand = new RelayCommand(() => EditCarViewModel.NewCar());

            mediator.Register<UpdateMessage<CarWrapper>>(async x => await LoadUserAsync(UserModel?.Id ?? default));
            mediator.Register<DeleteMessage<CarWrapper>>(async x => await LoadUserAsync(UserModel?.Id ?? default));
        }

        public ObservableCollection<CarInfoModel> Cars { get; set; } = new();
        public EditCarViewModel EditCarViewModel { get; }

        public ICommand SignoutCommand { get; }
        public ICommand SaveChangesCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public ICommand NewCarCommand { get; }
        public ICommand CarSelectedCommand { get; }


        private UserWrapper? _userModel;
        public UserWrapper? UserModel { get => _userModel; private set
            {
                _userModel = value;
                OnPropertyChanged();
            }
        }
        private bool CanSaveUser() => UserModel?.IsValid ?? false;

        private async void CarSelected(CarInfoModel? model) {
            if (model == null)
                return;

            await EditCarViewModel.LoadAsync(model.Id);
        }

        public async Task LoadUserAsync(Guid id)
        {
            UserModel = await _userFacade.GetAsync(id) ?? BL.Models.UserModel.Empty;

            Cars.Clear();
            var cars = await _carFacade.GetAsync();
            Cars.AddRange(cars.Where(x => x.CarOwnerId == id));
        }

        public async Task SaveUserAsync()
        {
            if (UserModel == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            UserModel = await _userFacade.SaveAsync(UserModel.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> { Model = UserModel });
        }

        public async Task DeleteUserAsync()
        {
            if (UserModel is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (UserModel.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete your account?",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _userFacade.DeleteAsync(UserModel!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = UserModel
                });

                _mediator.Send(new UserSignedOutMessage<UserWrapper>());
            }
        }
    }
}
