using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TestWPf.Helpers
{
    public class TextBoxHelper
    {


        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(MyPropertyProperty);
        }

        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string), typeof(TextBoxHelper), new PropertyMetadata(""));



        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }

        protected static void SetHasText(DependencyObject obj, bool value)
        {
            obj.SetValue(HasTextPropertyKey, value);
        }

        public static readonly DependencyProperty HasTextProperty;
        
        public static readonly DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("HasText",typeof(bool),typeof(TextBoxHelper),new PropertyMetadata(false));
        static TextBoxHelper()
        {
            HasTextProperty = HasTextPropertyKey.DependencyProperty;
        }
        public static bool GetMionitorTextChanged(DependencyObject obj)
        {
            return (bool)obj.GetValue(MionitorTextChangedProperty);
        }
        public static void SetMionitorTextChanged(DependencyObject obj, bool value)
        {
            obj.SetValue(MionitorTextChangedProperty, value);
        }
        public static readonly DependencyProperty MionitorTextChangedProperty =
            DependencyProperty.RegisterAttached("MionitorTextChanged", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false,MionitorTextPropertyChanged));
        private static void MionitorTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox box)
                throw new NotSupportedException();
            if ((bool)e.NewValue)
            {
                box.TextChanged +=Box_TextChanged;
                SetHasText(box!, !string.IsNullOrEmpty(box!.Text));

            }
            else
                box.TextChanged -=Box_TextChanged;
        }
        private static void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = sender as TextBox;
            SetHasText(box!,!string.IsNullOrEmpty(box!.Text));
        }
    }
}
