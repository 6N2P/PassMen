using Avalonia.Controls;
using ModelClient;
using PassMen.Desktop.ViewModels;
using System;

namespace PassMen.Desktop.Views;

public partial class AutorisationWindow : Window
{
    public AutorisationWindow()
    {
        InitializeComponent();
        var vm = new AutorisationViewModel();
        DataContext = vm;
        vm.UserReceived += GetUserFromvm;
    }

    public event EventHandler<DataEventArgs> UserReceived;

    private void GetUserFromvm(object? sender, DataEventArgs e)
    {       
        OnUserReceived(e);
    }

    protected virtual void OnUserReceived(DataEventArgs e)
    {
        UserReceived?.Invoke(this, e);
    }
}

