﻿using Contacts.Model;
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
    /// Описывает комманду для загрузки контакта.
    /// </summary>
    internal class LoadCommand : ICommand
    {
        /// <summary>
        /// Содержит экземпляр класса <see cref="ContactSerializer"./>
        /// </summary>
        private ContactSerializer _serializer;

        /// <summary>
        /// Содержит экземпляр класса <see cref="ModalVM"./>
        /// </summary>
        private ModalVM _viewModel;

        /// <summary>
        /// Задает или возвараешь экземпляр класса <see cref="ContactSerializer"./>
        /// </summary>
        public ContactSerializer Serializer
        {
            get => _serializer;
            set => _serializer = value;
        }

        /// <summary>
        /// Задает или возвараешь экземпляр класса <see cref="ModalVM"./>
        /// </summary>
        public ModalVM ViewModel
        {
            get => _viewModel;
            set => _viewModel = value;
        }


        /// <summary>
        /// Констуктор с параметрами для класса <see cref="LoadCommand"./>
        /// </summary>
        /// <param name="serializer">Объект класса <see cref="ContactSerializer"./></param>
        /// <param name="viewModel">Объект класса <see cref="ModalVM".</param>
        public LoadCommand(ContactSerializer serializer, ModalVM viewModel)
        {
            Serializer = serializer;
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
        /// Реализует загрузку данных контакта из файла.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void Execute(object parameter)
        {
            ViewModel.Contact = Serializer.LoadContact();
            ViewModel.PopulateFieldsFromContact(ViewModel.Contact);
        }

        public event EventHandler CanExecuteChanged;

    }
}