using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using House.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace House.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductesPage : ContentPage
    {
        public ProductesPage(int id)
        {
            InitializeComponent();
            //this.BindingContext = new DetailPageViewModel(id);
        }
    }
}
