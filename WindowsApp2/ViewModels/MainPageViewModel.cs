using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using WindowsApp2.Models;
using WindowsApp2.ViewModels.Commands;
using System.Net.Http;
using Template10.Utils;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;//new

namespace WindowsApp2.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private static readonly HttpClient client = new HttpClient();

        public TryLogin TryLogin { get; set; }
        public TryLogout TryLogout { get; set; }
        public TryRegister TryRegister { get; set; }
        public MainPageViewModel()
        {
            //ProfileButtonVisibility = "Collapsed";
            Login = "";
            this.TryLogin = new TryLogin(this);
            this.TryLogout = new TryLogout(this);
            this.TryRegister = new TryRegister(this);
        }
        string _Login = "";
        public string Login { get { return _Login; } set { Set(ref _Login, value); } }

        // is htis even needed? xd
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Login = suspensionState[nameof(Login)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Login)] = Login;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        /// ////////////////////


        /// /////////////////////////
        public void GotoDetailsPage() =>
            NavigationService.NavigateAsync(typeof(Views.DetailPage), Login);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

        public async void LogOut(Page grid)
        {
            TextBlock block = grid.FindName("Error1") as TextBlock;
            TextBox Login = grid.FindName("Login") as TextBox;
            PasswordBox Password = grid.FindName("Password") as PasswordBox;
            Button loginButton = grid.FindName("submitButton2") as Button;
            Button logoutButton = grid.FindName("LogoutButton") as Button;
            Button RegisterButon = grid.FindName("RegisterButton") as Button;
            ProgressBar progressbar = grid.FindName("progressbar") as ProgressBar;

            progressbar.IsIndeterminate = true;
            await Task.Delay(2000);
            progressbar.IsIndeterminate = false;
            UserAccount.LogOut();
            block.Text = "You have beed logged out.";
            block.Tag = "LoggedOut";
            loginButton.Visibility = Visibility.Visible;
            Login.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Visible;
            logoutButton.Visibility = Visibility.Collapsed;
            RegisterButon.Visibility = Visibility.Visible;

            Views.Shell.HamburgerMenu.IsFullScreen = true;
        }
        public async void AccessTheWebAsync(Page grid)
        {
           
            TextBlock block = grid.FindName("Error1") as TextBlock;
            TextBox Login = grid.FindName("Login") as TextBox;
            PasswordBox Password = grid.FindName("Password") as PasswordBox;
            ProgressBar progressbar = grid.FindName("progressbar") as ProgressBar;
            Button loginButton = grid.FindName("submitButton2") as Button;
            Button logoutButton = grid.FindName("LogoutButton") as Button;
            Button RegisterButon = grid.FindName("RegisterButton") as Button;

            block.Text = "Wait...";
            progressbar.IsIndeterminate = true;

            var values = new Dictionary<string, string>
            {
               { "post_user", Login.Text },
               { "post_pass", Password.Password }
            };
            
            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await client.PostAsync("https://adammak2342.000webhostapp.com/login.php", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (responseString == "")
                {
                    progressbar.IsIndeterminate = false;
                    block.Text = "User doesn't exist.";
                }
                else if (responseString == "login-SUCCESS")
                {
                    UserAccount.LogIn(Login.Text);
                    block.Text = "Logged in as " + UserAccount.LoggedInUsername;
                    block.Tag = "LoggedIn";
                    progressbar.IsIndeterminate = false;
                    loginButton.Visibility = Visibility.Collapsed;
                    Login.Visibility = Visibility.Collapsed;
                    Password.Visibility = Visibility.Collapsed;
                    logoutButton.Visibility = Visibility.Visible;
                    RegisterButon.Visibility = Visibility.Collapsed;

                    Views.Shell.HamburgerMenu.IsFullScreen = false;
                }
                else
                {
                    progressbar.IsIndeterminate = false;
                    block.Text = responseString;// "There is problem with connection, please try again";
                }
            }
            catch (HttpRequestException e) { block.Text = e.StackTrace; }
        }
        public async void Register(Page grid)
        {
            TextBlock block = grid.FindName("Error1") as TextBlock;
            TextBox Login = grid.FindName("Login") as TextBox;
            PasswordBox Password = grid.FindName("Password") as PasswordBox;
            ProgressBar progressbar = grid.FindName("progressbar") as ProgressBar;
            Button loginButton = grid.FindName("submitButton2") as Button;
            Button logoutButton = grid.FindName("LogoutButton") as Button;
            Button RegisterButon = grid.FindName("RegisterButton") as Button;

            block.Text = "Wait...";
            progressbar.IsIndeterminate = true;

            var values = new Dictionary<string, string>
            {
               { "post_user", Login.Text },
               { "post_pass", Password.Password }
            };
            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await client.PostAsync("https://adammak2342.000webhostapp.com/register.php", content);
                var responseString = await response.Content.ReadAsStringAsync();

                    if (responseString == "User has been created!")
                {
                    block.Text = "Registered as " + Login.Text;
                    UserAccount.LogIn(Login.Text);
                    block.Tag = "LoggedIn";
                    progressbar.IsIndeterminate = false;
                    loginButton.Visibility = Visibility.Collapsed;
                    Login.Visibility = Visibility.Collapsed;
                    Password.Visibility = Visibility.Collapsed;
                    logoutButton.Visibility = Visibility.Visible;
                    RegisterButon.Visibility = Visibility.Collapsed;

                    Views.Shell.HamburgerMenu.IsFullScreen = false;
                }

                else
                {
                    progressbar.IsIndeterminate = false;
                    block.Text = responseString; // "There is problem with connection";
                }
            }
            catch (HttpRequestException e) { block.Text = e.StackTrace; }
        }
    }
}

