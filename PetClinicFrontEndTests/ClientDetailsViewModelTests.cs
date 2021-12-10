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
    public class ClientDetailsViewModelTests
    {
        [Fact]
        public void ViewModel_ClientsInstanceIsInitialized()
        {
            int expectedClientId = 200;
            
            // Arrange and Act
            ClientDetailsViewModel viewModel = new ClientDetailsViewModel(expectedClientId);

            string expectedTitle = "Client Details";


            // Assert
            Assert.Equal(expectedTitle, viewModel.Title);
            Assert.Equal(viewModel.ClientsInstance, ClientList.Instance);
            Assert.Null(viewModel.SelectedClient);

        }


        [Fact]
        public void ViewModel_CommandsAreInitialized()
        {
            // Arrange and Act
            ClientDetailsViewModel viewModel = new ClientDetailsViewModel(1);

            // Assert
            Assert.NotNull(viewModel.ManageCommand);
            Assert.NotNull(viewModel.GoBackCommand);
            Assert.IsType<AsyncCommand<Pet>>(viewModel.ManageCommand);
            Assert.IsType<AsyncCommand>(viewModel.GoBackCommand);

        }

        [Fact]
        public void ViewModel_SelectedItemSetterAndGetterWorks()
        {
            // Arrange
            ClientDetailsViewModel viewModel = new ClientDetailsViewModel();

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
            viewModel.SelectedClient = new Client()
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
            Assert.NotNull(viewModel.SelectedClient);
            Assert.Equal(expectedId, viewModel.SelectedClient.id);
            Assert.Equal(expectedFirstName, viewModel.SelectedClient.firstName);
            Assert.Equal(expectedLastName, viewModel.SelectedClient.lastName);
            Assert.Equal(expectedAddress, viewModel.SelectedClient.address);
            Assert.Equal(expectedCity, viewModel.SelectedClient.city);
            Assert.Equal(expectedTelephone, viewModel.SelectedClient.telephone);

            Assert.Equal(JsonConvert.SerializeObject(expectedVisit),
                JsonConvert.SerializeObject(viewModel.SelectedClient.visits.Find(v => v.visitId == expectedVisitId)));
            Assert.Equal(JsonConvert.SerializeObject(expectedPet),
                JsonConvert.SerializeObject(viewModel.SelectedClient.pets.Find(p => p.id == expectedPetId)));

        }
    }
}
