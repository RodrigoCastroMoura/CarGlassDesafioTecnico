using CarGlassDesafioTecnico.Application.Usecases.Usuarios.Read;
using CarGlassDesafioTecnico.Domain.Data;
using CarGlassDesafioTecnico.Dto;

using Microsoft.AspNetCore.Mvc;

namespace CarGlassDesafioTecnico.Api.Controllers.v1;

[ApiVersion("1.0")]
[Route("Decompor")]
[ApiController]
[Produces("application/json")]
public class DecomporController :  ControllerBase
{

    private readonly IDecomporNumeroUsecases iDecomporNumeroUsecases;

    public DecomporController(IDecomporNumeroUsecases iDecomporNumeroUsecases)
    {
        this.iDecomporNumeroUsecases = iDecomporNumeroUsecases;
    }

    /// <summary>
    /// Decompor Numeros
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    /// Get/Decompor/45
    ///
    /// </remarks>
    /// <param name="numero"></param>
    /// <returns>returns a decompor</returns>
    /// <response code="200">Returns a decompor  </response>
    [HttpGet("{numero}")]
    [ProducesResponseType(typeof(DecomposicaoNumeroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<ServiceResponse<DecomposicaoNumeroDto>>> GetDecompor([FromRoute] int numero)
    {
        var response = await iDecomporNumeroUsecases.Execute(numero);

        if (response.Success)
        {
            return Ok(response.Data);
        }
        return NotFound(response.Message);
    }

}
