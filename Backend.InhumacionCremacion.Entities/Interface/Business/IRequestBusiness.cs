using System;
using Backend.InhumacionCremacion.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface IRequestBusiness
    {
        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="requestGestionDTO">The request dto.</param>
        /// <returns></returns>
        /// 
        Task<ResponseBase<string>> ConsultarFallecido(string numero,string persona);
        Task<ResponseBase<string>> AddGestion(DTOs.RequestGestionDTO requestGestionDTO);
        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="requestDTO">The request dto.</param>
        /// <returns></returns>
        Task<ResponseBase<string>> AddRequest(DTOs.RequestDTO requestDTO);

        /// <summary>
        /// GetRequestByIdUser
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<ResponseBase<List<DTOs.RequestDetailDTO>>> GetRequestByIdUser(string idUser);

        /// <summary>
        /// GetByIdUser
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<ResponseBase<List<DTOs.RequestDetailDTOUser>>> GetByIdUser(string idUser);

        /// <summary>
        /// Gets the code ventanilla by identifier user.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <returns></returns>
        Task<ResponseBase<int>> GetCodeVentanillaByIdUser(string idUser);
        
        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestById(string idSolicitud);

        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idEstado">The identifier solicitud.</param>
        /// <returns></returns>
        Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestByIdEstado(string idEstado);
        
        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestByIdSolicitud(string idSolicitud);

        ///// <summary>
        ///// Gets the detail request by identifier user.
        ///// </summary>
        ///// <param name="idUser">The identifier user.</param>
        ///// <returns></returns>
        //Task<ResponseBase<List<DTOs.RequestDTO>>> GetDetailRequestByIdUser(string idUser);

        /// <summary>
        /// GetAllRequest
        /// </summary>
        /// <returns></returns>
        Task<ResponseBase<List<DTOs.RequestDetailDTO>>> GetAllRequest();
        
        /// <summary>
        /// GetResumenSolicitud
        /// </summary>
        /// <returns></returns>
        Task<ResponseBase<List<ResumenSolicitud>>> GetResumenSolicitud(string idSolicitud);
		Task<string> GetMaxNumInhLicencias();

		Task<ResponseBase<dynamic>> GetDataFromInhumacionQuery(string idSolicitud);   
		Task<ResponseBase<dynamic>> GetInfoFallecidoByIdSol(string idSolicitud);     

        Task<string> UpdateRsumenSolicitud(ResumenSolicitud idSolicitud);

        Task<ResponseBase<List<DatosFuneraria>>> GetFuneraria(string idSolicitud);





    }
}
