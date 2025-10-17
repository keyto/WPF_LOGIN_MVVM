using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
  
        public class LoginViewModel : Shared.ViewModelBase
        {


            #region propiedades
            private string _username;
            private SecureString _password;
            private string _errorMessage;
            private bool _isViewVisible = true;

            public string Username
            {
                get => _username;
                set
                {
                    _username = value;
                    OnpropertyChanged(nameof(Username));
                }
            }
            public SecureString Password
            {
                get => _password;
                set
                {
                    _password = value;
                    OnpropertyChanged(nameof(Password));
                }
            }
            public string ErrorMessage
            {
                get => _errorMessage;
                set
                {
                    _errorMessage = value;
                    OnpropertyChanged(nameof(ErrorMessage));
                }

            }
            public bool IsViewVisible
            {
                get => _isViewVisible;
                set
                {
                    _isViewVisible = value;
                    OnpropertyChanged(nameof(IsViewVisible));
                }
            }


            // Comando para iniciar sesión
            public ICommand LoginCommand { get; }
            public ICommand RecoverPasswordCommand { get; }
            public ICommand ShowPasswordCommand { get; }
            public ICommand RememberPasswordCommand { get; }

            private readonly IUserRepository _userRepository;
            #endregion

            public LoginViewModel()
            {

                // Inicializar repositorio
                _userRepository = new UserRepository();

                // Inicializar comandos
                // 2 parametros: metodo a ejecutar y metodo que valida si se puede ejecutar
                LoginCommand = new Shared.ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
                RecoverPasswordCommand = new Shared.ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
                //ShowPasswordCommand = new Shared.ViewModelCommand(ExecuteShowPasswordCommand);
                //RememberPasswordCommand = new Shared.ViewModelCommand(ExecuteRememberPasswordCommand);
            }


            // validar si se puede ejecutar el comando
            // es decir, si los datos son correctos
            private bool CanExecuteLoginCommand(object obj)
            {
                bool validData;
                if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
                    validData = false;
                else
                    validData = true;

                return validData;
            }

            private void ExecuteLoginCommand(object obj)
            {
                var IsuserValid = _userRepository.AuthenticateUser(new System.Net.NetworkCredential(Username, Password));

                if (IsuserValid)
                {
                    // Login exitoso                
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Username), null); // Establecer el principal de seguridad de la aplicacion

                    IsViewVisible = false; // Ocultar la vista de login
                    ErrorMessage = string.Empty;
                }
                else
                {
                    // Login fallido
                    ErrorMessage = "Usuario o contraseña incorrectos.";
                }
            }

            // este metodo recibe 2 parametros, por eso, en la llamada se usa una expresion lambda
            private void ExecuteRecoverPasswordCommand(string username, string email)
            {
                throw new NotImplementedException();
            }
        }
    }
 
