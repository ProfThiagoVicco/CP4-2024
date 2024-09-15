using System.Net;
using CP4.Services.CountryServices;
using CP4.Services.CountryServices.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CP4.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CountriesController(ICountryService countryService) : ControllerBase
{
    /// <summary>
    /// Retorna a lista completa de países disponíveis.
    /// </summary>
    /// <remarks>
    /// Exemplo de solicitação:
    /// 
    ///     GET /api/countries
    /// 
    /// Esse endpoint retorna uma lista de todos os países disponíveis no sistema.
    /// </remarks>
    /// <response code="200">Retorna a lista de países</response>
    /// <response code="204">Nenhum país encontrado</response>
    /// <response code="503">Serviço indisponível</response>
    /// <response code="500">Erro interno no servidor</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CountriesResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await countryService.GetCountries();
            if (countries.Count == 0)
            {
                return NoContent(); 
            }

            return Ok(countries); 
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable, "Não foi possível obter a lista de países.");
        }
    }

    /// <summary>
    /// Retorna os detalhes de um país específico baseado no nome fornecido.
    /// </summary>
    /// <remarks>
    /// Exemplo de solicitação:
    /// 
    ///     GET /api/countries/getbyname?country=Brazil
    /// 
    /// O nome do país é insensível a maiúsculas/minúsculas.
    /// </remarks>
    /// <param name="country">O nome do país a ser buscado.</param>
    /// <response code="200">Retorna os detalhes do país encontrado</response>
    /// <response code="204">Nenhum país encontrado com o nome fornecido</response>
    /// <response code="503">Serviço indisponível</response>
    /// <response code="500">Erro interno no servidor</response>
    [HttpGet]
    [Route("getbyname")]
    [ProducesResponseType(typeof(List<CountriesResponse>),(int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCountryByName(string country)
    {   
        if (countryService == null)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable, "Serviço de países não disponível.");
        }

        var countries = await countryService.GetCountries();

        if (countries == null)
        {
            return StatusCode((int)HttpStatusCode.ServiceUnavailable, "Dados de países indisponíveis.");
        }

        var countryResponse = countries.FirstOrDefault(c => c.name.common.Equals(country, StringComparison.OrdinalIgnoreCase));

        if (countryResponse != null)
        {
            return Ok(countryResponse);
        }

        return NoContent();
    }
}