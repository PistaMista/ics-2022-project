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
    public class EditCarViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly CarFacade _carFacade;
        private readonly IMessageDialogService _messageDialogService;

        public EditCarViewModel(
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync, CanDelete);

            _mediator.Register<SelectedMessage<CarWrapper>>(async x =>
            {
                await LoadAsync(x.Id ?? Guid.Empty);
            });

            _mediator.Register<UserSignedInMessage<UserWrapper>>(x => defaultOwnerId = x.Id ?? default);
            _mediator.Register<UserSignedOutMessage<UserWrapper>>(x => Model = null);
        }

        private Guid defaultOwnerId = default;

        private CarWrapper? _model = null;
        public CarWrapper? Model
        {
            get => _model; private set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        private bool CanSave() => Model?.IsValid ?? false;
        private bool CanDelete() => Model != null && Model.Id != default;

        public void NewCar()
        {
            Model = CarModel.Empty with { CarOwnerId = defaultOwnerId };
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _carFacade.GetAsync(id) ?? CarModel.Empty;
        }


        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _carFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });
        }


        public async Task DeleteAsync()
        {
            if (Model is null)
                return;

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete {Model?.Manufacturer} {Model?.Type}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _carFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.Manufacturer} {Model?.Type} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<CarWrapper>
                {
                    Model = Model
                });

                Model = null;
            }
        }
    }
}
