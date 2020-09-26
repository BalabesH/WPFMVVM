using System;
using System.Windows.Input;

namespace WPFMVVM
{
    public class RelayCommand : ICommand
    /*ICommand, благодаря чему с помощью подобных команды мы сможем направлять вызовы к ViewModel.
     * Ключевым здесь является метод Execute(), который получает параметр и выполняет действие, 
     * переданное через конструктор команды.*/
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged //вызывается при изменении условий, указывающий, может ли команда выполняться.
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) //определяет, может ли команда выполняться
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public void Execute(object parameter) //выполняет логику команды
        {
            this.execute(parameter);
        }


    }
}
