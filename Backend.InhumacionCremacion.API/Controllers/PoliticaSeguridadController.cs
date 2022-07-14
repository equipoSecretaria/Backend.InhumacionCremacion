
using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.API.Controllers
{
    /// <summary>
    /// GeneratePDFController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PoliticaSeguridadController : ControllerBase
    {
        /// <summary>
        /// The generate PDF business
        /// </summary>
        private readonly IPoliticaSeguridadBusiness _politicaSeguridadBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliticaSeguridadController"/> class.
        /// </summary>
        /// <param name="politicaSeguridadBusiness">The generate PDF business.</param>
        public PoliticaSeguridadController(IPoliticaSeguridadBusiness politicaSeguridadBusiness)
        {
            _politicaSeguridadBusiness = politicaSeguridadBusiness;
        }

        /// <summary>
        /// Generates the PDF.
        /// </summary>
        /// <returns></returns>

        [HttpPost("AddPoliticaSeguridad")]
        public async Task<ActionResult> AddPoliticaSeguridad(PoliticaSeguridadDTO politicaSeguridadDTO)
        {
            var result = await _politicaSeguridadBusiness.AddPoliticaSeguridad(politicaSeguridadDTO);

            return StatusCode(result.Code, result); ;
        }

        /// <summary>
        /// Generates the PDF.
        /// </summary>
        /// /// <param name="idPoliticaSeguridad"></param>
        /// <returns></returns>

        [HttpGet("GetPoliticaSeguridad/{idPoliticaSeguridad}")]
        public async Task<ActionResult> GetPoliticaSeguridad(string idPoliticaSeguridad)
        {
            var result = await _politicaSeguridadBusiness.GetPoliticaSeguridad(idPoliticaSeguridad);

            return StatusCode(result.Code, result); ;
        }
    }
}
