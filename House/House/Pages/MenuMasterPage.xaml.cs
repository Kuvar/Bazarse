using House.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace House.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuMasterPage : ContentPage
    {
        public MenuMasterPage()
        {
            InitializeComponent();
            this.BindingContext = new MasterPageViewModel(Navigation);
        }
    }
}
