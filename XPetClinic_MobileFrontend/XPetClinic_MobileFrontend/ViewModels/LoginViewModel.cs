using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XPetClinic_MobileFrontend.Models;
using XPetClinic_MobileFrontend.Services.Network;
using XPetClinic_MobileFrontend.Views;

namespace XPetClinic_MobileFrontend.ViewModels
{
    public class LoginViewModel : ViewModelBase 
    {

        private INetworkService<HttpResponseMessage> networkService;

        public AsyncCommand LoginCommand { get; }

        private string username;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string message;

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        public LoginViewModel()
        {
            Title = "Login";

            this.networkService = NetworkService<HttpResponseMessage>.Instance;

            LoginCommand = new AsyncCommand(Login);
        }

        private async Task Login()
        {
            Message = "";

            if(password == null || username == null ||
                password.Length == 0 || username.Length == 0)
            {
                Message = "Fields cannot be empty.";
                return;
            }

            User user = new User()
            {
                email = this.username,
                password = this.password
            };

            HttpResponseMessage response = await networkService.PostAsync(ApiConstants.GetLoginURI(), user);

            if(response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                Message = "Wrong credentials. Try again.";
                return;
            }
            if (!response.IsSuccessStatusCode)
            {
                Message = "Service unavailable.";
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(ClientListPage)}");
        }
    }
}
