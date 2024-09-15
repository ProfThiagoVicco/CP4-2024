using CP4.Services.CountryServices;
using CP4.Services.CountryServices.Responses;

namespace CP4.Services.Test;

public class CountryServiceTest
{
    
    private readonly CountryService _countryService;
    
    public CountryServiceTest()
    {
        //A - Arrange (Preparação)
        _countryService = new CountryService();
    }
    
    [Fact]
    public async Task GetCountries_ReturnsListCountriesResponse()
    {  
        //A - Action (Ação)
        List<CountriesResponse> countriesResponses = await _countryService.GetCountries();

        //A - Assert (Resultado)
        Assert.NotNull(countriesResponses);
        Assert.Equal(250, countriesResponses.Count);
    }
    
    [Fact]
    public async Task GetCountries_ReturnsHungaryItemInListCountriesResponse()
    {  
        //A - Action (Ação)
        List<CountriesResponse> countriesResponses = await _countryService.GetCountries();

        //A - Assert (Resultado)
        Assert.NotNull(countriesResponses);
        Assert.Equal("Hungary", countriesResponses[4].name.common);
    }
}