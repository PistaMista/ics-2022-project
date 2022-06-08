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
    public class RidesViewModel : ViewModelBase, IListViewModel
    {
        private readonly PassengerFacade _passangerFacade;
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;

        public RidesViewModel(RideFacade rideFacade, PassengerFacade passangerFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _passangerFacade = passangerFacade;
            _mediator = mediator;
 
            RideSelectedCommand = new AsyncRelayCommand<RideInfoModel>(RideSelected);

            FilterRidesCommand = new RelayCommand(async () => {
                await LoadAsync();
            });

            RideJoinCommand = new AsyncRelayCommand(JoinRide, CanJoinRide);

            _mediator.Register<SelectedMessage<UserWrapper>>(async x => {
                selectedUserId = x.Id;
                await LoadAsync();
            });

            _mediator.Register<UpdateMessage<RideWrapper>>(async x => await LoadAsync());
            _mediator.Register<DeleteMessage<RideWrapper>>(async x => await LoadAsync());
        }

        public ObservableCollection<RideInfoModel> Rides { get; set; } = new();

        public string FilterStartLocation { get; set; } = "";

        public string FilterEndLocation { get; set; } = "";

        public DateTime FilterStartDate { get; set; } = default(DateTime);

        public ICommand RideSelectedCommand { get; }

        public ICommand FilterRidesCommand { get; }

        public ICommand RideJoinCommand { get; }

        private Guid? selectedUserId;
        private Guid? selectedRideId;

        private async Task RideSelected(RideInfoModel? ride)
        {
            if (ride == null)
                return;

            selectedRideId = ride.Id;
        }

        public async Task LoadAsync()
        {
            selectedRideId = null;
            Rides.Clear();
            var rides = await _rideFacade.FilterOfRides(FilterStartLocation, FilterEndLocation, FilterStartDate);
            Rides.AddRange(rides);
        }

        public override void LoadInDesignMode()
        {
            Rides.Add(new RideInfoModel(
                StartTime: DateTime.Now,
                StartLocation: "Brno",
                EndLocation: "Praha",
                CarId: Guid.Empty,
                DriverId: Guid.Empty
               ));
        }

        public async Task JoinRide()
        {
            if (selectedRideId == null || selectedUserId == null)
                return;

            await _passangerFacade.AddPassengerToRide(userId: (Guid)selectedUserId, rideId: (Guid)selectedRideId);
            await LoadAsync();
            _mediator.Send(new UpdateMessage<RideWrapper>());
        }

        private bool CanJoinRide() => selectedRideId != null && selectedUserId != null;
    }
}
