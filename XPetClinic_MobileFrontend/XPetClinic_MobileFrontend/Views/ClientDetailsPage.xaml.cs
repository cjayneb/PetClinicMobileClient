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
    [QueryProperty(nameof(clientId), nameof(clientId))]
    public partial class ClientDetailsPage : ContentPage
    {

        public int clientId { get; set; }

        public ClientDetailsPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            BindingContext =
                new ClientDetailsViewModel(clientId);
        }
        
    }
}