﻿using Contacts.Model;
using Contacts.Model.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает VM главного окна.
    /// </summary>
    internal class ModalVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Задает или возвращает объект класса <see cref="Contact"./>
        /// </summary>
        public Contact Contact{ get; set; }

        /// <summary>
        /// Содержит имя контакта.
        /// </summary>
        private string _name = "";

        /// <summary>
        /// Содержит номер телефона контакта.
        /// </summary>
        private string _phoneNumber = "";

        /// <summary>
        /// Содержит адрес электронной почты контакта.
        /// </summary>
        private string _email = "";


        /// <summary>
        /// Хранит экземпляр класса <see cref="ContactSerializer"./>
        /// </summary>
        private ContactSerializer _serializer = new ContactSerializer();

        /// <summary>
        /// Хранит значение, которое указывает на то, включена ли кнопка сохранения данных в файл или нет.
        /// </summary>
        private bool _isSaveButtonEnabled = false;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="SaveCommand"./>
        /// </summary>
        public SaveCommand SaveCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="LoadCommand"./>
        /// </summary>
        public LoadCommand LoadCommand { get; } = null;

        /// <summary>
        /// Задает или возваращет имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                UpdateSaveEnabled();
            }
        }

        /// <summary>
        /// Задает или возваращет номер телефона контакта.
        /// </summary>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
                UpdateSaveEnabled();
            }
        }

        /// <summary>
        /// Задает или возвращает адрес электронной почты контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                UpdateSaveEnabled();
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="ModalVM"./>
        /// </summary>
        public ModalVM()
        {
            SaveCommand = new SaveCommand(_serializer, this);
            LoadCommand = new LoadCommand(_serializer, this);
        }

        /// <summary>
        /// Метод, который инициализирует контакт данными.
        /// </summary>
        public void InitializeContact()
        {
            Contact = new Contact(Name, PhoneNumber, Email);
        }

        /// <summary>
        /// Метод, который очищает поля формы.
        /// </summary>
        public void ClearFields()
        {
            Name = "";
            PhoneNumber = "";
            Email = "";
        }

        /// <summary>
        /// Задает или возваращет значение, описывающее активна ли кнопка сохранения данных в файл или нет.
        /// </summary>
        public bool IsSaveEnabled
        {
            get => _isSaveButtonEnabled;
            set
            {
                _isSaveButtonEnabled = value;
                OnPropertyChanged(nameof(IsSaveEnabled));
            }
        }

        /// <summary>
        /// Метод для проверки заполненности полей.
        /// </summary>
        private void UpdateSaveEnabled()
        {
            IsSaveEnabled = !string.IsNullOrWhiteSpace(Name) &&
                            !string.IsNullOrWhiteSpace(PhoneNumber) &&
                            !string.IsNullOrWhiteSpace(Email);
        }

        /// <summary>
        /// Метод для заполнения значений полей данными из объекта Contact.
        /// </summary>
        /// <param name="contact">Объект Contact с загруженными данными.</param>
        public void PopulateFieldsFromContact(Contact contact)
        {
            if (contact != null)
            {
                Name = contact.Name;
                PhoneNumber = contact.Number;
                Email = contact.Email;
            }
        }

        /// <summary>
        /// Обработчик события на изменение значения текстового поля.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывается при изменении значения свойства объекта.
        /// </summary>
        /// <param name="propertyName">Поле, в котором произошло изменение.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.InitializeContact();
        }

    }
}
