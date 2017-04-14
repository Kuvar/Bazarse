using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using House.Helpers;
using House.Models;
using House.Pages;
using Xamarin.Forms;

namespace House.ViewModels
{
    public class LoginViewModel : ObservableObject
    {

        #region Command
        private ICommand _goTosignupCommand;
        public ICommand GoToSignupCommand
        {
            get { return _goTosignupCommand; }
        }

        private ICommand _goToLoginCommand;
        public ICommand GoToLoginCommand { get { return _goToLoginCommand; } }

        private ICommand _signUpCommand;
        public ICommand SignUpCommand { get { return _signUpCommand; } }

        private ICommand _loginCommand;
        public ICommand LoginCommand { get { return _loginCommand; } }

        private ICommand _onEmailTextChanged;
        public ICommand OnEmailTextChanged { get { return _onEmailTextChanged; } }

        private ICommand _onPasswordTextChanged;
        public ICommand OnPasswordTextChanged { get { return _onPasswordTextChanged; } }

        
        #endregion

        #region Properties
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy,value); }
        }

        private bool _isError;
        public bool IsError { get { return _isError; } set { SetProperty(ref _isError ,value); } }

        private string _errorMessage;
        public string ErrorMessage { get { return _errorMessage; } set { SetProperty(ref _errorMessage, value); } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; } }

        private string _mobile;
        public string Mobile { get { return _mobile; } set { _mobile = value; } } 
         
        private string _password;
        public string Password { get { return _password; } set { _password = value; } }
        #endregion

        public LoginViewModel(INavigation navigation)
        {
            IsError = false;
            Navigation = navigation;
            _goTosignupCommand = new Command(GoToSignup);
            _goToLoginCommand = new Command(GoToLogin);
            _signUpCommand = new Command(SignUp);
            _loginCommand = new Command(Login);
            _onEmailTextChanged = new Command(EmailTextChanged);
            _onPasswordTextChanged = new Command(PasswordTextChanged);
        }

        private void EmailTextChanged()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                if (ErrorMessage != null && ErrorMessage.Equals("Email is required."))
                {
                    ErrorMessage = "";
                    IsError = false;
                }
            }
            else {
                ErrorMessage = "Email is required.";
                IsError = true;
            }                       
        }

        private void PasswordTextChanged()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                if (ErrorMessage != null && ErrorMessage.Equals("Password is required."))
                {
                    ErrorMessage = "";
                    IsError = false;
                }
            }
            else
            {
                ErrorMessage = "Password is required.";
                IsError = true;
            }
        }

        async void Login()
        {
            await App.Navigation.PushModalAsync(new MainPage());
        }

        async void Login1()
        {
            
            if (string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(_email))
            {
                ErrorMessage = "Email is required.";
                IsError = true;
            }
            if (string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(_password))
            {
                ErrorMessage = "Password is required.";
                IsError = true;
            }

            if (string.IsNullOrEmpty(ErrorMessage) && !IsError)
            {
                LoginModel obj = new LoginModel { Email = Email, Password = Password };
                await ServiceHandler.PostData<LoginResponse, LoginModel>("Login", HttpMethod.Post, obj).ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        Application.Current.MainPage.DisplayAlert("", Constants.SomethingWentWrong, "OK");
                    }
                    else
                    {
                        var data = t.Result;
                        if (data?.StatusCode == HttpStatusCode.OK)
                        {
                            if (data.User != null)
                            {
                               
                                
                            }
                            else {
                                ErrorMessage = "Incorrect username or password.";
                                IsError = true;
                            }
                            
                        }
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        async void SignUp()
        {
            User obj = new User
            {
                Name = Name,
                Email = Email,
                Mobile = Mobile,
                Password = Password
            };
            await ServiceHandler.PostData<SaveResponse, User>("SignUp", HttpMethod.Post, obj).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    Application.Current.MainPage.DisplayAlert("", Constants.SomethingWentWrong, "OK");
                }
                else
                {
                    var data = t.Result;
                    if (data != null)
                    {
                        if (data.StatusCode == HttpStatusCode.OK)
                        {
                            Application.Current.MainPage.DisplayAlert("", "You have successfully registered.", "OK");
                            Navigation.PushModalAsync(new LoginPage());
                        }
                       else if (data.StatusCode == HttpStatusCode.Conflict)
                        {
                            Application.Current.MainPage.DisplayAlert("", data.ErrorMessage, "OK");
                        }
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("", Constants.SomethingWentWrong, "OK");
                    }
                }
            },TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void GoToLogin()
        {
            Navigation.PushModalAsync(new LoginPage());
        }

        private void GoToSignup()
        {
            Navigation.PushModalAsync(new SignupPage());
        }
    }
}
