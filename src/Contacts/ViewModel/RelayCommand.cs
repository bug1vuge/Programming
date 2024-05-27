using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Реализация команды, позволяющая связывать действия в ViewModel с элементами управления в представлении.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие, выполняемое при вызове команды.
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Функция, которая определяет, может ли команда выполняться.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Делегат, выполняемый при выполнении команды.</param>
        /// <param name="canExecute">Делегат, определяющий, может ли команда выполняться.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="execute"/> равно null.</exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли команда выполняться.
        /// </summary>
        /// <param name="parameter">Параметр, передаваемый команде.</param>
        /// <returns>Значение <see langword="true"/>, если команда может выполняться; в противном случае - <see langword="false"/>.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр, передаваемый команде.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Происходит при изменении условий, определяющих, может ли команда выполняться.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
