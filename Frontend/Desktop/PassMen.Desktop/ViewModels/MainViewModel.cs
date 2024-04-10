using ModelClient;
using PassMen.Desktop.Views;
using ReactiveUI;
using System;

namespace PassMen.Desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private AutorisationWindow _autorisationWindow;
    public string Greeting => "Welcome to Avalonia!";
    private UserPassDto _user;
    public UserPassDto User
    {
        get => _user;
        set=> this.RaiseAndSetIfChanged(ref _user, value);
        
    }

    public void ShowAutorisationWindow()
    {
        _autorisationWindow = new AutorisationWindow();
        _autorisationWindow.UserReceived += GetUser;
        _autorisationWindow.Show();
    }

    private void GetUser(object? sender, DataEventArgs e)
    {
        if (e.Data != null)
        {
            User = (UserPassDto)e.Data;
            _autorisationWindow.Close();
           // _autorisationWindow = null;
        }

    }
}
