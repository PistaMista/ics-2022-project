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
using System.Collections.ObjectModel;
using System.Linq;
using CarPool.App.Extensions;

namespace CarPool.App.ViewModels
{
    public class RideDriverViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly RideFacade _rideFacade;
        private readonly CarFacade _carFacade;
        private readonly PassengerFacade _passengerFacade;
        private readonly IMessageDialogService _messageDialogService;

        public RideDriverViewModel(
            RideFacade rideFacade,
            CarFacade carFacade,
            PassengerFacade passengerFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _rideFacade = rideFacade;
            _carFacade = carFacade;
            _passengerFacade = passengerFacade;

            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync, CanDelete);
            CarSelectedCommand = new RelayCommand<CarInfoModel>(CarSelected);

            _mediator.Register<SelectedMessage<RideWrapper>>(async x =>
            {
                await LoadAsync(x.Id ?? default);
            });

            _mediator.Register<UserSignedInMessage<UserWrapper>>(async x => {
                defaultDriverId = x?.Id ?? default;
                await LoadAvailableCarsAsync();
            });

            _mediator.Register<UserSignedOutMessage<UserWrapper>>(x =>
            {
                defaultDriverId = default;
                AvailableCars.Clear();
            });

            _mediator.Register<UpdateMessage<CarWrapper>>(async _ => await LoadAvailableCarsAsync());
            _mediator.Register<DeleteMessage<CarWrapper>>(async _ => await LoadAvailableCarsAsync());
        }

        private Guid defaultDriverId = default;

        private RideWrapper? _model = null;
        public RideWrapper? Model
        {
            get => _model; set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CarInfoModel> AvailableCars { get; set; } = new();

        private void CarSelected(CarInfoModel model)
        {
            if (model == null || Model == null)
                return;

            Model.CarId = model.Id;
        }

        public async Task LoadAvailableCarsAsync()
        {
            AvailableCars.Clear();
            var cars = await _carFacade.GetAsync();
            AvailableCars.AddRange(cars.Where(x => x.CarOwnerId == defaultDriverId));
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CarSelectedCommand { get; }
        private bool _isNew = false;
        public bool IsNew { get => _isNew; private set
            {
                _isNew = value;
                OnPropertyChanged();
            }
        }
        private bool CanSave() => (Model != null && Model.IsValid);
        private bool CanDelete() => (Model != null && !IsNew && Model.DriverId == defaultDriverId);

        public async Task LoadAsync(Guid id)
        {
            Model = await _rideFacade.GetAsync(id) ?? RideModel.Empty;
            IsNew = Model.Id == default;
        }

        public async void NewRide()
        {
            Model = RideModel.Empty;
            Model.DriverId = defaultDriverId;
            Model.StartTime = DateTime.Now;

            IsNew = Model.Id == default;
            await LoadAvailableCarsAsync();
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _rideFacade.SaveAsync(Model.Model);
            IsNew = Model.Id == default;
            _mediator.Send(new UpdateMessage<RideWrapper> { Model = Model });
        }

        

        public async Task DeleteAsync()
        {
            if (Model is null)
                return;

            
            var delete = _messageDialogService.Show(
                $"Delete",
                $"Do you want to delete the ride?",
                MessageDialogButtonConfiguration.YesNo,
                MessageDialogResult.No);

            if (delete == MessageDialogResult.No) return;

            try
            {
                await _rideFacade.DeleteAsync(Model!.Id);
            }
            catch
            {
                var _ = _messageDialogService.Show(
                    $"Deleting of ride failed!",
                    "Deleting failed",
                    MessageDialogButtonConfiguration.OK,
                    MessageDialogResult.OK);
            }

            _mediator.Send(new DeleteMessage<RideWrapper>
            {
                Model = Model
            });

            Model = null;
        }
    }
}
