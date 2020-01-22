using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Accretion.Core
{
    public class Command<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecutePredicate;
        private readonly List<string> _propertyNamesThatChangeCanExecute;

        public Command(Action<T> executeAction) : this(executeAction, null, null, null) { }

        public Command(Action<T> executeAction, InputGesture inputGesture) : this(executeAction, null, inputGesture, null) { }

        public Command(Action<T> executeAction, Func<T, bool> canExecutePredicate, InputGesture inputGesture, INotifyPropertyChanged observableObject, params string[] propertyNamesThatChangeCanExecute)
        {
            _executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));

            _canExecutePredicate = canExecutePredicate;
            _propertyNamesThatChangeCanExecute = (propertyNamesThatChangeCanExecute ?? Array.Empty<string>()).ToList();
            InputGesture = inputGesture;

            if (observableObject != null)
            {
                observableObject.PropertyChanged += RaiseCanExecuteChanged;
            }
        }

        public event EventHandler CanExecuteChanged;

        public InputGesture InputGesture { get; }

        public bool CanExecute(object parameter)
        {
            return _canExecutePredicate is null ? true : _canExecutePredicate((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeAction((T)parameter);
            }
        }

        private void RaiseCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_propertyNamesThatChangeCanExecute.Contains(e.PropertyName))
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public class Command : Command<object>
    {
        public Command(Action executeAction) : this(executeAction, null, null, null) { }

        public Command(Action executeAction, InputGesture inputGesture) : this(executeAction, null, inputGesture, null) { }

        public Command(Action executeAction, Func<bool> canExecutePredicate, InputGesture inputGesture, INotifyPropertyChanged observableObject, params string[] propertyNamesThatChangeCanExecute) : base(x => executeAction(), canExecutePredicate is null ? null : (Func<object, bool>)(x => canExecutePredicate()), inputGesture, observableObject, propertyNamesThatChangeCanExecute) { }
    }
}
