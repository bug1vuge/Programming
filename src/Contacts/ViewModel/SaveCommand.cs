using Contacts.ViewModel;
using Contacts.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает комманду для сохранения данных о контакте в файл.
    /// </summary>
    internal class SaveCommand : ICommand
    {
        /// <summary>
        /// Задает или возвараешь экземпляр класса <see cref="ContactSerializer"./>
        /// </summary>
        public ContactSerializer Serializer { get; set; }

        /// <summary>
        /// Задает или возвараешь экземпляр класса <see cref="ModalVM"./>
        /// </summary>
        public ModalVM ViewModel { get; set; }

        /// <summary>
        /// Определяет, может ли команда выполняться в текущий момент времени.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns>True</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Реализует сохранение данных контакта в файл.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter)
        {
            Serializer.SaveContact(ViewModel.Contact);
        }

        /// <summary>
        /// Констуктор с параметрами для класса <see cref="SaveCommand"./>
        /// </summary>
        /// <param name="serializer">Объект класса <see cref="ContactSerializer"./></param>
        /// <param name="viewModel">Объект класса <see cref="ModalVM".</param>
        public SaveCommand(ContactSerializer serializer, ModalVM viewModel)
        {
            Serializer = serializer;
            ViewModel = viewModel;
        }

        /// <summary>
        /// Событие, которое сигнализирует об изменении возможности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

    }
}
