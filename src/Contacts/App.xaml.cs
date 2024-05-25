using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Contacts
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Хранит экземпляр объекта <see cref="ModalVM"./>
        /// </summary>
        private ModalVM ModalVM { get; set; } = new ModalVM();

        /// <summary>
        /// Метод, который вызывается при запуске программы.
        /// </summary>
        /// <param name="sender">Обработчик события.</param>
        /// <param name="e">Объект события.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {

            MainWindow mainWindow = new MainWindow
            {
                DataContext = ModalVM
            };

            mainWindow.Show();
        }

        /// <summary>
        /// Метод, который вызывается при завершении работы программы.
        /// </summary>
        /// <param name="sender">Обработчик события.</param>
        /// <param name="e">Объект события.</param>
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Application.Current.MainWindow.DataContext is ModalVM viewModel)
            {
                ModalVM.ContactSerializer.SaveContacts(ModalVM.Contacts);
            }
        }
    }
}
