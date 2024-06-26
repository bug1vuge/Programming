﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Model;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Описывает VM главного окна.
    /// </summary>
    public class ModalVM : ObservableObject, IDataErrorInfo
    {
        /// <summary>
        /// Задает или возвращает объект класса <see cref="Contact"./>
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        /// Содержит имя контакта.
        /// </summary>
        private string _name = string.Empty;

        /// <summary>
        /// Содержит номер телефона контакта.
        /// </summary>
        private string _phoneNumber = string.Empty;

        /// <summary>
        /// Содержит адрес электронной почты контакта.
        /// </summary>
        private string _email = string.Empty;

        /// <summary>
        /// Содержит значение для поиска.
        /// </summary>
        private string _searchText = string.Empty;

        /// <summary>
        /// Содержит сообщение об ошибке валидации.
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="IRelayCommand "./>
        /// </summary>
        public IRelayCommand AddCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="IRelayCommand "./>
        /// </summary>
        public IRelayCommand ApplyCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="IRelayCommand "./>
        /// </summary>
        public IRelayCommand EditCommand { get; } = null;

        /// <summary>
        /// Хранит экземпляр объекта <see cref="IRelayCommand "./>
        /// </summary>
        public IRelayCommand RemoveCommand { get; } = null;

        /// <summary>
        /// Содержит список контактов.
        /// </summary>
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Содержит список отфильтрованных контактов.
        /// </summary>
        private ObservableCollection<Contact> _filteredContacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Содержит выбранный контакт.
        /// </summary>
        private Contact _selectedContact = new Contact();

        /// <summary>
        /// Содержит экземпляр класса <see cref="ContactSerializer"./>
        /// </summary>
        public ContactSerializer ContactSerializer { get; set; } = new ContactSerializer();

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
        /// Хранит флаг доступности применения изменений контакта.
        /// </summary>
        private bool _isApplyButtonEnabled = false;

        /// <summary>
        /// Содержит флаг наличия ошибок валидации.
        /// </summary>
        private bool _isHasValidationErrors = true;

        /// <summary>
        /// Задает или возвращает флаг, указывающий на ошибки валидации.
        /// </summary>
        public bool IsHasValidationErrors
        {
            get => _isHasValidationErrors;
            set
            {
                if (_isHasValidationErrors != value)
                {
                    _isHasValidationErrors = value;
                    OnPropertyChanged(nameof(IsApplyButtonEnabled));
                }
            }
        }

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
                UpdateApplyButtonEnabled();
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
                UpdateApplyButtonEnabled();
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
                UpdateApplyButtonEnabled();
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
        /// Возвращает флаг, указывающий доступна ли кнопки применения или нет.
        /// </summary>
        public bool IsApplyButtonEnabled => !IsHasValidationErrors;

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
            var loadedContacts = ContactSerializer.LoadContacts();

            AddCommand = new RelayCommand<object>(_ => AddContact(), _ => IsAddButtonEnabled);
            EditCommand = new RelayCommand<object>(_ => EditContact(), _ => IsEditButtonEnabled);
            RemoveCommand = new RelayCommand<object>(_ => RemoveContact(), _ => IsRemoveButtonEnabled);
            ApplyCommand = new RelayCommand<object>(_ => ApplyContact());

            Contacts = new ObservableCollection<Contact>
            {
                new Contact("John Doe", "123-456-7890", "john@example.com"),
                new Contact("Jane Smith", "456-789-0123", "jane@example.com"),
                new Contact("Mike Johnson", "789-012-3456", "mike@example.com")
            };

            FilteredContacts = new ObservableCollection<Contact>(Contacts);

            if (loadedContacts != null)
            {
                Contacts = new ObservableCollection<Contact>(loadedContacts);
                FilteredContacts = new ObservableCollection<Contact>(Contacts);
            }

            if (Contacts.Any())
            {
                SelectedContact = Contacts[0];
            }
        }


        /// <summary>
        /// Возвращает сообщение об ошибке.
        /// </summary>
        /// <param name="columnName">Имя свойства.</param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                Error = null;

                switch (columnName)
                {
                    case nameof(Name):

                        if (Name.Length > 100)
                        {
                            Error = "Name must be non-empty and not longer than 100 characters.";
                        }

                        break;

                    case nameof(PhoneNumber):

                        if (PhoneNumber.Length > 100 || !System.Text.RegularExpressions.Regex.IsMatch(PhoneNumber, @"^[0-9\+\-\(\) ]+$") && !string.IsNullOrEmpty(PhoneNumber))
                        {
                            Error = "PhoneNumber must be non-empty, not longer than 100 characters, and can only contain digits and the symbols +-()";
                        }

                        break;

                    case nameof(Email):

                        if (Email.Length > 100 || !Email.Contains("@") && !string.IsNullOrEmpty(PhoneNumber))
                        {
                            Error = "Email must be non-empty, not longer than 100 characters, and must contain '@'";
                        }

                        break;

                }

                return Error;
            }
        }


        /// <summary>
        /// Задает или возвращает флаг доступности кнопки добавления контакта.
        /// </summary>
        private void UpdateApplyButtonEnabled()
        {
            IsHasValidationErrors = !string.IsNullOrEmpty(this[nameof(Name)]) ||
                                    !string.IsNullOrEmpty(this[nameof(PhoneNumber)]) ||
                                    !string.IsNullOrEmpty(this[nameof(Email)]);
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
            Name = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;

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
                if (Name != string.Empty && PhoneNumber != string.Empty && Email != string.Empty)
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

                    UpdateEditButtonEnabled(null);
                    UpdateRemoveButtonEnabled(null);
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
            Name = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            IsReadOnly = true;
            IsApplyButtonVisible = false;
            IsCreatingNewContact = false;
        }
    }
}
