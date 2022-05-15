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

            CarSelectedCommand = new RelayCommand<CarInfoModel>(CarSelected);

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            NewCarCommand = new AsyncRelayCommand(async () => {
                await EditCarViewModel.LoadAsync(Guid.Empty);
                EditCarViewModel.Model.CarOwnerId = Model.Id;
            });

            _mediator.Register<SelectedMessage<UserWrapper>>(async x => {
                await LoadAsync(x.Id ?? Guid.Empty);
                await LoadCarsAsync();
            });

            mediator.Register<UpdateMessage<CarWrapper>>(CarUpdated);
            mediator.Register<DeleteMessage<CarWrapper>>(CarDeleted);
        }

        public ObservableCollection<CarInfoModel> Cars { get; set; } = new();
        public EditCarViewModel EditCarViewModel { get; }

        public ICommand SaveCommand { get;  }
        public ICommand DeleteCommand { get; }

        public ICommand NewCarCommand { get; }
        public ICommand CarSelectedCommand { get; }


        private UserWrapper? _model;
        public UserWrapper? Model { get => _model; private set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private async void CarUpdated(UpdateMessage<CarWrapper> _) => await LoadCarsAsync();

        private async void CarDeleted(DeleteMessage<CarWrapper> _) => await LoadCarsAsync();
        private async void CarSelected(CarInfoModel? model) => await EditCarViewModel.LoadAsync(model?.Id ?? Guid.Empty);

        public async Task LoadCarsAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAsync();
            Cars.AddRange(cars.Where(x => x.CarOwnerId == Model?.Id));
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> { Model = Model });
        }
        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete your account?",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
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
                    Model = Model
                });
            }
        }
    }
}