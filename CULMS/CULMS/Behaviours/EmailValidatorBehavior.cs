using CULMS.Helpers;
using System;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CULMS.Behaviours
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
            bindable.TextChanged += Bindable_TextChanged;
            base.OnAttachedTo(bindable);
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                var Email = (Entry)sender;
                bool IsValid = false;
                if (Email.Text != null)
                {

                    var userEmail = (Entry)sender;
                    IsValid = Regex.IsMatch(userEmail.Text, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    ((Entry)sender).TextColor = IsValid ? Color.Black : Color.Red;

                }
                if (IsValid)
                {
                    StringConstant.IsValidEmail = true;
                    Preferences.Set("ValidEmail", StringConstant.IsValidEmail);
                }
                else
                {
                    StringConstant.IsValidEmail = false;
                    Preferences.Set("ValidEmail", StringConstant.IsValidEmail);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
            bindable.TextChanged -= Bindable_TextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
