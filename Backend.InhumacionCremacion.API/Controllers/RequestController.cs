using System.Threading.Tasks;
using Backend.InhumacionCremacion.BusinessRules;
using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.AspNetCore.Cors;
using System;

namespace Backend.InhumacionCremacion.API.Controllers
{
    /// <summary>
    ///     RequestController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_ToOracle")]
    //[EnableCors(origins: "http://example.com", headers: "*", methods: "*")]
    public class RequestController : ControllerBase
    {
        #region Cosnstructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="RequestController" /> class.
        /// </summary>
        /// <param name="requestBusiness">The request business.</param>
        /// <param name="updateRequestBusiness">The update request business.</param>
        public RequestController(IRequestBusiness requestBusiness,
            IUpdateRequestBusiness updateRequestBusiness)
        {
            RequestBusiness = requestBusiness;
            UpdateRequestBusiness = updateRequestBusiness;
        }

        #endregion

        #region Attributes

        /// <summary>
        ///     The request business
        /// </summary>
        private readonly IRequestBusiness RequestBusiness;

        /// <summary>
        ///     The update request business
        /// </summary>
        private readonly IUpdateRequestBusiness UpdateRequestBusiness;


        #endregion

        #region Methods

        /// <summary>
        ///     Adds the rquest.
        /// </summary>
        /// <param name="requestDTO">The request dto.</param>
        /// <returns></returns>
        /// 

        [HttpGet("ConsultarLicencia/{numero}/{tipo}")]
        public async Task<ActionResult> ConsultarLicencia(string numero, string tipo)
        {
            var result = await RequestBusiness.ConsultarLicencia(numero, tipo);
            return StatusCode(result.Code, result);
        }

        [HttpPost("ModificarCementerio/{numero}/{persona}/{nombre}")]
        public async Task<ActionResult> ModificarCementerio(string numero, string persona,string nombre)
        {
            var result = await RequestBusiness.ModificarCementerio(numero, persona,nombre);
            return StatusCode(result.Code, result);
        }

        [HttpGet("ConsultarFallecidoGestion/{numero}/{id}")]
        public async Task<ActionResult> ConsultarFallecidoGestion(string numero, string id)
        {
            var result = await RequestBusiness.ConsultarFallecidoGestion(numero, id);
            return StatusCode(result.Code, result);
        }

        [HttpGet("ConsultarFallecido/{numero}/{persona}")]
        public async Task<ActionResult> ConsultarFallecido(string numero,string persona)
        {
            var result = await RequestBusiness.ConsultarFallecido(numero,persona);
            return StatusCode(result.Code, result);
        }

