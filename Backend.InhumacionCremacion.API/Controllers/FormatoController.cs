using System.Threading.Tasks;
using Backend.InhumacionCremacion.BusinessRules;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Microsoft.AspNetCore.Mvc;

namespace Backend.InhumacionCremacion.API.Controllers
{
    /// <summary>
    /// FormatoController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class FormatoController : ControllerBase
    {
        #region Attributes        
        /// <summary>
        /// The request business
        /// </summary>
        private readonly IFormatoBusiness FormatoBusiness;

        #endregion
        
        
        #region Cosnstructor                
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestController"/> class.
        /// </summary>
        /// <param name="requestBusiness">The request business.</param>
        /// <param name="updateRequestBusiness">The update request business.</param>
        public FormatoController(IFormatoBusiness formatoBusiness)
        {
            FormatoBusiness = formatoBusiness;
            
        }
        #endregion
        
        
        
        #region Methods        

        /// <summary>
        /// GetFormatoByTipoPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        [HttpGet("GetFormatoByTipoPlantilla/{idPlantilla}")]
        public async Task<ActionResult> GetFormatoByTipoPlantilla(string idPlantilla)
        {
            var result = await FormatoBusiness.GetFormatoByTipoPlantilla(idPlantilla);
            return StatusCode(result.Code, result);
        }
        #endregion
    }
}