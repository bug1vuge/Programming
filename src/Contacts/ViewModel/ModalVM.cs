using Contacts.Model;
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
        /// Содержит экземпляр класса <see cref="Contact"./>
        /// </summary>
        private Contact _contact = null;



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
        /// Содержит список контактов.
        /// </summary>
        private List<Contact> Contacts { get; set; } = new List<Contact>();


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
            }
        }

        /// <summary>
        /// Задает или возвращает объект класса <see cref="Contact"./>
        /// </summary>
        public Contact Contact
        {
            get => _contact;
            set => _contact = value;
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
            _contact = new Contact(Name, PhoneNumber, Email);
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            this.InitializeContact();
        }

    }
}
