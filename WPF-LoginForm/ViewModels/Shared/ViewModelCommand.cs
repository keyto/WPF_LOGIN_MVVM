using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;     // referencia al ensamblado de entrada de windows

namespace WPF_LoginForm.ViewModels.Shared
{
    /// <summary>
    /// Esta clase implementa ICommand, una interfaz de WPF que permite vincular acciones (comandos) desde el ViewModel hacia la Vista (XAML), 
    /// siguiendo el patrón MVVM.
    /// </summary>
    public class ViewModelCommand : ICommand
    {
        // Delegado que representa el método a ejecutar (por ejemplo, Guardar()).
        private readonly Action<object> _executeAction;  // accion que se ejecutara

        // definir un predicado que se usara para determinar si el comando puede ejecutarse (Validacion de acción)
        // función booleana que decide si el comando puede ejecutarse (por ejemplo, si un formulario está completo).
        private readonly Predicate<object> _canExecuteAction;

        

        // Constructor que se usa cuando queremos validar la accion
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        // Constructor que se usa cuando NO queremos validar la accion
        public ViewModelCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        // Este evento se dispara cuando cambia la capacidad de ejecucion del comando
        // Suscribimos o damos de baja el evento RequerySuggested del CommandManager segun sea necesario
        // para ello usamos  el administrador de comandos de WPF

        // WPF usa CommandManager.RequerySuggested para recalcular automáticamente cuándo se deben habilitar/deshabilitar los comandos
        // (por ejemplo, al cambiar el foco o un valor de entrada).
        // De esta forma, cuando cambias propiedades del ViewModel, el sistema refresca el estado de los botones automáticamente.
        // ¿Cuándo debo volver a comprobar si el comando puede ejecutarse o no?
        public event EventHandler CanExecuteChanged 
        {
            add { CommandManager.RequerySuggested += value;}
            remove { CommandManager.RequerySuggested -= value; }
        }

        // ¿Puede ejecutarse este comando ahora mismo?
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        // ¿Qué hago cuando el usuario lo ejecuta(ej.clic)?
        public void Execute(object MetodoAEjecutar)
        {
            _executeAction(MetodoAEjecutar);
        }
    }
}
