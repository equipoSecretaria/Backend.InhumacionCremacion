﻿using Backend.InhumacionCremacion.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface ISeguimientoBusiness
    {
        /// <summary>
        /// GetSeguimientoBySolicitud
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        Task<ResponseBase<List<Entities.DTOs.SeguimientoDto>>> GetSeguimientoBySolicitud(string idSolicitud);

        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="seguimiento"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> AddSeguimiento(Models.InhumacionCremacion.Seguimiento seguimiento);
    }
}
