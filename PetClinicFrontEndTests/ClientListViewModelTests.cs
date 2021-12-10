using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XPetClinic_MobileFrontend.Models;
using XPetClinic_MobileFrontend.ViewModels;
using Xunit;

namespace PetClinicFrontEndTests
{
    public class ClientListViewModelTests
    {
        [Fact]
        public void ViewModel_ClientsInstanceIsInitialized()
        {
            string expectedTitle = "Clients";
            // Arrange and Act
            ClientListViewModel viewModel = new ClientListViewModel();

            // Assert
            Assert.Equal(expectedTitle, viewModel.Title);
            Assert.Equal(viewModel.ClientsInstance, ClientList.Instance);

        }

        [Fact]
        public void ViewModel_ClientsIsInitialized()
        {
            // Arrange and Act
            ClientListViewModel viewModel = new ClientListViewModel();

            // Assert
            Assert.NotNull(viewModel.Clients);
            Assert.Empty(viewModel.Clients);

        }

        [Fact]
        public void ViewModel_CommandsAreInitialized()
        {
            // Arrange and Act
            ClientListViewModel viewModel = new ClientListViewModel();

            // Assert
            Assert.NotNull(viewModel.SelectedCommand);
            Assert.NotNull(viewModel.RefreshCommand);
            Assert.IsType<AsyncCommand>(viewModel.SelectedCommand);
            Assert.IsType<AsyncCommand>(viewModel.RefreshCommand);

        }

        [Fact]
        public void ViewModel_SelectedItemSetterAndGetterWorks()
        {
            // Arrange
            ClientListViewModel viewModel = new ClientListViewModel();

            int expectedId = 200;
            string expectedFirstName = "John";
            string expectedLastName = "Smith";
            string expectedAddress = "200 cool street";
            string expectedCity = "cool city";
            string expectedTelephone = "4504504500";

            string expectedVisitId = "validVisitId";
            string expectedDescription = "description";
            int expectedPetId = 200;
            int expectedPractitionerId = 200200;
            string expectedDate = "2021-01-01";
            bool expectedStatus = true;

            string expectedPetName = "Max";
            string expectedBirthDate = "2021-02-02";

            Visit expectedVisit = new Visit()
            {
                visitId = expectedVisitId,
                petId = expectedPetId,
                practitionerId=expectedPractitionerId,
                description = expectedDescription,
                date = expectedDate,
                status = expectedStatus
            };

            Pet expectedPet = new Pet()
            {
                id = expectedPetId,
                name = expectedPetName,
                birthDate = expectedBirthDate
            };

            // Act
            viewModel.SelectedItem = new Client()
            {
                id = 200,
                firstName = "John",
                lastName = "Smith",
                address = "200 cool street",
                city = "cool city",
                telephone = "4504504500",
                visits = new List<Visit>()
                {
                    new Visit()
                    {
                        visitId = "validVisitId",
                        description = "description",
                        petId = 200,
                        practitionerId = 200200,
                        date = "2021-01-01",
                        status = true
                    }
                },
                pets = new List<Pet>()
                {
                    new Pet()
                    {
                        id = 200,
                        name = "Max",
                        birthDate = "2021-02-02"
                    }
                }
            };

            // Assert
            Assert.NotNull(viewModel.SelectedItem);
            Assert.Equal(expectedId, viewModel.SelectedItem.id);
            Assert.Equal(expectedFirstName, viewModel.SelectedItem.firstName);
            Assert.Equal(expectedLastName, viewModel.SelectedItem.lastName);
            Assert.Equal(expectedAddress, viewModel.SelectedItem.address);
            Assert.Equal(expectedCity, viewModel.SelectedItem.city);
            Assert.Equal(expectedTelephone, viewModel.SelectedItem.telephone);

            Assert.Equal(JsonConvert.SerializeObject(expectedVisit), 
                JsonConvert.SerializeObject(viewModel.SelectedItem.visits.Find(v => v.visitId == expectedVisitId)));
            Assert.Equal(JsonConvert.SerializeObject(expectedPet),
                JsonConvert.SerializeObject(viewModel.SelectedItem.pets.Find(p => p.id == expectedPetId)));

        }
    }
}
