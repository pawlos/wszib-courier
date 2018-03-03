using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Courier.Core.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Courier.Core.Services
{
    public class LocationService : ILocationService
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json")
        };

        public async Task<AddressDto> GetAsync(string address)
        {
            var response = await _client.GetAsync($"?address={address}&key=AIzaSyD5UaNtOrvxjvxUJscB1qsCfHrPWv6UTtk");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationResponse>(content);

            return new AddressDto();
        }

        private class LocationResponse
        {
            public IEnumerable<LocationResult> Results { get; set; }
        }

        private class LocationResult
        {
            [JsonProperty(PropertyName = "formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty(PropertyName = "address_components")]
            public IEnumerable<AddressComponent> AddressComponents { get; set; }
        }

        private class AddressComponent
        {
            [JsonProperty(PropertyName = "long_name")]
            public string LongName { get; set; }

            [JsonProperty(PropertyName = "short_name")]
            public string ShortName { get; set; }

            public IEnumerable<string> Types { get; set; }
        }
    }
}