using XPetClinic_MobileFrontend.ViewModels;
using Xunit;

namespace PetClinicClientTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            ClientListViewModel viewModel = new ClientListViewModel();

            // Act

            // Assert
            Assert.NotNull(viewModel.ClientsInstance);
        }
    }
}