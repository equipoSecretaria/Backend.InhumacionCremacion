﻿using Backend.InhumacionCremacion.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface IRequestBusiness
    {
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

        ///// <summary>
        ///// Gets the detail request by identifier user.
        ///// </summary>
        ///// <param name="idUser">The identifier user.</param>
        ///// <returns></returns>
        //Task<ResponseBase<List<DTOs.RequestDTO>>> GetDetailRequestByIdUser(string idUser);
    }
}
