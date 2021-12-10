using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XPetClinic_MobileFrontend.Models
{
    public sealed class ClientList : BindableObject
    {
        private static readonly Lazy<ClientList> lazy =
           new Lazy<ClientList>(() => new ClientList());

        public static ClientList Instance { get => lazy.Value; }

        private ClientList()
        {

        }
        
        public List<Client> clients { get; set; }
    }
}
