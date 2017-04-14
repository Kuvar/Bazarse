using House.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Icu.Text;
using House.Models;
using Java.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace House.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDetailPage : ContentPage
    {
        public MenuDetailPage()
        {
            InitializeComponent();
            //this.BindingContext = new DetailPageViewModel(3);
        }
    }
}
