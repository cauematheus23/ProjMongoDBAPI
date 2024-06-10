
using Newtonsoft.Json;
using ProjMongoDBAPI.Models.DTO;

namespace ProjMongoDBAPI.Services
{
    public class PostOfficeServices
    {
        static readonly HttpClient address = new HttpClient();

        public static async Task<AddressDTO> GetAddress(string cep)
        {
            try
            {
                var response = await address.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AddressDTO>(content);

            }
            catch (HttpRequestException ex)
            {

                throw;
            }
        }
    }
}
