using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Views;

namespace WPF_LoginForm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void Application_Startup(object sender, StartupEventArgs e)
        {
            // Aquí puedes agregar código que se ejecute al iniciar la aplicación
            // instanciamdos la ventana principal
            var loginView = new Views.LoginView();
            loginView.Show();
            loginView.IsVisibleChanged +=(s,ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    // Si la ventana de login se cierra (desaparece), abrimos la ventana principal
                    var mainView = new MainWindow();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
