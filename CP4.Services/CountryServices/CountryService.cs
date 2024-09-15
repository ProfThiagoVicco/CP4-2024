using CP4.Services.CountryServices.Responses;
using Newtonsoft.Json;

namespace CP4.Services.CountryServices;

public class CountryService: ICountryService
{
    public async Task<List<CountriesResponse>> GetCountries()
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("https://restcountries.com/v3.1/all?fields=name,flags");

        HttpResponseMessage response = await client.GetAsync("");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<CountriesResponse> >(json);
        }
        else
        {
            return null;
        }
    }
}