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

namespace CarPool.App.ViewModels
{
    public class EditRideViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly RideFacade _rideFacade;
        private readonly IMessageDialogService _messageDialogService;

        public EditRideViewModel(
            RideFacade rideFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _rideFacade = rideFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);

            _mediator.Register<SelectedMessage<RideWrapper>>(async x =>
            {
                await LoadAsync(x.Id ?? Guid.Empty);
                await LoadCarsAsync();
            });
        }

        private RideWrapper? _model;
        public RideWrapper? Model
        {
            get => _model; private set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CarInfoModel> Cars { get; set; } = new();

        private async void CarSelected(CarInfoModel? model)
        {
            if (model == null)
                return;

            await EditRideViewModel.LoadAsync(model.Id);
        }

        public async Task LoadCarsAsync()
        {
            Cars.Clear();
            var cars = await _carFacade.GetAsync();
            Cars.AddRange(cars.Where(x => x.CarOwnerId == Model?.Id));
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _rideFacade.GetAsync(id) ?? RideModel.Empty;
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
