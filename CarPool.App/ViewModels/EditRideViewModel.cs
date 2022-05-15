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
    public class EditRideViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly RideFacade _rideFacade;
        private readonly CarFacade _carFacade;
        private readonly IMessageDialogService _messageDialogService;

        public EditRideViewModel(
            RideFacade rideFacade,
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _rideFacade = rideFacade;
            _carFacade = carFacade;

            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            CarSelectedCommand = new RelayCommand<CarInfoModel>(CarSelected);

            _mediator.Register<SelectedMessage<RideWrapper>>(async x =>
            {
                await LoadAsync(x.Id ?? Guid.Empty);
            });

            _mediator.Register<SelectedMessage<UserWrapper>>(async x => {
                userGuid = x?.Id ?? Guid.Empty;
                await LoadCarsAsync();
            });

            _mediator.Register<UpdateMessage<CarWrapper>>(async _ => await LoadCarsAsync());
            _mediator.Register<DeleteMessage<CarWrapper>>(async _ => await LoadCarsAsync());
        }

        private RideWrapper? _model;
        public RideWrapper? Model
        {
            get => _model; set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CarInfoModel> Cars { get; set; } = new();
        private Guid userGuid;

        private void CarSelected(CarInfoModel model)
        {
            if (model == null || Model == null)
                return;

            Model.CarId = model.Id;
            OnPropertyChanged();
        }

        public async Task LoadCarsAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAsync();
            Cars.AddRange(cars.Where(x => x.CarOwnerId == userGuid));
            //Cars.AddRange(cars);
            OnPropertyChanged();
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CarSelectedCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _rideFacade.GetAsync(id) ?? RideModel.Empty;
        }

        public void LoadEmpty()
        {
            Model = RideModel.Empty;
            Model.DriverId = userGuid;
            Model.StartLocation = "Brno";
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _rideFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<RideWrapper> { Model = Model });
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
                return;

            if (Model.Id != Guid.Empty)
            {
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
            }
        }

        //public override void LoadInDesignMode()
        //{
        //    base.LoadInDesignMode();
        //}
    }
}
