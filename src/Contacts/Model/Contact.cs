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
        /// Задает или возвращает имя контакта.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Задает или возвращает номер телефона контакта.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Задает или возвращает адрес электронной почты контакта.
        /// </summary>
        public string Email { get; set; }

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

    }
}
