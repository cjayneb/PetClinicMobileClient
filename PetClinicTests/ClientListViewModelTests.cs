using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPetClinic_MobileFrontend.ViewModels;
using XPetClinic_MobileFrontend;
using Xunit;

namespace PetClinicTests
{
    public class ClientListViewModelTests
    {
        [Fact]
        public void ViewModel_does_not_fill_list_of_clients()
        {
            // Arrange
            ClientListViewModel viewModel = new ClientListViewModel();

            // Act


            // Assert
            Assert.Empty(viewModel.ClientsInstance.clients);
        }
    }
}
