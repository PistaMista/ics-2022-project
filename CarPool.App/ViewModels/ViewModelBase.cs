using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CarPool.App.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                LoadInDesignMode();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public virtual void LoadInDesignMode() { }
    }
}
