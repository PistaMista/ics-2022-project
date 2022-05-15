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

namespace CarPool.App.ViewModels
{
    public class ManageAccountViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly UserFacade _userFacade;
        private readonly IMessageDialogService _messageDialogService;

        public ManageAccountViewModel(UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;


            _mediator.Register<SelectedMessage<UserWrapper>>(async x => {
                await LoadAsync(x.Id ?? Guid.Empty);
            });
        }

        private UserWrapper? _model;
        public UserWrapper? Model { get => _model; private set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ?? UserModel.Empty;
        }
    }
}
