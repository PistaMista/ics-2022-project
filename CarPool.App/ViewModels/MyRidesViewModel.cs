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
using System.Linq;

namespace CarPool.App.ViewModels
{
    public class MyRidesViewModel : ViewModelBase, IListViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;

        public MyRidesViewModel(RideFacade rideFacade,
            IMediator mediator,
            RideDriverViewModel editRideViewModel,
            RidePassengerViewModel ridePassengerViewModel)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;
            RideDriverViewModel = editRideViewModel;
            RidePassengerViewModel = ridePassengerViewModel;

            RideSelectedCommand = new AsyncRelayCommand<RideInfoModel>(RideSelected);

            NewRideCommand = new RelayCommand(() => {
                RideDriverViewModel.NewRide();
            });

            _mediator.Register<UserSignedInMessage<UserWrapper>>(async x => {
                currentUserId = x.Id;
                await LoadAsync();
            });

            _mediator.Register<UpdateMessage<RideWrapper>>(async x => await LoadAsync());
            _mediator.Register<DeleteMessage<RideWrapper>>(async x => await LoadAsync());
        }

        public ObservableCollection<RideInfoModel> Rides { get; set; } = new();

        public RideDriverViewModel RideDriverViewModel { get; }
        public RidePassengerViewModel RidePassengerViewModel { get; }
        public ICommand RideSelectedCommand { get; }

        public ICommand NewRideCommand { get; }

        private Guid? currentUserId;

        private async Task RideSelected(RideInfoModel? ride)
        {
            if (ride == null)
                return;

            _mediator.Send(new SelectedMessage<RideWrapper> { Id = ride.Id});
        }

        public async Task LoadAsync()
        {
            Rides.Clear();
            var rides = await _rideFacade.GetAsync();
            Rides.AddRange(rides.Where(x => x.DriverId == currentUserId || x.Passengers.Exists(y => y.PassengerId == currentUserId)));
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
    }
}
