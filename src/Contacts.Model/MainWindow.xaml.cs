using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace Contacts.Model
{
    /// <summary>
    /// Хранит поля и методы, описывающие контакт.
    /// </summary>
    public class Contact : INotifyPropertyChanged
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

    /// <summary>
    /// Описывает сериализацию контактов.
    /// </summary>
    public class ContactSerializer
    {
        /// <summary>
        /// Задает или возвращает путь к файлу.
        /// </summary>
        public string FilePath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contacts", "contacts.json");

        /// <summary>
        /// Сохраняет коллекцию контактов в файл.
        /// </summary>
        /// <param name="contacts">Коллекция объектов <see cref="Contact"/>.</param>
        public void SaveContacts(IEnumerable<Contact> contacts)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(FilePath));

            string json = JsonConvert.SerializeObject(contacts);
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Загружает коллекцию контактов из файла.
        /// </summary>
        /// <returns>Коллекция контактов.</returns>
        public IEnumerable<Contact> LoadContacts()
        {
            if (!File.Exists(FilePath))
            {
                return Enumerable.Empty<Contact>();
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }
}
