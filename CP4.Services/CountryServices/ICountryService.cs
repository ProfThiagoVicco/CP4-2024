using CP4.Services.CountryServices.Responses;

namespace CP4.Services.CountryServices;

public interface ICountryService
{
    Task<List<CountriesResponse>> GetCountries();
}