using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json; 

namespace Contacts.Model.Services
{
    /// <summary>
    /// Описывает сериализацию класса <see cref="Contact"./>
    /// </summary>
    internal class ContactSerializer
    {
        /// <summary>
        /// Задает или возвращает путь к файлу.
        /// </summary>
        public string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contacts", "contacts.json");

        /// <summary>
        /// Сохраняет данные контакта в файл.
        /// </summary>
        /// <param name="contact">Экземпляр объекта <see cref="Contact"./></param>
        public void SaveContact(Contact contact)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            string json = JsonConvert.SerializeObject(contact);
            File.WriteAllText(FilePath, json);

            MessageBox.Show("Данных успешно сохранены!");
        }

        /// <summary>
        /// Загружает данные контакта из файла.
        /// </summary>
        /// <returns>Данные контакта.</returns>
        public Contact LoadContact()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<Contact>(json);
        }
    }
}
