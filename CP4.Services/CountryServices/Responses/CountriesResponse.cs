using Newtonsoft.Json.Serialization;

namespace CP4.Services.CountryServices.Responses;

public class CountriesResponse
{
    public FlagsResponse flags { get; set; }
    public NameResponse name { get; set; }
}