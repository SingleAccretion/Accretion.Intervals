using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Accretion.Core
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        private static readonly Cache<string, PropertyChangedEventArgs> _propertyChangedEventArgsCache = new Cache<string, PropertyChangedEventArgs>(x => new PropertyChangedEventArgs(x));

        private readonly Dictionary<string, Action> _simpleActionsOnPropertyChanged = new Dictionary<string, Action>();

        protected ObservableObject() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SubscribeToPropertyChanged(string propertyName, Action action)
        {
            if (_simpleActionsOnPropertyChanged.ContainsKey(propertyName))
            {
                _simpleActionsOnPropertyChanged[propertyName] += action;
            }
            else
            {
                _simpleActionsOnPropertyChanged.Add(propertyName, new Action(action));
            }
        }

        public void UnsubscribeFromPropertyChanged(string propertyName, Action action)
        {
            if (_simpleActionsOnPropertyChanged.ContainsKey(propertyName))
            {
                _simpleActionsOnPropertyChanged[propertyName] -= action;
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {            
            PropertyChanged?.Invoke(this, _propertyChangedEventArgsCache.RequestValue(propertyName));
            if (_simpleActionsOnPropertyChanged.ContainsKey(propertyName))
            {
                _simpleActionsOnPropertyChanged[propertyName].Invoke();
            }            
        }        
    }
}
