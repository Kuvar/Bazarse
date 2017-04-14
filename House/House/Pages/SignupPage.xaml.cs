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
    public partial class SignupPage 
    {
        public SignupPage()
        {
            string imgName = string.Empty;
            InitializeComponent();
            this.BindingContext = new LoginViewModel(Navigation);

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

            Relativelayout.Children.Add(logo, centerX, centerY);
        }
    }
}
