using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace House
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void NavigateToPage(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;
        }
    }
}
