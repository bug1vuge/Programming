using Contacts.Model;
using Contacts.Model.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private string _name = String.Empty;

        /// <summary>
        /// Содержит номер телефона контакта.
        /// </summary>
        private string _phoneNumber = String.Empty;

        /// <summary>
        /// Содержит адрес электронной почты контакта.
        /// </summary>
        private string _email = String.Empty;

        /// <summary>
        /// Содержит значение для поиска.
        /// </summary>
        private string _searchText = String.Empty;

        /// <summary>
        /// Содержит выбранный контакт.
        /// </summary>
        private Contact _selectedContact = new Contact();

        /// <summary>
        /// Хранит экземпляр объекта <see cref="AddCommand"./>
        /// </summary>
        public AddCommand AddCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="ApplyCommand"./>
        /// </summary>
        public ApplyCommand ApplyCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="EditCommand"./>
        /// </summary>
        public EditCommand EditCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="RemoveCommand"./>
        /// </summary>
        public RemoveCommand RemoveCommand { get; } = null;

        /// <summary>
        /// Содержит список контактов.
        /// </summary>
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Содержит список найденных контактов.
        /// </summary>
        private ObservableCollection<Contact> _filteredContacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Задает или возвращает экземпляр класса <see cref="ContactSerializer"/>
        /// </summary>
        public ContactSerializer ContactSerializer {  get; set; } = new ContactSerializer();

        /// <summary>
        /// Содержит флаг видимости кнопки "Apply".
        /// </summary>
        private bool _isApplyButtonVisible = false;

        /// <summary>
        /// Содержит флаг доступности полей.
        /// </summary>
        private bool _isReadOnly = true;

        /// <summary>
        /// Содержит флаг, который указывает выбран ли другой контакт при создании нового контакта.
        /// </summary>
        private bool _isCreatingNewContact = true;

        /// <summary>
        /// Хранит флаг доступности кнопки добавления контакта.
        /// </summary>
        private bool _isAddButtonEnabled = true;

        /// <summary>
        /// Хранит флаг доступности редактирования контакта.
        /// </summary>
        private bool _isEditButtonEnabled = false;

        /// <summary>
        /// Хранит флаг доступности удаления контакта.
        /// </summary>
        private bool _isRemoveButtonEnabled = false;


        /// <summary>
        /// Задает или возвращает имя контакта.
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
        /// Задает или возвращает значение для поиска.
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                UpdateFilteredContacts();
            }
        }

        /// <summary>
        /// Задает или возвращает флаг видимости кнопки "ApplyButton"
        /// </summary>
        public bool IsApplyButtonVisible
        {
            get => _isApplyButtonVisible;
            set
            {
                if (_isApplyButtonVisible != value)
                {
                    _isApplyButtonVisible = value;
                    OnPropertyChanged(nameof(IsApplyButtonVisible));
                }
            }
        }

        /// <summary>
        /// Задает или возвращает флаг доступности добавления контакта.
        /// </summary>
        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set
            {
                _isAddButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddButtonEnabled));
            }
        }

        /// <summary>
        /// Задает или возвращает флаг доступности редактирования контакта.
        /// </summary>
        public bool IsEditButtonEnabled
        {
            get => _isEditButtonEnabled;
            set
            {
                _isEditButtonEnabled = value;
                OnPropertyChanged(nameof(IsEditButtonEnabled));
            }
        }

        /// <summary>
        /// Задает или возвращает флаг доступности удаления контакта.
        /// </summary>
        public bool IsRemoveButtonEnabled
        {
            get => _isRemoveButtonEnabled;
            set
            {
                _isRemoveButtonEnabled = value;
                OnPropertyChanged(nameof(IsRemoveButtonEnabled));
            }
        }

        /// <summary>
        /// Задает или возвращает флаг, указывающий на создание нового контакта.
        /// </summary>
        public bool IsCreatingNewContact
        {
            get => _isCreatingNewContact;
            set => _isCreatingNewContact = value;
        }

        /// <summary>
        /// Задает или возвращает выбранный контакт.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (IsCreatingNewContact && value != null)
                {
                    CancelNewContact();
                }

                _selectedContact = value;
                
                if (value != null)
                {
                    Name = value.Name;
                    PhoneNumber = value.Number;
                    Email = value.Email;
                    IsReadOnly = true;
                    IsApplyButtonVisible = false;

                    UpdateAddButtonEnabled(true);
                    UpdateEditButtonEnabled(SelectedContact);
                    UpdateRemoveButtonEnabled(SelectedContact);
                }

                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        /// <summary>
        /// Задает или возвращает флаг заполненности полей.
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        /// <summary>
        /// Задает или возвращает список контактов класса <see cref="Contact"./>
        /// </summary>
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        /// <summary>
        /// Задает или возвращает список найденных контактов.
        /// </summary>
        public ObservableCollection<Contact> FilteredContacts
        {
            get => _filteredContacts;
            set
            {
                _filteredContacts = value;
                OnPropertyChanged("FilteredContacts");
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="ModalVM"./>
        /// </summary>
        public ModalVM()
        {
            AddCommand = new AddCommand(this);
            ApplyCommand = new ApplyCommand(this);
            EditCommand = new EditCommand(this);
            RemoveCommand = new RemoveCommand(this);

            _contacts = new ObservableCollection<Contact>
            {
                new Contact("John Doe", "123-456-7890", "john@example.com"),
                new Contact("Jane Smith", "456-789-0123", "jane@example.com"),
                new Contact("Mike Johnson", "789-012-3456", "mike@example.com")
            };

            _filteredContacts = new ObservableCollection<Contact>(_contacts); ;
        }

        /// <summary>
        /// Задает или возвращает флаг доступности кнопки добавления контакта.
        /// </summary>
        /// <param name="isAddEnabled">Значение доступности.</param>
        private void UpdateAddButtonEnabled(bool isAddEnabled)
        {
            IsAddButtonEnabled = isAddEnabled;
        }

        /// <summary>
        /// Задает или возвращает флаг доступности кнопки редактирования контакта.
        /// </summary>
        /// <param name="selectedContact">Объект выбранного контакта.</param>
        private void UpdateEditButtonEnabled(Contact selectedContact)
        {
            IsEditButtonEnabled = selectedContact != null && Contacts.Any();
        }

        /// <summary>
        /// Задает или возвращает флаг доступности кнопки удаления контакта.
        /// </summary>
        /// <param name="selectedContact">Объект выбранного контакта.</param>
        private void UpdateRemoveButtonEnabled(Contact selectedContact)
        {
            IsRemoveButtonEnabled = selectedContact != null && Contacts.Any();
        }

        /// <summary>
        /// Обновляет список контактов.
        /// </summary>
        private void UpdateFilteredContacts()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                FilteredContacts = new ObservableCollection<Contact>(Contacts);
            }
            else
            {
                FilteredContacts = new ObservableCollection<Contact>(
                    Contacts.Where(c => c.Name.ToLower().Contains(SearchText.ToLower())));
            }
        }

        /// <summary>
        /// Логика создания нового контакта.
        /// </summary>
        public void AddContact()
        {
            Name = "";
            PhoneNumber = "";
            Email = "";

            SelectedContact = null;

            IsApplyButtonVisible = true;
            IsReadOnly = false;

            UpdateAddButtonEnabled(false);
            UpdateEditButtonEnabled(null);
            UpdateRemoveButtonEnabled(null);
        }

        /// <summary>
        /// Логика применения изменений для контакта.
        /// </summary>
        public void ApplyContact()
        {
            if (SelectedContact != null)
            {
                SelectedContact.Name = Name;
                SelectedContact.Number = PhoneNumber;
                SelectedContact.Email = Email;

                IsReadOnly = true;
                IsApplyButtonVisible = false;

                UpdateAddButtonEnabled(true);
                UpdateEditButtonEnabled(SelectedContact);
                UpdateRemoveButtonEnabled(SelectedContact);

                OnPropertyChanged(nameof(SelectedContact));
            }
            else
            {
                if (Name != "" && PhoneNumber != "" && Email != "") 
                {
                    Contact newContact = new Contact(Name, PhoneNumber, Email);

                    Contacts.Add(newContact);
                    FilteredContacts.Add(newContact);

                    SelectedContact = newContact;
                    IsApplyButtonVisible = false;

                    IsReadOnly = true;
                    IsApplyButtonVisible = false;

                    ContactSerializer.SaveContacts(Contacts);
                    OnPropertyChanged(nameof(FilteredContacts));                
                } 
                else
                {
                    IsReadOnly = false;
                    IsApplyButtonVisible = true;

                    MessageBox.Show("Заполните поля!");
                }
            }
        }

        /// <summary>
        /// Логика редактирования контакта.
        /// </summary>
        public void EditContact()
        {
            IsApplyButtonVisible = true;
            IsReadOnly = false;

            UpdateAddButtonEnabled(false);
            UpdateEditButtonEnabled(null);
            UpdateRemoveButtonEnabled(null);
        }

        /// <summary>
        /// Логика удаления контакта.
        /// </summary>
        public void RemoveContact()
        {
            if (SelectedContact != null)
            {
                int index = Contacts.IndexOf(SelectedContact);

                Contacts.Remove(SelectedContact);
                FilteredContacts.Remove(SelectedContact);

                if (Contacts.Count == 0)
                {
                    SelectedContact = null;
                    Name = string.Empty;
                    PhoneNumber = string.Empty;
                    Email = string.Empty;
                }
                else
                {
                    if (index < Contacts.Count)
                    {
                        SelectedContact = Contacts[index];
                    }
                    else
                    {
                        SelectedContact = Contacts[Contacts.Count - 1];
                    }
                }

                OnPropertyChanged(nameof(FilteredContacts));
                OnPropertyChanged(nameof(SelectedContact));

                ContactSerializer.SaveContacts(Contacts);
            }
        }

        /// <summary>
        /// Логика сброса данных.
        /// </summary>
        private void CancelNewContact()
        {
            Name = "";
            PhoneNumber = "";
            Email = "";
            IsReadOnly = true;
            IsApplyButtonVisible = false;
            IsCreatingNewContact = false;
        }

        /// <summary>
        /// Описывает событие при изменении значения.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, который вызывается при изменении значения свойства. 
        /// </summary>
        /// <param name="name">Название свойства.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
