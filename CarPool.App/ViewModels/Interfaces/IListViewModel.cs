using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.App.ViewModels
{
    public interface IListViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
