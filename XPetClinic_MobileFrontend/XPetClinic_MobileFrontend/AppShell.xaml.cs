using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPetClinic_MobileFrontend.Views;

namespace XPetClinic_MobileFrontend
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ClientListPage), typeof(ClientListPage));
            Routing.RegisterRoute(nameof(ClientDetailsPage), typeof(ClientDetailsPage));
            Routing.RegisterRoute(nameof(VisitListPage), typeof(VisitListPage));
            Routing.RegisterRoute(nameof(VisitDetailsPage), typeof(VisitDetailsPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

    }
}
