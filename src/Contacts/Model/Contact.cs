using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Model
{
    /// <summary>
    /// Хранит поля и методы, описывающие контакт.
    /// </summary>
    internal class Contact
    {

        /// <summary>
        /// Хранит имя контакта.
        /// </summary>
        private string _name = "";

        /// <summary>
        /// Хранит номер телефона контакта.
        /// </summary>
        private string _number = "";

        /// <summary>
        /// Хранит адрес электронной почты контакта
        /// </summary>
        private string _email = "";

        /// <summary>
        /// Задает или возвращает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Задает или возвращает номер телефона контакта.
        /// </summary>
        public string Number
        {
            get => _number;
            set => _number = value;
        }

        /// <summary>
        /// Задает или возвращает адрес электронной почты контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set => _email = value;
        }


        /// <summary>
        /// Конструктор по умолчанию класса <see cref="Contact"./>
        /// </summary>
        public Contact()
        {

        }

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

    }
}
