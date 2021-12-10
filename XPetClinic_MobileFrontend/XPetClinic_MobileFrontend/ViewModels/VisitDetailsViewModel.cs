using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VisitDetailsViewModel : ViewModelBase 
    {

        private INetworkService<HttpResponseMessage> networkService;
        private string visitId;
        private int clientId;
        private int petId;

        public ClientList ClientsInstance;

        public AsyncCommand GoBackCommand { get; }

        public AsyncCommand SubmitCommand { get; }

        private Visit selectedVisit;

        public Visit SelectedVisit
        {
            get => selectedVisit;
            set => SetProperty(ref selectedVisit, value);
        }

        private Vet selectedVet;

        public Vet SelectedVet
        {
            get => selectedVet;
            set
            {
                SetProperty(ref selectedVet, value);
                SelectedVisit.practitionerId = SelectedVet.vetId;
            }
        }

        public ObservableRangeCollection<Vet> Vets { get; set; }

        private string buttonText;

        public string ButtonText
        {
            get => buttonText;
            set => SetProperty(ref buttonText, value);
        }

        private string message;

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }



        public VisitDetailsViewModel() { }

        public VisitDetailsViewModel(string visitId, int clientId, int petId)
        {
            Title = "Visit Details";

            this.visitId = visitId;

            this.clientId = clientId;

            this.petId = petId;

            this.networkService = NetworkService<HttpResponseMessage>.Instance;

            ClientsInstance = ClientList.Instance;

            Vets = new ObservableRangeCollection<Vet>();

            GoBackCommand = new AsyncCommand(GoBack);

            SubmitCommand = new AsyncCommand(Submit);

            SetupAsync();
        }


        private async Task Submit()
        {
            Message = "";

            if(SelectedVisit.description == null || SelectedVet == null
                || SelectedVisit.description == "")
            {
                Message = "Fields cannot be empty.";
                return;
            }

            DateTime d = new DateTime();
            DateTime.TryParse(SelectedVisit.date, out d);

            SelectedVisit.date = d.ToString("yyyy-MM-dd");

            HttpResponseMessage response = new HttpResponseMessage();

            if (SelectedVisit.visitId != null)
            {
                response =
                    await networkService.PutAsync(
                        ApiConstants.GetVisitPutURI(SelectedVisit.visitId, petId),
                        SelectedVisit);
            }
            else
            {
                response =
                    await networkService.PostAsync(
                        ApiConstants.GetVisitPostURI(petId), SelectedVisit);
            }

            if(response.StatusCode.Equals(HttpStatusCode.Forbidden))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Error", "Something happened.", "Cool");
                return;
            }

            string content = await response.Content.ReadAsStringAsync();

            Visit respVisit = JsonConvert.DeserializeObject<Visit>(content);

            if(SelectedVisit.visitId != null)
            {
                ClientsInstance.clients
                    .Find(c => c.id == clientId).visits
                    .RemoveAll(v => v.visitId == SelectedVisit.visitId);
                
                
            }

            ClientsInstance.clients
                    .Find(c => c.id == clientId).visits.Add(respVisit);
            //OnPropertyChanged("ClientsInstance");

            await GoBack();
        }

        private async Task GoBack()
        {

            DateTime d = new DateTime();
            DateTime.TryParse(SelectedVisit.date, out d);

            SelectedVisit.date = d.ToString("yyyy-MM-dd");

            await Shell.Current.GoToAsync($"{nameof(VisitListPage)}?petId={petId}&clientId={clientId}");
        }

        private async Task SetupAsync()
        {
            HttpResponseMessage response = await networkService.GetAsync(ApiConstants.GetVetsURI());

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

            List<Vet> respVets = JsonConvert.DeserializeObject<List<Vet>>(content);
            Vets.ReplaceRange(respVets);
            OnPropertyChanged("Vets");

            if (visitId != "-1")
            {
                ButtonText = "Update";
                SelectedVisit = ClientsInstance.clients.Find(c => c.id == clientId)
                    .visits.Find(v => v.visitId == visitId);
                SelectedVet = Vets.FirstOrDefault(v => v.vetId == SelectedVisit.practitionerId);
            }
            else
            {
                ButtonText = "Create";
                SelectedVisit = new Visit()
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd"),
                    petId = petId
                };
            }

            Console.WriteLine(respVets);
        }

    }
}
