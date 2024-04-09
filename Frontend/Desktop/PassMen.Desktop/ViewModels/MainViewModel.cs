using PassMen.Desktop.Views;

namespace PassMen.Desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private AutorisationWindow _autorisationWindow;
    public string Greeting => "Welcome to Avalonia!";

    public void ShowAutorisationWindow()
    {
        _autorisationWindow = new AutorisationWindow();
        _autorisationWindow.Show();
    }
}