        [HttpGet("ConsultarCertificado/{numero}")]
        public async Task<ActionResult> ConsultarCertificado(string numero)
        {
            var result = await RequestBusiness.ConsultarCertificado(numero);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdUser
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>

        [HttpPost("AddGestion")]
        public async Task<ActionResult> AddGestion([FromBody] RequestGestionDTO requestGestionDTO)
        {
            var result = await RequestBusiness.AddGestion(requestGestionDTO);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdUser
        /// </summary>
        /// <param name="firma"></param>
        /// <returns></returns>

        [HttpPost("AddFirma")]
        public async Task<ActionResult> AddFirma([FromBody] FirmaUsuariosDTO firma)
        {
            var result = await RequestBusiness.AddFirma(firma);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdUser
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>

        [HttpPost("AddRquest")]
        public async Task<ActionResult> AddRquest([FromBody] RequestDTO requestDTO)
        {
            var result = await RequestBusiness.AddRequest(requestDTO);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdUser
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("GetRequestByIdUser/{idUser}")]
        public async Task<ActionResult> GetRequestByIdUser(string idUser)
        {
            var result = await RequestBusiness.GetRequestByIdUser(idUser);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdUser
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet("GetByIdUser/{idUser}")]
        public async Task<ActionResult> GetByIdUser(string idUser)
        {
            var result = await RequestBusiness.GetByIdUser(idUser);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     GetRequestByIdEstado
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        [HttpGet("GetRequestByIdEstado/{idEstado}")]
        public async Task<ActionResult> GetRequestByIdEstado(string idEstado)
        {
            var result = await RequestBusiness.GetRequestByIdEstado(idEstado);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// GetRequestById
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        [HttpGet("GetRequestById/{idSolicitud}")]
        public async Task<ActionResult> GetRequestById(string idSolicitud)
        {
            var result = await RequestBusiness.GetRequestById(idSolicitud);
            return StatusCode(result.Code, result);
        }
        /// <summary>
        /// GetRequestById
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        [HttpGet("GetDocumentosRechazados/{idSolicitud}")]
        public async Task<ActionResult> GetDocumentosRechazados(Guid idSolicitud)
        {
            var result = await RequestBusiness.GetDocumentosRechazados(idSolicitud);
            return StatusCode(result.Code, result);
        }


        /// <summary>
        ///     Gets the code ventanilla by identifier user.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <returns></returns>
        [HttpGet("GetCodeVentanillaByIdUser/{idUser}")]
        public async Task<ActionResult> GetCodeVentanillaByIdUser(string idUser)
        {
            var result = await RequestBusiness.GetCodeVentanillaByIdUser(idUser);
            return StatusCode(result.Code, result);
        }

        // <summary>
        ///     GetFuneraria
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFunerariabyidSolicitud/{idSolicitud}")]
        public async Task<ActionResult> GetFuneraria(string idSolicitud)
        {
            var result = await RequestBusiness.GetFuneraria(idSolicitud);
            return StatusCode(result.Code, result);
        }/// 


        /// <summary>
        ///     GetRequestByIdSolicitud
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRequestByIdSolicitud/{idSolicitud}")]
        public async Task<ActionResult> GetRequestByIdSolicitud(string idSolicitud)
        {
            var result = await RequestBusiness.GetRequestByIdSolicitud(idSolicitud);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     UpdateConstante
        /// </summary>
        /// <param name="idConstante"></param>
        /// /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("UpdateConstante/{idConstante}/{value}")]
        public async Task<ActionResult> UpdateConstante(string idConstante, string value)
        {
            var result = await UpdateRequestBusiness.UpdateConstante(idConstante, value);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     UpdateStateRequest
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// /// <param name="idEstado"></param>
        /// <returns></returns>
        [HttpPut("UpdateStateRequest/{idSolicitud}/{idEstado}")]
        public async Task<ActionResult> UpdateStateRequest(string idSolicitud, string idEstado)
        {
            var result = await UpdateRequestBusiness.UpdateStateRequest(idSolicitud, idEstado);
            return StatusCode(result.Code, result);
        }


        /// <summary>
        ///     Updates the request.
        /// </summary>
        /// <param name="solicitudDTO">The solicitud dto.</param>
        /// <returns></returns>
        [HttpPut("UpdateRequest")]
        public async Task<ActionResult> UpdateRequest([FromBody] SolicitudDTO solicitudDTO)
        {
            var result = await UpdateRequestBusiness.UpdateRequest(solicitudDTO);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        ///     Updates the request.
        /// </summary>
        /// <param name="solicitudDTO">The solicitud dto.</param>
        /// <returns></returns>
        /// 
        [HttpPut("UpdateMedico/{idMedico}/{campo}/{cambio}")]
        public async Task<ActionResult> UpdateMedico( string idMedico, string campo, string cambio)
        {
            var result = await UpdateRequestBusiness.UpdateMedico(idMedico, campo, cambio);
            return StatusCode(result.Code, result);
        }


        /// <summary>
        /// GetAllRequest
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRequest")]
        public async Task<ActionResult> GetAllRequest()
        {
            var result = await RequestBusiness.GetAllRequest();
            return StatusCode(result.Code, result);
        }
        
        
        
        /// <summary>
        /// GetAllRequest
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetResumenSolicitud/{idSolicitud}")]
       
        public async Task<ActionResult> GetResumenSolicitud(string idSolicitud)
        {
            var result = await RequestBusiness.GetResumenSolicitud(idSolicitud);
            return StatusCode(result.Code, result);
        }

        ///// <summary>
        ///// Get Maximo Num. Inh-Licencias
        ///// </summary>
        ///// <param name="requestDTO">The request dto.</param>
        ///// <returns></returns>
        //[HttpGet("GetMaxNumInhLicencias")]
        //public async Task<ActionResult> MaxNumInhLicencias()
        //{
        //    var result = await RequestBusiness.GetMaxNumInhLicencias();
        //    return StatusCode(result.Code, result);
        //}

        /// <summary>
        /// Execute a Query into SQL Database
        /// </summary>
        /// <param name="idSolicitud">The request dto.</param>
        /// <param name="idTipoPersona">The request dto.</param>
        /// <returns></returns>
        [HttpOptions("GetDataFromQuery/{idSolicitud}")]
        public async Task<ActionResult> GetDataFromInhumacionQuery(string idSolicitud)
        {
            var result = await RequestBusiness.GetDataFromInhumacionQuery(idSolicitud);
            return StatusCode(result.Code, result);
        }
        
        /// <summary>
        /// Execute a Query into SQL Database
        /// </summary>
        /// <param name="idSolicitud">The request dto.</param>
        /// <returns></returns>
        [HttpGet("GetInfoFallecido/{idSolicitud}")]
        public async Task<ActionResult> GetInfoFallecidoSolicitud(string idSolicitud)
        {
            var result = await RequestBusiness.GetInfoFallecidoByIdSol(idSolicitud);
            return StatusCode(result.Code, result);
        }          




        #endregion
    }
}