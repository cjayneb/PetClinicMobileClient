using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XPetClinic_MobileFrontend.Services.Network;
using XPetClinic_MobileFrontend.ViewModels;

namespace XPetClinic_MobileFrontend.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(visitId), nameof(visitId))]
    [QueryProperty(nameof(clientId), nameof(clientId))]
    [QueryProperty(nameof(petId), nameof(petId))]
    public partial class VisitDetailsPage : ContentPage
    {

        public string visitId { get; set; }

        public int clientId { get; set; }

        public int petId { get; set; }

        public VisitDetailsPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new VisitDetailsViewModel(visitId, clientId, petId);
        }
    }
}