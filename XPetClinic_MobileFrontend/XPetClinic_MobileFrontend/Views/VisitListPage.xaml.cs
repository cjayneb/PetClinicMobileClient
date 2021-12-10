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
    [QueryProperty(nameof(petId), nameof(petId))]
    [QueryProperty(nameof(clientId), nameof(clientId))]

    public partial class VisitListPage : ContentPage
    {

        public int petId { get; set; }
        public int clientId { get; set; }


        public VisitListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            VisitListViewModel visitListViewModel = new VisitListViewModel(petId, clientId);
            BindingContext = visitListViewModel;
           
            visitListViewModel.ShowDeleteUndo += VisitListViewModel_ShowDeleteUndo;
        }

        private async void VisitListViewModel_ShowDeleteUndo(object sender, EventArgs e)
        {
            progressBar.IsVisible = true;
            undoDeleteButton.IsVisible = true;
            progressBar.Progress = 100;
            await progressBar.ProgressTo(0, 5000, Easing.Linear);
            progressBar.IsVisible = false;
            undoDeleteButton.IsVisible = false;
        }
    }
}