using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestWPf.Constrolls
{
    public class CustomerTextBox:TextBox
    {
        public bool IsLighited
        {
            get { return (bool)GetValue(IsLighitedProperty); }
            set { SetValue(IsLighitedProperty, value);}
        }
        public static readonly DependencyProperty IsLighitedProperty = DependencyProperty.Register(
            nameof(IsLighited),
            typeof(bool),
            typeof(TextBox),
            new PropertyMetadata(false));
        public bool HasText => (bool)GetValue(HasTextProperty);
        public static readonly DependencyProperty HasTextProperty;
        public static readonly DependencyPropertyKey HasTextPropertyKey;
        static CustomerTextBox()
        {
            HasTextPropertyKey = DependencyProperty.RegisterReadOnly("HasText", typeof(bool), typeof(CustomerTextBox), new PropertyMetadata(false));
            HasTextProperty = HasTextPropertyKey.DependencyProperty;
        }
    }
}
