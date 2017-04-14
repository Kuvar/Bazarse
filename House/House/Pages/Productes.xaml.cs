using House.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using House.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace House.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Productes : ContentPage
    {
        private List<Product> _objList;
        private DetailPageViewModel _viewModel;
        public Productes(int id)
        {
            _viewModel = new DetailPageViewModel();
            InitializeComponent();             
            this.BindingContext = _viewModel;    
            GetData(id);
        }

        async void GetData(int id)
        {
            ProductDataRequest objRequest = new ProductDataRequest { Id = id };
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
                        _objList = new List<Product>(data.Productes);
                        BindData();
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void BindData()
        {
            int w = App.ScreenWidth;
            int a = _objList.Count / 2;
            int b = _objList.Count % 2;
            int c = b > 0 ? (a + 1) : a;

            for (int i = 0; i < c; i++)
            {
                gridLayout.RowDefinitions.Add(new RowDefinition());
            }

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            //gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, eve) => {
                var imageSender = (Image)sender;
                var source = imageSender.Source as FileImageSource;
                imageSender.Source = source != null && source.File == "uncheck.png" ? "check.png" : "uncheck.png";
            };

            var productIndex = 0;
            for (int rowIndex = 0; rowIndex < c; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                {
                    if (productIndex >= _objList.Count)
                    {
                        break;
                    }
                    var product = _objList[productIndex];
                    productIndex += 1;

                    var stack = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 0,
                        Padding = new Thickness(5),
                        WidthRequest = 160,
                        Children =
                        {
                            new Image
                            {
                                Source =
                                    Device.OnPlatform(iOS: ImageSource.FromFile("Images/" + product.IconSource),
                                        Android: ImageSource.FromFile(product.IconSource),
                                        WinPhone: ImageSource.FromFile("Images/" + product.IconSource)),
                                HeightRequest = 160,
                                WidthRequest = 160
                            },
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Spacing = 5,
                                BackgroundColor = Color.FromHex("#335BFF"),
                                Padding = new Thickness(10,0,8,0),
                                HeightRequest = 50,
                                Children =
                                {
                                    new StackLayout
                                    {
                                        Orientation = StackOrientation.Vertical,
                                        VerticalOptions = LayoutOptions.FillAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Spacing = 1,
                                        Padding = new Thickness(0),
                                        Children =
                                        {
                                            new Label
                                            {
                                                Text = product.Title,
                                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                                TextColor = Color.FromHex("#FFFFFF"),
                                                FontSize = 15
                                            },
                                            new Label
                                            {
                                                Text = product.Type,
                                                VerticalOptions = LayoutOptions.StartAndExpand,
                                                TextColor = Color.FromHex("#FFFB21"),
                                                FontSize = 12
                                            }
                                        }
                                    },
                                    new Image
                                    {
                                        Source = Device.OnPlatform(iOS: ImageSource.FromFile("Images/uncheck.png"),
                                        Android: ImageSource.FromFile("uncheck.png"),
                                        WinPhone: ImageSource.FromFile("Images/uncheck.png")),
                                        VerticalOptions = LayoutOptions.EndAndExpand,
                                        HorizontalOptions = LayoutOptions.EndAndExpand,
                                        HeightRequest = 30,
                                        WidthRequest = 30,
                                        Margin = new Thickness(0,0,0,9),
                                       GestureRecognizers= { tapGestureRecognizer }
                                    }
                                }
                            }
                        }
                    };
                    gridLayout.Children.Add(stack, columnIndex, rowIndex);
                }
            }
            _viewModel._isBusy = false;
            _viewModel.OnPropertyChanged(nameof(IsBusy));
        }
    }
}
