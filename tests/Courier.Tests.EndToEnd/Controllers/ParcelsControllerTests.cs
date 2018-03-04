using System.Threading.Tasks;
using Courier.Core.Dto;
using Courier.Core.Queries;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Courier.Tests.EndToEnd.Controllers
{
    public class ParcelsControllerTests : TestControllerBase
    {
        [Fact]
        public async Task parcels_controller_get_should_return_parcels()
        {
            var expectedParcels = 10; 
            var response = await HttpClient.GetAsync($"/parcels?page=1&results={expectedParcels}");
            var result = await DeserializeAsync<PagedResult<ParcelDto>>(response);
            result.Should().NotBeNull();
            result.ResultsPerPage.Should().Be(expectedParcels);
            result.Results.Should().NotBeEmpty();
            result.Results.Should().HaveCount(expectedParcels);
        }
    }
}