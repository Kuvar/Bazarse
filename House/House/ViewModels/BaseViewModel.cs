using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using House.Annotations;
using Xamarin.Forms;

namespace House.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        internal virtual Task Initialize(params object[] args)
        {
            return Task.FromResult(0);
        }

        public bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }

        [NotifyPropertyChangedInvocator]
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetObservableProperty<T>( ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region RunSafe

        public async Task RunSafe(Action actionToExecute)
        {
            try
            {
                IsBusy = true;
                await Task.Run(() => { actionToExecute?.Invoke(); });
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                string errorMessage = ex.Message;
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = Constants.SomethingWentWrong;
                }
                await Application.Current.MainPage.DisplayAlert(Constants.ErrorTitle, errorMessage, Constants.CancelButtonTitle);
            }
        }

        public virtual void OnDisappear()
        {
            //Clean your stuff
        }

        public virtual void OnAppear()
        {
            //Create your stuff
        }

        #endregion
    }
}
