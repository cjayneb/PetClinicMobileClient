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
    public class ClientDetailsViewModel : ViewModelBase
    {

        private INetworkService<HttpResponseMessage> networkService;

        public AsyncCommand<Pet> ManageCommand { get; }
        public AsyncCommand GoBackCommand { get; }

        private Pet selectedPet;

        public Pet SelectedPet
        {
            get => selectedPet;
            set => SetProperty(ref selectedPet, value);
        }



        public ClientList ClientsInstance { get; set; }

        private Client selectedClient;
        public Client SelectedClient
        {
            get => selectedClient;
            set => SetProperty(ref selectedClient, value);
        }

        private int id;

        public ClientDetailsViewModel()
        {

        }

        public ClientDetailsViewModel(int id)
        {
            Title = "Client Details";

            this.networkService = NetworkService<HttpResponseMessage>.Instance;
            this.id = id;

            ClientsInstance = ClientList.Instance;

            GoBackCommand = new AsyncCommand(GoBack);

            ManageCommand = new AsyncCommand<Pet>(Manage);

            SetupAsync();
        }

        private async Task Manage(Pet thePet)
        {            
            string route = $"{nameof(VisitListPage)}?petId={thePet.id}&clientId={SelectedClient.id}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task SetupAsync()
        {

            SelectedClient = ClientsInstance.clients.Find(c => c.id == id);

        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(ClientListPage)}");
            //await Shell.Current.GoToAsync("..");
        }
    }
}
