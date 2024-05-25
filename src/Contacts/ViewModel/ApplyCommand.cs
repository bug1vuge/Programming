﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает комманду для применения изменений контакта.
    /// </summary>
    internal class ApplyCommand : ICommand
    {

        /// <summary>
        /// Задает или возварает экземпляр класса <see cref="ModalVM"./>
        /// </summary>
        public ModalVM ViewModel { get; set; }

        /// <summary>
        /// Констуктор с параметрами для класса <see cref="AddCommand"./>
        /// </summary>
        /// <param name="viewModel">Объект класса <see cref="ModalVM".</param>
        public ApplyCommand(ModalVM viewModel)
        {
            ViewModel = viewModel;
        }

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
        /// Реализует создание контакта.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter)
        {
            ViewModel.ApplyContact();
        }

        public event EventHandler CanExecuteChanged;

    }
}
