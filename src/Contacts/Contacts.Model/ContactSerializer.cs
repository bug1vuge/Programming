using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Contacts.Model
{
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
