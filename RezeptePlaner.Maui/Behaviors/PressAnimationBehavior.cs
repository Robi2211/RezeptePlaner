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

        // Subscribe to existing gesture recognizers
        foreach (var gesture in bindable.GestureRecognizers.OfType<TapGestureRecognizer>())
        {
            gesture.Tapped += OnElementTapped;
        }

        // Listen for changes to gesture recognizers collection
        bindable.GestureRecognizers.CollectionChanged += OnGestureRecognizersChanged;
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        base.OnDetachingFrom(bindable);
        
        // Unsubscribe from gesture recognizers
        foreach (var gesture in bindable.GestureRecognizers.OfType<TapGestureRecognizer>())
        {
            gesture.Tapped -= OnElementTapped;
        }

        bindable.GestureRecognizers.CollectionChanged -= OnGestureRecognizersChanged;
        _associatedObject = null;
    }

    private void OnGestureRecognizersChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Subscribe to any newly added tap gesture recognizers
        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems.OfType<TapGestureRecognizer>())
            {
                item.Tapped += OnElementTapped;
            }
        }

        // Unsubscribe from removed tap gesture recognizers
        if (e.OldItems != null)
        {
            foreach (var item in e.OldItems.OfType<TapGestureRecognizer>())
            {
                item.Tapped -= OnElementTapped;
            }
        }
    }

    private async void OnElementTapped(object? sender, EventArgs e)
    {
        if (_associatedObject == null) return;

        // Animate scale down
        await _associatedObject.ScaleTo(0.9, 50, Easing.CubicOut);
        
        // Animate scale back up
        await _associatedObject.ScaleTo(1.0, 50, Easing.CubicIn);
    }
}
