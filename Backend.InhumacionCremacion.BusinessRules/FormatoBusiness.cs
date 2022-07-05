using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using Microsoft.AspNetCore.Mvc;

namespace Backend.InhumacionCremacion.BusinessRules
{
    
    /// <summary>
    /// AddRequest
    /// </summary>
    /// <seealso cref="Backend.InhumacionCremacion.Entities.Interface.Business.IFormatoBusiness" />
    public class FormatoBusiness : Entities.Interface.Business.IFormatoBusiness
    {
        
        /// <summary>
        /// The telemetry exception
        /// </summary>
        private readonly ITelemetryException _telemetryException;
        
        /// <summary>
        /// The repository solicitud
        /// </summary>
        private readonly
            Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<
                Entities.Models.InhumacionCremacion.Formatos> _repositoryFormato;


        #region Constructor

        public FormatoBusiness(ITelemetryException telemetryException,
            Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<
                Entities.Models.InhumacionCremacion.Formatos> repositoryFormato)
        {
            _repositoryFormato = repositoryFormato;
        }

        #endregion

        #region Methods





        /// <summary>
        ///     GetFormatoByTipoPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        [HttpGet("GetFormatoByTipoPlantilla/{IdPlantilla}")]
        public async Task<ResponseBase<dynamic>> GetFormatoByTipoPlantilla(string IdPlantilla)
        {
            try
            {
                var result = await _repositoryFormato.GetAsync(p => p.IdPlantilla.Equals(Guid.Parse(IdPlantilla)));
                if (result == null)
                {
                    return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "No se encontraron resultados");

                }

                var formatoDTO = new Entities.DTOs.FormatoDTO()
                {
                    
                   
                    AsuntoNotificacion = result.AsuntoNotificacion,
                    TipoPDF = result.TipoPDF,
                    valor = result.valor,
                    


                };
                return new ResponseBase<dynamic>(HttpStatusCode.OK, "Formato Ok", formatoDTO);

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<dynamic>(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #endregion
        
    }
}