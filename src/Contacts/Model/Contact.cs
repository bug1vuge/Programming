using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Contacts.Model
{
    /// <summary>
    /// Хранит поля и методы, описывающие контакт.
    /// </summary>
    internal class Contact : INotifyPropertyChanged
    {
        /// <summary>
        /// Хранит имя контакта.
        /// </summary>
        private string _name = string.Empty;

        /// <summary>
        /// Хранит номер телефона контакта.
        /// </summary>
        private string _number = string.Empty;

        /// <summary>
        /// Хранит email-адрес контакта.
        /// </summary>
        private string _email = string.Empty;

        /// <summary>
        /// Задает или возвращает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Задает или возвращает номер телефона контакта.
        /// </summary>
        public string Number
        {
            get => _number;
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Number));
                }
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
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        /// <summary>
        /// Конструктор по умолчанию класса <see cref="Contact"./>
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Конструктор с параметрами класса <see cref="Contact"./>
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        /// <param name="number">Номер телефона контакта.</param>
        /// <param name="email">Адрес электронной почты контакта.</param>        
        public Contact(string name, string number, string email)
        {
            Name = name;
            Number = number;
            Email = email;
        }

        /// <summary>
        /// Описывает событие при изменении значения.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, который вызывается при изменении значения свойства. 
        /// </summary>
        /// <param propertyName="propertyName">Название свойства.</param>
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
