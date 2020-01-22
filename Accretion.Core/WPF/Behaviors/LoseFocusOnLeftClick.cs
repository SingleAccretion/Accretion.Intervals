using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Accretion.Core
{
    public class LoseFocusOnLeftClick : Behavior<FrameworkElement>
    {
        private readonly MouseBinding _leftClick;
        private readonly Label _emptyControl = new Label() { Focusable = true, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

        public LoseFocusOnLeftClick()
        {
            _leftClick = new MouseBinding(new Command(LoseFocus), new MouseGesture(MouseAction.LeftClick));
        }

        protected override void OnAttached()
        {
            AssociatedObject.InputBindings.Add(_leftClick);
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }        

        protected override void OnDetaching()
        {
            AssociatedObject.InputBindings.Remove(_leftClick);
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            AttachEmptyControl();
            LoseFocus();
        }

        private void AttachEmptyControl()
        {            
            DependencyObject currentElement = AssociatedObject;
            while (!(currentElement is Panel))
            {
                currentElement = VisualTreeHelper.GetChild(currentElement, 0);
            }
            
            ((Panel)currentElement).Children.Add(_emptyControl);            
        }

        private void LoseFocus()
        {            
            _emptyControl.Focus();
        }
    }
}
