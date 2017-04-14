using House.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace House.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage 
    {
        public LoginPage()
        {
            string imgName = string.Empty;
            InitializeComponent();
            this.BindingContext = new LoginViewModel(this.Navigation);

            var centerX = Constraint.RelativeToParent(parent => (parent.Width / 2) - 141);
            var centerY = Constraint.RelativeToParent(parent => (parent.Height / 2) - 152);

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    imgName = "BazaarSe_Logo.png";
                    break;
                case Device.Android:
                    imgName = "BazaarSe_Logo.png";
                    break;
                case Device.WinPhone:
                    imgName = "BazaarSe_Logo.png";
                    break;
            }

            var logo = new Image
            {
                 Source = imgName,
                 HorizontalOptions = LayoutOptions.CenterAndExpand,
                 VerticalOptions = LayoutOptions.CenterAndExpand,
                 Opacity = .20
            };

            Relativelayout.Children.Add(logo,centerX,centerY);

            
        }

        public async void IsLogin()
        {
            Navigation.InsertPageBefore(new MainPage(), this);
            if (Device.OS == TargetPlatform.iOS)
            {
                await Navigation.PopToRootAsync();
            }
            Application.Current.MainPage = new MainPage();
        }
    }
}
