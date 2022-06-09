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
    public class RidePassengerViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly RideFacade _rideFacade;
        private readonly PassengerFacade _passengerFacade;
        private readonly IMessageDialogService _messageDialogService;

        public RidePassengerViewModel(
            RideFacade rideFacade,
            PassengerFacade passengerFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _rideFacade = rideFacade;
            _passengerFacade = passengerFacade;

            _messageDialogService = messageDialogService;
            _mediator = mediator;

            RideJoinCommand = new AsyncRelayCommand(JoinRide, () => !IsPassenger);
            RideLeaveCommand = new AsyncRelayCommand(LeaveRide, () => IsPassenger);

            _mediator.Register<SelectedMessage<RideWrapper>>(async x =>
            {
                await LoadAsync(x.Id ?? default);
                if (Model?.DriverId == userGuid)
                {
                    Model = null;
                }
            });

            _mediator.Register<UserSignedInMessage<UserWrapper>>(x => {
                userGuid = x?.Id ?? default;
            });

            _mediator.Register<UserSignedOutMessage<UserWrapper>>(x =>
            {
                userGuid = default;
                Model = null;
            });
        }

        private Guid userGuid;


        private RideModel? _model = null;
        public RideModel? Model
        {
            get => _model; set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private bool _isPassenger = false;
        public bool IsPassenger
        {
            get => _isPassenger;
            private set
            {
                _isPassenger = value;
                OnPropertyChanged();
            }
        }
        public ICommand RideJoinCommand { get; }
        public ICommand RideLeaveCommand { get; }

        public async Task LoadAsync(Guid id)
        {
            Model = await _rideFacade.GetAsync(id);
            IsPassenger = Model?.Passengers.Any(x => x.PassengerId == userGuid) ?? false;
        }

        private async Task JoinRide()
        {
            if (Model == null)
                return;

            await _passengerFacade.AddPassengerToRide(userGuid, Model.Id);
            await LoadAsync(Model.Id);

            _mediator.Send(new UpdateMessage<RideWrapper> { Id = Model?.Id });
        }

        private async Task LeaveRide()
        {
            if (Model == null)
                return;

            await _passengerFacade.RemovePassengerFromRide(userGuid, Model.Id);
            Model = null;

            _mediator.Send(new UpdateMessage<RideWrapper> { Id = Model?.Id });
        }
    }
}
