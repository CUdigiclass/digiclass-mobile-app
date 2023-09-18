using System.ComponentModel;
using Xamarin.Forms;

namespace CULMS.Behaviours
{
    public class FadingBehavior : Behavior<VisualElement>
    {
        public uint FadeTime { get; set; } = 500;

        protected override void OnAttachedTo(VisualElement bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.PropertyChanged += VisibilityMightHaveChanged;
        }

        protected override void OnDetachingFrom(VisualElement bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.PropertyChanged -= VisibilityMightHaveChanged;
        }

        private void VisibilityMightHaveChanged(object sender, PropertyChangedEventArgs args)
        {
            var element = sender as VisualElement;
            if (args.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
            {
                var fadeToValue = element.IsVisible ? 1 : 0;
                element.Opacity = -fadeToValue + 1;
                element.FadeTo(fadeToValue, FadeTime);
            }
        }
    }
}
