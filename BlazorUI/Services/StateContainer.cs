namespace BlazorUI.Services;

public class StateContainer
{
    private bool _isLoggedIn = false;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            _isLoggedIn = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
}