using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using House.Annotations;
using House.Models;
using Xamarin.Forms;

namespace House.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                
            }
        }

        public  DetailPageViewModel()
        {
            _isBusy = true;
            //GetProducts(id);
            //_isBusy = false;
            
        }

        async void GetProducts(int id)
        {
            
            ProductDataRequest objRequest = new ProductDataRequest {Id = id};
            await ServiceHandler.PostData<ProductList, ProductDataRequest>("GetProductsByCategory", HttpMethod.Post, objRequest).ContinueWith((t) =>
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
                        _products = new ObservableCollection<Product>(data.Productes);
                        _isBusy = false;
                        OnPropertyChanged(nameof(Products));
                        OnPropertyChanged(nameof(IsBusy));
                    }
                    
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
