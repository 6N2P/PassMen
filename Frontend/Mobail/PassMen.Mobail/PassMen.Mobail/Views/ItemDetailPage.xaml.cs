using PassMen.Mobail.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PassMen.Mobail.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}