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
    public class VisitListViewModel : ViewModelBase 
    {

        private INetworkService<HttpResponseMessage> networkService;

        public AsyncCommand SelectedCommand { get; }

        public AsyncCommand GoBackCommand { get; }

        public AsyncCommand<Visit> DeleteCommand { get; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand AddCommand { get; }

        public event EventHandler ShowDeleteUndo;

        public ObservableRangeCollection<Visit> VisitList { get; set; }

        private int petId;
        private int clientId;

        private Visit selectedVisit;

        public Visit SelectedVisit
        {
            get { return selectedVisit; }
            set { selectedVisit = value; }
        }

        public ClientList ClientsInstance { get; set; }

        public VisitListViewModel() { }

        public VisitListViewModel(int petId, int clientId)
        {
            Title = "Visits";

            this.petId = petId;

            this.clientId = clientId;

            this.networkService = NetworkService<HttpResponseMessage>.Instance;

            ClientsInstance = ClientList.Instance;

            SelectedCommand = new AsyncCommand(Selected);

            GoBackCommand = new AsyncCommand(GoBack);

            AddCommand = new AsyncCommand(Add);

            RefreshCommand = new AsyncCommand(Refresh);

            DeleteCommand = new AsyncCommand<Visit>(Delete);

            VisitList = new ObservableRangeCollection<Visit>();

            Refresh();
        }

        private async Task Refresh()
        {
            IsBusy = true;
            HttpResponseMessage response = await networkService.GetAsync(ApiConstants.GetVisitsForPetURI(petId));

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

            List<Visit> vis = JsonConvert.DeserializeObject<List<Visit>>(content);

            ClientsInstance.clients.Find(c => c.id == clientId).visits.RemoveAll(v => v.visitId.Length > 0);
            ClientsInstance.clients.Find(c => c.id == clientId).visits.AddRange(vis);

            VisitList.ReplaceRange(ClientsInstance.clients.Find(c => c.id == clientId)
                .visits.FindAll(v => v.petId == petId));

            IsBusy = false;

        }

        private async Task Add()
        {
            await Shell.Current.GoToAsync($"{nameof(VisitDetailsPage)}?clientId={clientId}&petId={petId}&visitId=-1");
        }

        private async Task Delete(Visit visit)
        {
            if (visit == null)
                return;

            bool confirmed = await Shell.Current.DisplayAlert("Delete Confirmation", "Do you really want to delete this visit?", "Yes", "No");

            if (!confirmed)
                return;

            HttpResponseMessage response = 
                await networkService.DeleteAsync(ApiConstants.GetDeleteVisitURI(visit.visitId));

            if (response.StatusCode.Equals(HttpStatusCode.Forbidden))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Error", $"Something happened. {response.StatusCode}", "Epic");
                return;
            }

            ClientsInstance.clients.Find(c => c.id == clientId).visits.RemoveAll(v => v.visitId == visit.visitId);
            VisitList.Remove(visit);

            
            ShowDeleteUndo?.Invoke(this, EventArgs.Empty);

        }

        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(ClientDetailsPage)}?clientId={clientId}");
        }

        private async Task Selected()
        {
            if (SelectedVisit == null)
                return;

            string route = $"{nameof(VisitDetailsPage)}?visitId={SelectedVisit.visitId}&clientId={clientId}&petId={petId}";
            await Shell.Current.GoToAsync(route);

            SelectedVisit = null;
        }

       

    }
}
