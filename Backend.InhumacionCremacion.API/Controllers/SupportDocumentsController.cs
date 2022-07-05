using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.AspNetCore.Mvc;

namespace Backend.InhumacionCremacion.API.Controllers
{
    /// <summary>
    ///     SupportDocumentsController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class SupportDocumentsController : ControllerBase
    {
        #region Attributes

        /// <summary>
        ///     The request business
        /// </summary>
        private readonly ISupportDocumentsBusiness _supportDocumentsBusiness;

        #endregion

        #region Cosnstructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SupportDocumentsController" /> class.
        /// </summary>
        /// <param name="supportDocumentsBusiness">The support documents business.</param>
        public SupportDocumentsController(ISupportDocumentsBusiness supportDocumentsBusiness)
        {
            _supportDocumentsBusiness = supportDocumentsBusiness;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the support documents.
        /// </summary>
        /// <param name="requestSupportDocumentsDTO">The request support documents dto.</param>
        /// <returns></returns>
        [HttpPost("AddSupportDocuments")]
        public async Task<ActionResult> AddSupportDocuments(
            [FromBody] List<RequestSupportDocumentsDTO> requestSupportDocumentsDTO)
        {
            var result = await _supportDocumentsBusiness.AddSupportDocuments(requestSupportDocumentsDTO);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     Adds the support documents.
        /// </summary>
        /// <param name="IdSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        [HttpGet("GetAllSuportByRequestId/{IdSolicitud}")]
        public async Task<ActionResult> GetAllSuportByRequestId(string IdSolicitud)
        {
            var result = await _supportDocumentsBusiness.GetAllSuportByRequestId(IdSolicitud);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     Updates the suport.
        /// </summary>
        /// <param name="documentosSoporte">The documentos soporte.</param>
        /// <returns></returns>
        [HttpPut("UpdateSuport")]
        public async Task<ActionResult> UpdateSuport([FromBody] List<DocumentosSoporte> documentosSoporte)
        {
            var result = await _supportDocumentsBusiness.UpdateSuport(documentosSoporte);
            return StatusCode(result.Code, result);
        }

        #endregion
    }
}