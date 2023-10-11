using CULMS.Helpers;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CULMS.Behaviours
{
    public class PasswordValidationBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";//@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
        //@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,12}$";//@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,13}$";
        static readonly BindablePropertyKey IsInvalidPropertyKey = BindableProperty.CreateReadOnly("IsInvalid", typeof(bool), typeof(PasswordValidationBehavior), false);

        public static readonly BindableProperty IsInvalidProperty = IsInvalidPropertyKey.BindableProperty;
        public bool IsInvalid
        {
            get { return (bool)base.GetValue(IsInvalidProperty); }
            private set { base.SetValue(IsInvalidPropertyKey, value); }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsInvalid = !(Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = IsInvalid ? Color.Red : Color.Default;
            StringConstant.IsPasswordIsValid = IsInvalid ? false : true;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
