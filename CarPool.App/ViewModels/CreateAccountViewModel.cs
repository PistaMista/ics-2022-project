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
    public class CreateAccountViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly UserFacade _userFacade;
        private readonly IMessageDialogService _messageDialogService;

        public CreateAccountViewModel(
            UserFacade userFacade,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _mediator = mediator;

            RegisterCommand = new AsyncRelayCommand(RegisterAsync, CanRegister);
            CancelCommand = new RelayCommand(() => Model = null);
        }

        private UserWrapper? _model = null;
        public UserWrapper? Model
        {
            get => _model; private set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }


        public void StartRegistration()
        {
            Model = UserModel.Empty;
        }

        public async Task RegisterAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> { Model = Model });
            _mediator.Send(new UserSignedInMessage<UserWrapper> { Id = Model.Id });

            Model = UserModel.Empty;
            OnPropertyChanged();
        }

        private bool CanRegister() => Model?.IsValid ?? false;
    }
}
