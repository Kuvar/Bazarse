using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using House.Pages;

using Xamarin.Forms;

namespace House
{
    public partial class App : Application
    {
        public static int ScreenWidth;
        public static INavigation Navigation { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //MainPage = new LoginPage();
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
