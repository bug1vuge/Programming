using Contacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Contacts.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события PreviewTextInput для поля ввода номера телефона.
        /// Разрешает ввод только цифр, пробелов и символов +-().
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void PhoneNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        /// <summary>
        /// Обработчик события Pasting для поля ввода номера телефона.
        /// Предотвращает вставку недопустимых символов из буфера обмена.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Аргументы события.</param>
        private void PhoneNumber_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        /// <summary>
        /// Проверяет, содержит ли строка только допустимые символы для номера телефона.
        /// Разрешены только цифры, пробелы и символы +-().
        /// </summary>
        /// <param name="text">Текст для проверки.</param>
        /// <returns>true, если текст содержит только допустимые символы; иначе false.</returns>
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex(@"^[0-9\+\-\(\) ]+$");
            return regex.IsMatch(text);
        }
    }
}
