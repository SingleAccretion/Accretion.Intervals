using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Accretion.Core
{    
    public class DependencyPropertySubscriber : DependencyObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Dependency properties naming conventions")]
        private static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(DependencyPropertySubscriber), new PropertyMetadata(null, ValueChanged));

        private readonly PropertyChangedCallback _handler;

        public DependencyPropertySubscriber(DependencyObject dependencyObject, DependencyProperty dependencyProperty, PropertyChangedCallback handler)
        {
            if (dependencyObject is null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (dependencyProperty is null)
            {
                throw new ArgumentNullException(nameof(dependencyProperty));
            }

            _handler = handler ?? throw new ArgumentNullException(nameof(handler));

            var binding = new Binding() { Path = new PropertyPath(dependencyProperty), Source = dependencyObject, Mode = BindingMode.OneWay };
            BindingOperations.SetBinding(this, ValueProperty, binding);
        }        

        public void Unsubscribe()
        {
            BindingOperations.ClearBinding(this, ValueProperty);
        }

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DependencyPropertySubscriber)d)._handler(d, e);
        }
    }
}