namespace RezeptePlaner.Maui.Behaviors;

/// <summary>
/// Behavior that adds a press animation effect to any visual element
/// </summary>
public class PressAnimationBehavior : Behavior<VisualElement>
{
    private VisualElement? _associatedObject;

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        _associatedObject = bindable;

        // Add tap gesture recognizer for animation
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnTapped;
        bindable.GestureRecognizers.Insert(0, tapGesture);
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        base.OnDetachingFrom(bindable);
        
        // Remove the gesture recognizer
        var tapGesture = bindable.GestureRecognizers.FirstOrDefault(g => g is TapGestureRecognizer);
        if (tapGesture != null)
        {
            ((TapGestureRecognizer)tapGesture).Tapped -= OnTapped;
        }
        
        _associatedObject = null;
    }

    private async void OnTapped(object? sender, EventArgs e)
    {
        if (_associatedObject == null) return;

        // Animate scale down
        await _associatedObject.ScaleTo(0.9, 50, Easing.CubicOut);
        
        // Animate scale back up
        await _associatedObject.ScaleTo(1.0, 50, Easing.CubicIn);
    }
}
