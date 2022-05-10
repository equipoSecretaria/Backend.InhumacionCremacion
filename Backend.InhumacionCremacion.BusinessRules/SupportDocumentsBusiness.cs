using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class SupportDocumentsBusiness : Entities.Interface.Business.ISupportDocumentsBusiness
    {

        #region Attributes

        /// <summary>
        /// The telemetry exception
        /// </summary>
        private readonly ITelemetryException _telemetryException;

        /// <summary>
        /// The repository ubicacion persona
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DocumentosSoporte> _repositoryDocumentosSoporte;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportDocumentsBusiness"/> class.
        /// </summary>
        /// <param name="telemetryException">The telemetry exception.</param>
        /// <param name="repositoryDocumentosSoporte">The repository documentos soporte.</param>
        public SupportDocumentsBusiness(ITelemetryException telemetryException,
                                        Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DocumentosSoporte> repositoryDocumentosSoporte)
        {
            _telemetryException = telemetryException;
            _repositoryDocumentosSoporte = repositoryDocumentosSoporte;
        }
        #endregion

        #region Methods
        public async Task<ResponseBase<bool>> AddSupportDocuments(List<RequestSupportDocumentsDTO> requestSupportDocumentsDTO)
        {
            try
            {
                if (requestSupportDocumentsDTO == null)
                {
                    return new ResponseBase<bool>(code: HttpStatusCode.OK, message: "Los Campos son obligatorios", data: false);
                }
                foreach (var item in requestSupportDocumentsDTO)
                {
                    await _repositoryDocumentosSoporte.AddAsync(new Entities.Models.InhumacionCremacion.DocumentosSoporte
                    {
                        IdDocumentoSoporte = Guid.NewGuid(),
                        IdSolicitud = System.Guid.Parse(item.IdSolicitud),
                        FechaRegistro = System.DateTime.Now,
                        FechaModificacion = System.DateTime.Now,
                        IdTipoDocumentoSoporte = item.IdTipoDocumentoSoporte,
                        IdUsuario = item.IdUsuario,
                        Path = item.Path
                    });
                }
                return new ResponseBase<bool>(code: HttpStatusCode.OK, message: "Solicitud OK", data: true);
            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<bool>(code: HttpStatusCode.InternalServerError, message: ex.Message, data: false);
            }
        }

        /// <summary>
        /// Gets all suport by request identifier.
        /// </summary>
        /// <param name="IdSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ResponseBase<List<DocumentosSoporte>>> GetAllSuportByRequestId(string IdSolicitud)
        {
            try
            {
                var result = await _repositoryDocumentosSoporte.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(IdSolicitud)));

                if (result == null)
                {
                    return new ResponseBase<List<DocumentosSoporte>>(code: HttpStatusCode.OK, message: "No se encontraron resultados");
                }
                return new ResponseBase<List<DocumentosSoporte>>(code: HttpStatusCode.OK, message: "Solicitud OK", data: result.OrderBy(x => x.Path).ToList(), count: result.Count());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<DocumentosSoporte>>(code: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        public async Task<ResponseBase<bool>> UpdateSuport(List<DocumentosSoporte> documentosSoporte)
        {
            try
            {
                foreach (var item in documentosSoporte)
                {
                    var suportBD = await _repositoryDocumentosSoporte.GetAsync(w => w.IdDocumentoSoporte.Equals(item.IdDocumentoSoporte));

                    if (suportBD == null)
                    {
                        await _repositoryDocumentosSoporte.AddAsync(new Entities.Models.InhumacionCremacion.DocumentosSoporte
                        {
                            IdDocumentoSoporte = Guid.NewGuid(),
                            IdSolicitud = item.IdSolicitud,
                            FechaRegistro = System.DateTime.Now,
                            FechaModificacion = System.DateTime.Now,
                            IdTipoDocumentoSoporte = item.IdTipoDocumentoSoporte,
                            IdUsuario = item.IdUsuario,
                            Path = item.Path
                        });
                    }
                    else
                    {
                        suportBD.FechaModificacion = item.FechaModificacion;
                        await _repositoryDocumentosSoporte.UpdateAsync(suportBD);
                    }
                }

                return new ResponseBase<bool>(code: HttpStatusCode.OK, message: "Solicitud OK", data: true);
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<bool>(code: HttpStatusCode.InternalServerError, message: ex.Message, data: false);
            }
        }
        #endregion
    }
}