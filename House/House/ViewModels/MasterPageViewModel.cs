using House.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using House.Pages;
using Xamarin.Forms;

namespace House.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        public ObservableCollection<Category> MenuList { get; set; }

        private Category _ItemSelected;
        public Category OnRowItemSelected
        {
            get
            {
                return _ItemSelected;
            }
            set
            {
                if (_ItemSelected != value)
                {
                    _ItemSelected = value;
                    OnRowSelection(value);
                }
            }
        }

        public MasterPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            //this.MenuList = new ObservableCollection<Category>
            //{
            //    new Category { Id=1, Title="Kitchen Grocery", IconSource="flour.png" },
            //    new Category { Id=2, Title="Fruits", IconSource="fruit.png" },
            //    new Category { Id=3, Title="Vegetable", IconSource="vegetable.png" },
            //    new Category { Id=4, Title="Body Care", IconSource="cosmetics.png" },
            //    new Category { Id=5, Title="Stationary", IconSource="stationary.png" },
            //    new Category { Id=6, Title="Electronic", IconSource="electronic.png" }
            //};
            GetMenu();

        }

        public async void GetMenu()
        {
            await ServiceHandler.PostData<CategoryList, String>("GetAllCategory", HttpMethod.Post, string.Empty).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    Application.Current.MainPage.DisplayAlert("", Constants.SomethingWentWrong, "OK");
                }
                else
                {
                    if (t.Result != null)
                    {
                        var data = t.Result;
                        MenuList = new ObservableCollection<Category>(data.Categories);
                        OnPropertyChanged("MenuList");
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void OnRowSelection(Category data)
        {
            var page = new Productes(data.Id);
            //((MainPage)Application.Current.MainPage).NavigateToPage(page);
            Navigation.PushModalAsync(new Productes(data.Id));
        }
        
    }
}
