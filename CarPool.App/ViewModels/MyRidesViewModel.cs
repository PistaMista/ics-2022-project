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
    public class MyRidesViewModel : ViewModelBase, IListViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly IMediator _mediator;

        public MyRidesViewModel(RideFacade rideFacade, IMediator mediator, EditRideViewModel editRideViewModel)
        {
            _rideFacade = rideFacade;
            _mediator = mediator;
            EditRideViewModel = editRideViewModel;

            RideSelectedCommand = new RelayCommand<RideInfoModel>(RideSelected);

            NewRideCommand = new RelayCommand(() => {
                EditRideViewModel.LoadEmpty();
            });

            _mediator.Register<SelectedMessage<UserWrapper>>(async x => {
                selectedUserId = x.Id;
                await LoadAsync();
            });

            _mediator.Register<UpdateMessage<RideWrapper>>(async x => await LoadAsync());
            _mediator.Register<DeleteMessage<RideWrapper>>(async x => await LoadAsync());
        }

        public ObservableCollection<RideInfoModel> Rides { get; set; } = new();

        public EditRideViewModel EditRideViewModel { get; }

        public ICommand RideSelectedCommand { get; }

        public ICommand NewRideCommand { get; }

        private Guid? selectedUserId;

        private void RideSelected(RideInfoModel? user)
        {
            // TODO: ride select logic
        }

        public async Task LoadAsync()
        {
            Rides.Clear();
            var rides = await _rideFacade.GetAsync();
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
    }
}
