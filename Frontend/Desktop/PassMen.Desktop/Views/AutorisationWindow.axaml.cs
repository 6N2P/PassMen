using Avalonia.Controls;
using PassMen.Desktop.ViewModels;

namespace PassMen.Desktop.Views;

    public partial class AutorisationWindow : Window
    {
        public AutorisationWindow()
        {
            InitializeComponent();
            DataContext = new AutorisationViewModel();
        }
    }

