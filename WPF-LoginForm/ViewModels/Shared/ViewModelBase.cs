using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_LoginForm.ViewModels.Shared
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        /// <summary>
        /// deinir metodo que se ejecutara cuando una propiedad haya cambiado
        /// el parametro propertyName es el nombre de la propiedad que ha cambiado
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
