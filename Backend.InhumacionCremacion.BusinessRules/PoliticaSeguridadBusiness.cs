using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Backend.InhumacionCremacion.Entities.Models.Commons;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class PoliticaSeguridadBusiness : IPoliticaSeguridadBusiness
    {
        private readonly ITelemetryException _telemetryException;
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<PoliticaSeguridad> _repositoryPoliticaSeguridad;

        public PoliticaSeguridadBusiness(ITelemetryException telemetryException,
                               Entities.Interface.Repository.IBaseRepositoryCommons<PoliticaSeguridad> repositoryPoliticaSeguridad
                               )
        {
            _telemetryException = telemetryException;
            _repositoryPoliticaSeguridad  = repositoryPoliticaSeguridad;

        }


        public async Task<ResponseBase<int>> AddPoliticaSeguridad(PoliticaSeguridadDTO politicaSeguridadDTO)
        {
            try
            {
                // Identificadores
                Guid idPoliticaSeguridad = Guid.NewGuid();
               

                    int result = await _repositoryPoliticaSeguridad.AddAsync(new Entities.Models.Commons.PoliticaSeguridad
                    {

                        idPoliticaSeguridad = idPoliticaSeguridad,
                        estado = true,
                        fecha = politicaSeguridadDTO.fecha,
                        usuario = politicaSeguridadDTO.usuario
                        
                    });

                    return new ResponseBase<int>(code: System.Net.HttpStatusCode.OK, message: "Acuerdo de politica de seguridad registrada", data:result);
                


            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                _telemetryException.RegisterException(ex);
                return new ResponseBase<int>(code: System.Net.HttpStatusCode.InternalServerError, message: msg, data: 0);
            }
        }

        public async Task<ResponseBase<PoliticaSeguridad>> GetPoliticaSeguridad(string idPoliticaSeguridad )
        {
            try
            {

                var politicaSeguridad = await _repositoryPoliticaSeguridad.GetAsync(p => p.idPoliticaSeguridad == Guid.Parse(idPoliticaSeguridad));


               if(politicaSeguridad != null)
                {
                    return new ResponseBase<PoliticaSeguridad>(code: System.Net.HttpStatusCode.OK, message: "Politica de seguridad encontrada", data: politicaSeguridad);
                }
                else
                {
                    return new ResponseBase<PoliticaSeguridad>(code: System.Net.HttpStatusCode.NotFound, message: "Politica de seguridad no encontrada");
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                _telemetryException.RegisterException(ex);
                return new ResponseBase<PoliticaSeguridad>(code: System.Net.HttpStatusCode.InternalServerError, message: msg);
            }
        }


    }

}
