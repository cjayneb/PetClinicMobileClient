using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
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
    public class ClientListViewModel : ViewModelBase
    {

        private INetworkService<HttpResponseMessage> networkService;

        public AsyncCommand SelectedCommand { get; }

        public AsyncCommand RefreshCommand { get; }

        public ObservableRangeCollection<Client> Clients { get; set; }

        public ClientList ClientsInstance { get; set; }

        private Client selectedItem;

        public Client SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ClientListViewModel()
        {
            Title = "Clients";

            this.networkService = NetworkService<HttpResponseMessage>.Instance;

            this.ClientsInstance = ClientList.Instance;

            Clients = new ObservableRangeCollection<Client>();

            SelectedCommand = new AsyncCommand(GoToClientDetails);
            RefreshCommand = new AsyncCommand(Refresh);


            InitCLients();
        }

        private async Task GoToClientDetails()
        {
            if (SelectedItem == null)
                return;

            string route = $"{nameof(ClientDetailsPage)}?clientId={SelectedItem.id}";
            await Shell.Current.GoToAsync(route);

            SelectedItem = null;
        }

        private async Task Refresh()
        {
            IsBusy = true;

            HttpResponseMessage response = await networkService.GetAsync(ApiConstants.GetOwnersURI());

            if (response.StatusCode.Equals(HttpStatusCode.Forbidden))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Error", "Something happened.", "Epic");
                return;
            }

            string content = await response.Content.ReadAsStringAsync();

            List<Client> cli = JsonConvert.DeserializeObject<List<Client>>(content);

            ClientsInstance.clients.RemoveAll(c => c.id != null);
            ClientsInstance.clients.AddRange(cli);

            Clients.ReplaceRange(ClientsInstance.clients);

            IsBusy = false;
        }

        private async Task InitCLients()
        {
            HttpResponseMessage response = await networkService.GetAsync(ApiConstants.GetOwnersURI());

            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Error", "Service is unavailable.", "Cool");
                return;
            }

            string serialized = await response.Content.ReadAsStringAsync();

            ClientsInstance.clients = JsonConvert.DeserializeObject<List<Client>>(serialized);

            Clients.ReplaceRange(ClientsInstance.clients);
        }
    }
}
