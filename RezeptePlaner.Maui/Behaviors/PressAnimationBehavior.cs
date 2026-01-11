namespace RezeptePlaner.Maui.Behaviors;

/// <summary>
/// Behavior that adds a press animation effect to any visual element
/// </summary>
public class PressAnimationBehavior : Behavior<VisualElement>
{
    private VisualElement? _associatedObject;
    private TapGestureRecognizer? _tapGestureRecognizer;

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        _associatedObject = bindable;

        // Add tap gesture recognizer for animation
        _tapGestureRecognizer = new TapGestureRecognizer();
        _tapGestureRecognizer.Tapped += OnTapped;
        bindable.GestureRecognizers.Add(_tapGestureRecognizer);
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        base.OnDetachingFrom(bindable);
        
        // Remove the specific gesture recognizer we added
        if (_tapGestureRecognizer != null)
        {
            _tapGestureRecognizer.Tapped -= OnTapped;
            bindable.GestureRecognizers.Remove(_tapGestureRecognizer);
            _tapGestureRecognizer = null;
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
