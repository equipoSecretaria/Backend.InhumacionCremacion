using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;

namespace Backend.InhumacionCremacion.BusinessRules
{
    /// <summary>
    /// AddRequest
    /// </summary>
    /// <seealso cref="Backend.InhumacionCremacion.Entities.Interface.Business.IRequestBusiness" />
    public class RequestBusiness : Entities.Interface.Business.IRequestBusiness
    {
        #region Attributes

        /// <summary>
        /// The telemetry exception
        /// </summary>
        private readonly ITelemetryException _telemetryException;

        /// <summary>
        /// The repository ubicacion persona
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.UbicacionPersona> _repositoryUbicacionPersona;

        /// <summary>
        /// The repository persona
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> _repositoryPersona;

        /// <summary>
        /// The repository lugar defuncion
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.LugarDefuncion> _repositoryLugarDefuncion;

        /// <summary>
        /// The repository datos cementerio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> _repositoryDatosCementerio;

        /// <summary>
        /// The repository institucion certifica fallecimiento
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.InstitucionCertificaFallecimiento> _repositoryInstitucionCertificaFallecimiento;

        /// <summary>
        /// The repository solicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> _repositorySolicitud;

        

        /// <summary>
        /// _repositoryDominio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> _repositoryDominio;


        /// <summary>
        /// The repository Resumen Solicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.EstadoDocumentosSoporte> _repositoryEstadoDocumentosSoporte;


        /// <summary>
        /// The repository Resumen Solicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.ResumenSolicitud> _repositoryResumenSolicitud;

        #endregion

        #region Constructor                                                
        /// <summary>
        /// RequestBusiness
        /// </summary>
        /// <param name="telemetryException"></param>
        /// <param name="repositoryDominio"></param>
        /// <param name="repositorySolicitud"></param>
        /// <param name="repositoryDatosCementerio"></param>
        /// <param name="repositoryInstitucionCertificaFallecimiento"></param>
        /// <param name="repositoryLugarDefuncion"></param>
        /// <param name="repositoryPersona"></param>
        /// <param name="repositoryUbicacionPersona"></param>
        public RequestBusiness(ITelemetryException telemetryException,
                               Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> repositoryDominio,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> repositorySolicitud,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> repositoryDatosCementerio,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.InstitucionCertificaFallecimiento> repositoryInstitucionCertificaFallecimiento,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.LugarDefuncion> repositoryLugarDefuncion,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> repositoryPersona,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.UbicacionPersona> repositoryUbicacionPersona,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.ResumenSolicitud> repositoryResumenSolicitud,
                               Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.EstadoDocumentosSoporte> repositoryEstadoDocumentosSoporte
            )
        {
            _telemetryException = telemetryException;
            _repositorySolicitud = repositorySolicitud;
            _repositoryDatosCementerio = repositoryDatosCementerio;
            _repositoryInstitucionCertificaFallecimiento = repositoryInstitucionCertificaFallecimiento;
            _repositoryLugarDefuncion = repositoryLugarDefuncion;
            _repositoryPersona = repositoryPersona;
            _repositoryUbicacionPersona = repositoryUbicacionPersona;
            _repositoryDominio = repositoryDominio;
            _repositoryResumenSolicitud = repositoryResumenSolicitud;
            _repositoryEstadoDocumentosSoporte = repositoryEstadoDocumentosSoporte;
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the request.
        /// </summary>
        /// <param name="requestDTO">The request dto.</param>
        /// <returns></returns>
        /// 

        public async Task<ResponseBase<string>> AddGestion(Entities.DTOs.RequestGestionDTO requestGestionDTO)
        {
            try
            {
                
                    Guid IdEstadoDocumento = Guid.NewGuid();
                    await _repositoryEstadoDocumentosSoporte.AddAsync(new Entities.Models.InhumacionCremacion.EstadoDocumentosSoporte
                    {
                        IdEstadoDocumento = IdEstadoDocumento,
                        IdSolicitud = requestGestionDTO.estado.IdSolicitud,
                        IdDocumentoSoporte = requestGestionDTO.estado.IdDocumentoSoporte,
                        Path = requestGestionDTO.estado.Path,
                        Observaciones = requestGestionDTO.estado.Observaciones,
                        Estado_Documento = requestGestionDTO.estado.Estado_Documento

                    });
                    
                
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.OK, message: "Solicitud OK");


            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }


        public async Task<ResponseBase<string>> AddRequest(Entities.DTOs.RequestDTO requestDTO)
        {
            try
            {
                //datos cementerio
                Guid IdDatosCementerio = Guid.NewGuid();
                await _repositoryDatosCementerio.AddAsync(new Entities.Models.InhumacionCremacion.DatosCementerio
                {
                    IdDatosCementerio = IdDatosCementerio,
                    EnBogota = requestDTO.Solicitud.DatosCementerio.EnBogota,
                    FueraBogota = requestDTO.Solicitud.DatosCementerio.FueraBogota,
                    Cementerio = requestDTO.Solicitud.DatosCementerio.Cementerio,
                    OtroSitio = requestDTO.Solicitud.DatosCementerio.OtroSitio,
                    Ciudad = requestDTO.Solicitud.DatosCementerio.Ciudad,
                    IdPais = requestDTO.Solicitud.DatosCementerio.IdPais,
                    IdDepartamento = requestDTO.Solicitud.DatosCementerio.IdDepartamento,
                    IdMunicipio = requestDTO.Solicitud.DatosCementerio.IdMunicipio

                });

                //institucion que certifica el fallecemiento
                Guid IdInstitucionCertificaFallecimiento = Guid.NewGuid();

                await _repositoryInstitucionCertificaFallecimiento.AddAsync(new Entities.Models.InhumacionCremacion.InstitucionCertificaFallecimiento
                {
                    IdInstitucionCertificaFallecimiento = IdInstitucionCertificaFallecimiento,
                    TipoIdentificacion = requestDTO.Solicitud.InstitucionCertificaFallecimiento.TipoIdentificacion,
                    NumeroIdentificacion = requestDTO.Solicitud.InstitucionCertificaFallecimiento.NumeroIdentificacion,
                    RazonSocial = requestDTO.Solicitud.InstitucionCertificaFallecimiento.RazonSocial,
                    NumeroProtocolo = requestDTO.Solicitud.InstitucionCertificaFallecimiento.NumeroProtocolo,
                    NumeroActaLevantamiento = requestDTO.Solicitud.InstitucionCertificaFallecimiento.NumeroActaLevantamiento,
                    FechaActa = requestDTO.Solicitud.InstitucionCertificaFallecimiento.FechaActa,
                    SeccionalFiscalia = requestDTO.Solicitud.InstitucionCertificaFallecimiento.SeccionalFiscalia,
                    NoFiscal = requestDTO.Solicitud.InstitucionCertificaFallecimiento.NoFiscal,
                    IdTipoInstitucion = requestDTO.Solicitud.InstitucionCertificaFallecimiento.IdTipoInstitucion,
                });

                //lugar de defuncion
                Guid IdLugarDefuncion = Guid.NewGuid();
                await _repositoryLugarDefuncion.AddAsync(new Entities.Models.InhumacionCremacion.LugarDefuncion
                {
                    IdLugarDefuncion = IdLugarDefuncion,
                    IdPais = requestDTO.Solicitud.LugarDefuncion.IdPais,
                    IdDepartamento = requestDTO.Solicitud.LugarDefuncion.IdDepartamento,
                    IdMunicipio = requestDTO.Solicitud.LugarDefuncion.IdMunicipio,
                    IdAreaDefuncion = requestDTO.Solicitud.LugarDefuncion.IdAreaDefuncion,
                    IdSitioDefuncion = requestDTO.Solicitud.LugarDefuncion.IdSitioDefuncion

                });

                //almacenamiento datos de la solicitud
                Guid IdSolicitud = Guid.NewGuid();
                await _repositorySolicitud.AddAsync(new Entities.Models.InhumacionCremacion.Solicitud
                {
                    IdSolicitud = IdSolicitud,
                    NumeroCertificado = requestDTO.Solicitud.NumeroCertificado,
                    FechaDefuncion = requestDTO.Solicitud.FechaDefuncion,
                    SinEstablecer = requestDTO.Solicitud.SinEstablecer,
                    Hora = requestDTO.Solicitud.Hora,
                    IdSexo = requestDTO.Solicitud.IdSexo,
                    FechaSolicitud = DateTime.Now,
                    EstadoSolicitud = requestDTO.Solicitud.EstadoSolicitud,
                    IdPersonaVentanilla = requestDTO.Solicitud.IdPersonaVentanilla,
                    IdUsuarioSeguridad = requestDTO.Solicitud.IdUsuarioSeguridad,
                    IdTramite = requestDTO.Solicitud.IdTramite,
                    IdLugarDefuncion = IdLugarDefuncion,
                    IdTipoMuerte = requestDTO.Solicitud.IdTipoMuerte,
                    IdDatosCementerio = IdDatosCementerio,
                    IdInstitucionCertificaFallecimiento = IdInstitucionCertificaFallecimiento
                });

                //ubicacion persona
                // en el front, para los valores nulos se debe enciar el siguiente valor: "00000000-0000-0000-0000-000000000000"
                //342d934b-c316-46cb-a4f3-3aac5845d246 tipo persona madre
                Guid IdUbicacionPersona = Guid.Empty;

                //ResumenSolicitud 
                //ULTIMO CAMBIO - PRUEBA cambioasdas
                Guid NumeroTramite = Guid.NewGuid();
                Guid EstadoSolicitud = Guid.NewGuid();
                // HAY QUE PROBAR ESTE PUNTO
                await _repositoryResumenSolicitud.AddAsync(new Entities.Models.InhumacionCremacion.ResumenSolicitud
                { 
                    IdSolicitud = IdSolicitud,
                    NumeroTramite = NumeroTramite,
                    EstadoSolicitud = EstadoSolicitud,
                    NumeroLicencia = requestDTO.Solicitud.ResumenSolicitud.NumeroLicencia,
                    CorreoSolicitante = requestDTO.Solicitud.ResumenSolicitud.CorreoSolicitante,
                    CorreoFuneraria = requestDTO.Solicitud.ResumenSolicitud.CorreoFuneraria,
                    CorreoCementerio = requestDTO.Solicitud.ResumenSolicitud.CorreoCementerio,
                    CorreoMedico = requestDTO.Solicitud.ResumenSolicitud.CorreoMedico,
                    TipoSeguimiento = requestDTO.Solicitud.ResumenSolicitud.TipoSeguimiento,
                    NombreSolicitante = requestDTO.Solicitud.ResumenSolicitud.NombreSolicitante,
                    ApellidoSolicitante = requestDTO.Solicitud.ResumenSolicitud.ApellidoSolicitante,
                    NumeroDocumentoSolicitante = requestDTO.Solicitud.ResumenSolicitud.NumeroDocumentoSolicitante,
                    TipoDocumentoSolicitante = requestDTO.Solicitud.ResumenSolicitud.TipoDocumentoSolicitante,
                });

                foreach (var personas in requestDTO.Solicitud.Persona)
                {
                    if (personas.IdTipoPersona == Guid.Parse("342d934b-c316-46cb-a4f3-3aac5845d246") &&
                        requestDTO.Solicitud.UbicacionPersona.IdPaisResidencia != Guid.Empty &&
                        requestDTO.Solicitud.UbicacionPersona.IdDepartamentoResidencia != Guid.Empty &&
                        requestDTO.Solicitud.UbicacionPersona.IdCiudadResidencia != Guid.Empty &&
                        requestDTO.Solicitud.UbicacionPersona.IdLocalidadResidencia != Guid.Empty &&
                        requestDTO.Solicitud.UbicacionPersona.IdAreaResidencia != Guid.Empty &&
                        requestDTO.Solicitud.UbicacionPersona.IdBarrioResidencia != Guid.Empty)
                    {// si el tipo de persona es madre y los valores son diferentes de: "00000000-0000-0000-0000-000000000000" se inserta la ubicacion
                        IdUbicacionPersona = Guid.NewGuid();
                        await _repositoryUbicacionPersona.AddAsync(new Entities.Models.InhumacionCremacion.UbicacionPersona
                        {
                            IdUbicacionPersona = IdUbicacionPersona,
                            IdPaisResidencia = requestDTO.Solicitud.UbicacionPersona.IdPaisResidencia,
                            IdDepartamentoResidencia = requestDTO.Solicitud.UbicacionPersona.IdDepartamentoResidencia,
                            IdCiudadResidencia = requestDTO.Solicitud.UbicacionPersona.IdCiudadResidencia,
                            IdLocalidadResidencia = requestDTO.Solicitud.UbicacionPersona.IdLocalidadResidencia,
                            IdAreaResidencia = requestDTO.Solicitud.UbicacionPersona.IdAreaResidencia,
                            IdBarrioResidencia = requestDTO.Solicitud.UbicacionPersona.IdBarrioResidencia,
                        });

                        await _repositoryPersona.AddAsync(new Entities.Models.InhumacionCremacion.Persona
                        {
                            IdPersona = Guid.NewGuid(),
                            TipoIdentificacion = personas.TipoIdentificacion,
                            NumeroIdentificacion = personas.NumeroIdentificacion,
                            PrimerNombre = personas.PrimerNombre,
                            SegundoNombre = personas.SegundoNombre,
                            PrimerApellido = personas.PrimerApellido,
                            SegundoApellido = personas.SegundoApellido,
                            FechaNacimiento = personas.FechaNacimiento,
                            Nacionalidad = personas.Nacionalidad,
                            OtroParentesco = personas.OtroParentesco,
                            Estado = true,
                            IdEstadoCivil = personas.IdEstadoCivil,
                            IdNivelEducativo = personas.IdNivelEducativo,
                            IdEtnia = personas.IdEtnia,
                            IdRegimen = personas.IdRegimen,
                            IdTipoPersona = personas.IdTipoPersona,
                            IdSolicitud = IdSolicitud,
                            IdParentesco = personas.IdParentesco,
                            IdLugarExpedicion = personas.IdLugarExpedicion,
                            IdTipoProfesional = personas.IdTipoProfesional,
                            IdUbicacionPersona = IdUbicacionPersona
                        });

                    }
                    else // si el tipo de persona es diferente de la madre no tiene ubicacion
                    {
                        IdUbicacionPersona = Guid.Empty;

                        await _repositoryPersona.AddAsync(new Entities.Models.InhumacionCremacion.Persona
                        {
                            IdPersona = Guid.NewGuid(),
                            TipoIdentificacion = personas.TipoIdentificacion,
                            NumeroIdentificacion = personas.NumeroIdentificacion,
                            PrimerNombre = personas.PrimerNombre,
                            SegundoNombre = personas.SegundoNombre,
                            PrimerApellido = personas.PrimerApellido,
                            SegundoApellido = personas.SegundoApellido,
                            FechaNacimiento = personas.FechaNacimiento,
                            Nacionalidad = personas.Nacionalidad,
                            OtroParentesco = personas.OtroParentesco,
                            Estado = true,
                            IdEstadoCivil = personas.IdEstadoCivil,
                            IdNivelEducativo = personas.IdNivelEducativo,
                            IdEtnia = personas.IdEtnia,
                            IdRegimen = personas.IdRegimen,
                            IdTipoPersona = personas.IdTipoPersona,
                            IdSolicitud = IdSolicitud,
                            IdParentesco = personas.IdParentesco,
                            IdLugarExpedicion = personas.IdLugarExpedicion,
                            IdTipoProfesional = personas.IdTipoProfesional,
                            IdUbicacionPersona = IdUbicacionPersona
                        });
                    }
                }
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.OK, message: "Solicitud OK", data: IdSolicitud.ToString());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        /// <summary>
        /// Gets the code ventanilla by identifier user.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<ResponseBase<int>> GetCodeVentanillaByIdUser(string idUser)
        {
            try
            {
                var resultRequest = await _repositorySolicitud.GetAsync(predicate: p => p.IdUsuarioSeguridad.Equals(Guid.Parse(idUser)),
                                                                        selector: s => new Entities.Models.InhumacionCremacion.Solicitud { IdPersonaVentanilla = s.IdPersonaVentanilla });
                if (resultRequest == null)
                {
                    return new ResponseBase<int>(code: System.Net.HttpStatusCode.OK, message: "No se encontraron resultados");
                }

                return new ResponseBase<int>(code: System.Net.HttpStatusCode.OK, message: "Solicitud ok", data: resultRequest.IdPersonaVentanilla);
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<int>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestById(string idSolicitud)
        {
            try
            {
                var resultSolicitud = await _repositorySolicitud.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idSolicitud)), include: inc => inc
                                                                                                                                   .Include(i => i.IdDatosCementerioNavigation)
                                                                                                                                   .Include(i => i.IdInstitucionCertificaFallecimientoNavigation)
                                                                                                                                   .Include(i => i.Persona)
                                                                                                                                   );

                var resultLugarDefuncion = await _repositoryLugarDefuncion.GetAsync(predicate: p => p.IdLugarDefuncion.Equals(Guid.Parse(resultSolicitud.Select(x => x.IdLugarDefuncion).FirstOrDefault().ToString())));

                List<System.Guid> IdUbicacionPersona = new List<System.Guid>();

                foreach (var persona in resultSolicitud)
                {
                    IdUbicacionPersona.AddRange(persona.Persona.Select(x => x.IdUbicacionPersona.Value));
                }

                var resultUbicacionPersona = await _repositoryUbicacionPersona.GetAsync(w => w.IdPaisResidencia != Guid.Empty && IdUbicacionPersona.Any(a => a.Equals(w.IdUbicacionPersona)));
               

                var resultSol = new List<Entities.DTOs.SolicitudDTO>();

                foreach (var item in resultSolicitud)
                {
                    //solicitud validado
                    Entities.DTOs.SolicitudDTO solicitudDTO = new Entities.DTOs.SolicitudDTO
                    {
                        IdSolicitud = item.IdSolicitud,
                        NumeroCertificado = item.NumeroCertificado,
                        FechaDefuncion = item.FechaDefuncion,
                        SinEstablecer = item.SinEstablecer,
                        Hora = item.Hora,
                        IdSexo = item.IdSexo,
                        FechaSolicitud = item.FechaSolicitud,
                        EstadoSolicitud = item.EstadoSolicitud,
                        IdPersonaVentanilla = item.IdPersonaVentanilla,
                        IdUsuarioSeguridad = item.IdUsuarioSeguridad,
                        IdTramite = item.IdTramite,
                        IdTipoMuerte = item.IdTipoMuerte,

                        //ubicacion persona validado
                        UbicacionPersona = new Entities.DTOs.UbicacionPersonaDTO
                        {
                            IdUbicacionPersona = resultUbicacionPersona?.IdUbicacionPersona,
                            IdPaisResidencia = resultUbicacionPersona?.IdPaisResidencia,
                            IdDepartamentoResidencia = resultUbicacionPersona?.IdDepartamentoResidencia,
                            IdCiudadResidencia = resultUbicacionPersona?.IdCiudadResidencia,
                            IdLocalidadResidencia = resultUbicacionPersona?.IdLocalidadResidencia,
                            IdAreaResidencia = resultUbicacionPersona?.IdAreaResidencia,
                            IdBarrioResidencia = resultUbicacionPersona?.IdBarrioResidencia
                        },

                        Persona = new List<Entities.DTOs.PersonaDTO>(),

                        //datos cementerio validado
                        DatosCementerio = new Entities.DTOs.DatosCementerioDTO
                        {
                            IdDatosCementerio = item.IdDatosCementerio,
                            EnBogota = item.IdDatosCementerioNavigation.EnBogota,
                            FueraBogota = item.IdDatosCementerioNavigation.FueraBogota,
                            FueraPais = item.IdDatosCementerioNavigation.FueraPais,
                            Cementerio = item.IdDatosCementerioNavigation.Cementerio,
                            OtroSitio = item.IdDatosCementerioNavigation.OtroSitio,
                            Ciudad = item.IdDatosCementerioNavigation.Ciudad,
                            IdPais = item.IdDatosCementerioNavigation.IdPais,
                            IdDepartamento = item.IdDatosCementerioNavigation.IdDepartamento,
                            IdMunicipio = item.IdDatosCementerioNavigation.IdMunicipio
                        },
                        //lufar de defuncion validado
                        LugarDefuncion = new Entities.DTOs.LugarDefuncionDTO
                        {
                            IdLugarDefuncion = resultLugarDefuncion.IdLugarDefuncion,
                            IdPais = resultLugarDefuncion.IdPais,
                            IdDepartamento = resultLugarDefuncion.IdDepartamento,
                            IdMunicipio = resultLugarDefuncion.IdMunicipio,
                            IdAreaDefuncion = resultLugarDefuncion.IdAreaDefuncion,
                            IdSitioDefuncion = resultLugarDefuncion.IdSitioDefuncion
                        },

                        //datos intitucion certifica fallecimiento ok
                        InstitucionCertificaFallecimiento = new Entities.DTOs.InstitucionCertificaFallecimientoDTO
                        {
                            IdInstitucionCertificaFallecimiento = item.IdInstitucionCertificaFallecimiento,
                            TipoIdentificacion = item.IdInstitucionCertificaFallecimientoNavigation.TipoIdentificacion,
                            NumeroIdentificacion = item.IdInstitucionCertificaFallecimientoNavigation.NumeroIdentificacion,
                            RazonSocial = item.IdInstitucionCertificaFallecimientoNavigation.RazonSocial,
                            NumeroProtocolo = item.IdInstitucionCertificaFallecimientoNavigation.NumeroProtocolo,
                            NumeroActaLevantamiento = item.IdInstitucionCertificaFallecimientoNavigation.NumeroActaLevantamiento,
                            FechaActa = item.IdInstitucionCertificaFallecimientoNavigation.FechaActa,
                            SeccionalFiscalia = item.IdInstitucionCertificaFallecimientoNavigation.SeccionalFiscalia,
                            NoFiscal = item.IdInstitucionCertificaFallecimientoNavigation.NoFiscal,
                            IdTipoInstitucion = item.IdInstitucionCertificaFallecimientoNavigation.IdTipoInstitucion
                        },


                        
                    };

                    resultSol.Add(solicitudDTO);

                    foreach (var rsp in item.Persona)
                    {
                        //datos persona validado
                        Entities.DTOs.PersonaDTO personaDTO = new Entities.DTOs.PersonaDTO
                        {

                            IdPersona = rsp.IdPersona,
                            TipoIdentificacion = rsp.TipoIdentificacion,
                            NumeroIdentificacion = rsp.NumeroIdentificacion,
                            PrimerNombre = rsp.PrimerNombre,
                            SegundoNombre = rsp.SegundoNombre,
                            PrimerApellido = rsp.PrimerApellido,
                            SegundoApellido = rsp.SegundoApellido,
                            FechaNacimiento = rsp.FechaNacimiento,
                            Nacionalidad = rsp.Nacionalidad,
                            OtroParentesco = rsp.OtroParentesco,
                            Estado = rsp.Estado,
                            IdEstadoCivil = rsp.IdEstadoCivil,
                            IdNivelEducativo = rsp.IdNivelEducativo,
                            IdEtnia = rsp.IdEtnia,
                            IdRegimen = rsp.IdRegimen,
                            IdTipoPersona = rsp.IdTipoPersona,
                            IdSolicitud = rsp.IdSolicitud,
                            IdParentesco = rsp.IdParentesco,
                            IdLugarExpedicion = rsp.IdLugarExpedicion,
                            IdTipoProfesional = rsp.IdTipoProfesional,
                            IdUbicacionPersona = rsp.IdUbicacionPersona
                        };
                        solicitudDTO.Persona.Add(personaDTO);
                    }
                }
                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(code: System.Net.HttpStatusCode.OK, message: "Solicitud OK", data: resultSol.ToList());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        /// <summary>
        /// GetRequestByIdUser
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.RequestDetailDTO>>> GetRequestByIdUser(string idUser)
        {
            try
            {
                var resultRequest = await _repositorySolicitud.GetAllAsync(predicate: p => p.IdUsuarioSeguridad.Equals(Guid.Parse(idUser)));

                if (resultRequest == null)
                {
                    return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.OK, message: "No se encontraron resultados");
                }

                var listadoEstadoSolicitud = await _repositoryDominio.GetAllAsync(predicate: p => p.TipoDominio.Equals(Guid.Parse("C5D41A74-09B6-4A7C-A45D-42792FCB4AC2")));

                var listadoTramites = await _repositoryDominio.GetAllAsync(predicate: p => p.TipoDominio.Equals(Guid.Parse("37A8F600-30E7-4693-81B7-2F1114124834")));

                var resultJoin = (from rr in resultRequest
                                  join rd in listadoEstadoSolicitud on rr.EstadoSolicitud equals rd.Id
                                  join lt in listadoTramites on rr.IdTramite equals lt.Id
                                  select new Entities.DTOs.RequestDetailDTO
                                  {
                                      EstadoSolicitud = rr.EstadoSolicitud.ToString(),
                                      Tramite = lt.Descripcion,
                                      Solicitud = rd.Descripcion,
                                      IdSolicitud = rr.IdSolicitud,
                                      FechaSolicitud = rr.FechaSolicitud.ToString("dd-MM-yyyy"),
                                      NumeroCertificado = rr.NumeroCertificado,
                                  }).ToList();

                return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.OK, message: "Solicitud ok", data: resultJoin.ToList(), count: resultJoin.Count());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        /// <summary>
        /// GetAllRequest
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.RequestDetailDTO>>> GetAllRequest()
        {
            try
            {
                var resultRequest = await _repositorySolicitud.GetAllAsync();

                if (resultRequest == null)
                {
                    return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.OK, message: "No se encontraron resultados");
                }

                var listadoEstadoSolicitud = await _repositoryDominio.GetAllAsync(predicate: p => p.TipoDominio.Equals(Guid.Parse("C5D41A74-09B6-4A7C-A45D-42792FCB4AC2")));

                var listadoTramites = await _repositoryDominio.GetAllAsync(predicate: p => p.TipoDominio.Equals(Guid.Parse("37A8F600-30E7-4693-81B7-2F1114124834")));

                var resultJoin = (from rr in resultRequest
                                  join rd in listadoEstadoSolicitud on rr.EstadoSolicitud equals rd.Id
                                  join lt in listadoTramites on rr.IdTramite equals lt.Id
                                  select new Entities.DTOs.RequestDetailDTO
                                  {
                                      EstadoSolicitud = rr.EstadoSolicitud.ToString(),
                                      Tramite = lt.Descripcion,
                                      Solicitud = rd.Descripcion,
                                      IdSolicitud = rr.IdSolicitud,
                                      FechaSolicitud = rr.FechaSolicitud.ToString("dd-MM-yyyy"),
                                      NumeroCertificado = rr.NumeroCertificado,
                                  }).ToList();

                return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.OK, message: "Solicitud ok", data: resultJoin.ToList(), count: resultJoin.Count());

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.RequestDetailDTO>>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

       

        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idEstado">The identifier solicitud.</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestByIdEstado(string idEstado)
        {
            try
            {
                var resultSolicitud = await _repositorySolicitud.GetAllAsync(
                    predicate: p => p.EstadoSolicitud.Equals(Guid.Parse(idEstado)), include: inc => inc
                        .Include(i => i.IdDatosCementerioNavigation)
                        .Include(i => i.IdInstitucionCertificaFallecimientoNavigation)
                        .Include(i => i.Persona));

                var resultLugarDefuncion = await _repositoryLugarDefuncion.GetAsync(predicate: p =>
                    p.IdLugarDefuncion.Equals(Guid.Parse(resultSolicitud.Select(x => x.IdLugarDefuncion)
                        .FirstOrDefault().ToString())));

                List<System.Guid> IdUbicacionPersona = new List<System.Guid>();

                foreach (var persona in resultSolicitud)
                {
                    IdUbicacionPersona.AddRange(persona.Persona.Select(x => x.IdUbicacionPersona.Value));
                }

                var resultUbicacionPersona = await _repositoryUbicacionPersona.GetAsync(w =>
                    w.IdPaisResidencia != Guid.Empty && IdUbicacionPersona.Any(a => a.Equals(w.IdUbicacionPersona)));

                var resultSol = new List<Entities.DTOs.SolicitudDTO>();

                foreach (var item in resultSolicitud)
                {
                    //solicitud validado
                    Entities.DTOs.SolicitudDTO solicitudDTO = new Entities.DTOs.SolicitudDTO
                    {
                        IdSolicitud = item.IdSolicitud,
                        FechaSolicitud = item.FechaSolicitud,
                        EstadoSolicitud = item.EstadoSolicitud,
                        IdTramite = item.IdTramite,


                        Persona = new List<Entities.DTOs.PersonaDTO>(),

                    };

                    resultSol.Add(solicitudDTO);

                    foreach (var rsp in item.Persona)
                    {
                        //datos persona validado
                        Entities.DTOs.PersonaDTO personaDTO = new Entities.DTOs.PersonaDTO
                        {

                            IdPersona = rsp.IdPersona,
                            NumeroIdentificacion = rsp.NumeroIdentificacion,
                            PrimerNombre = rsp.PrimerNombre,
                            SegundoNombre = rsp.SegundoNombre,
                            PrimerApellido = rsp.PrimerApellido,
                            SegundoApellido = rsp.SegundoApellido,
                            Estado = rsp.Estado,

                        };
                        solicitudDTO.Persona.Add(personaDTO);
                    }
                }

                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(code: System.Net.HttpStatusCode.OK,
                    message: "Solicitud OK", data: resultSol.ToList());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(
                    code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="idSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<Entities.DTOs.SolicitudDTO>>> GetRequestByIdSolicitud(string idSolicitud)
        {
            try
            {
                var resultSolicitud = await _repositorySolicitud.GetAllAsync(
                    predicate: p => p.IdSolicitud.Equals(Guid.Parse(idSolicitud)), include: inc => inc
                        .Include(i => i.IdDatosCementerioNavigation)
                        .Include(i => i.IdInstitucionCertificaFallecimientoNavigation)
                        .Include(i => i.Persona));

                var resultLugarDefuncion = await _repositoryLugarDefuncion.GetAsync(predicate: p =>
                    p.IdLugarDefuncion.Equals(Guid.Parse(resultSolicitud.Select(x => x.IdLugarDefuncion)
                        .FirstOrDefault().ToString())));
               

                List<System.Guid> IdUbicacionPersona = new List<System.Guid>();

                foreach (var persona in resultSolicitud)
                {
                    IdUbicacionPersona.AddRange(persona.Persona.Select(x => x.IdUbicacionPersona.Value));
                }

                var resultUbicacionPersona = await _repositoryUbicacionPersona.GetAsync(w =>
                    w.IdPaisResidencia != Guid.Empty && IdUbicacionPersona.Any(a => a.Equals(w.IdUbicacionPersona)));

                var resultSol = new List<Entities.DTOs.SolicitudDTO>();

                foreach (var item in resultSolicitud)
                {
                    //solicitud validado
                    Entities.DTOs.SolicitudDTO solicitudDTO = new Entities.DTOs.SolicitudDTO
                    {
                        IdSolicitud = item.IdSolicitud,
                        NumeroCertificado = item.NumeroCertificado,
                        FechaDefuncion = item.FechaDefuncion,
                        SinEstablecer = item.SinEstablecer,
                        Hora = item.Hora,
                        IdSexo = item.IdSexo,
                        FechaSolicitud = item.FechaSolicitud,
                        EstadoSolicitud = item.EstadoSolicitud,
                        IdPersonaVentanilla = item.IdPersonaVentanilla,
                        IdUsuarioSeguridad = item.IdUsuarioSeguridad,
                        IdTramite = item.IdTramite,
                        IdTipoMuerte = item.IdTipoMuerte,

                        //ubicacion persona validado
                        UbicacionPersona = new Entities.DTOs.UbicacionPersonaDTO
                        {
                            IdUbicacionPersona = resultUbicacionPersona?.IdUbicacionPersona,
                            IdPaisResidencia = resultUbicacionPersona?.IdPaisResidencia,
                            IdDepartamentoResidencia = resultUbicacionPersona?.IdDepartamentoResidencia,
                            IdCiudadResidencia = resultUbicacionPersona?.IdCiudadResidencia,
                            IdLocalidadResidencia = resultUbicacionPersona?.IdLocalidadResidencia,
                            IdAreaResidencia = resultUbicacionPersona?.IdAreaResidencia,
                            IdBarrioResidencia = resultUbicacionPersona?.IdBarrioResidencia
                        },

                        Persona = new List<Entities.DTOs.PersonaDTO>(),

                        //datos cementerio validado
                        DatosCementerio = new Entities.DTOs.DatosCementerioDTO
                        {
                            IdDatosCementerio = item.IdDatosCementerio,
                            EnBogota = item.IdDatosCementerioNavigation.EnBogota,
                            FueraBogota = item.IdDatosCementerioNavigation.FueraBogota,
                            FueraPais = item.IdDatosCementerioNavigation.FueraPais,
                            Cementerio = item.IdDatosCementerioNavigation.Cementerio,
                            OtroSitio = item.IdDatosCementerioNavigation.OtroSitio,
                            Ciudad = item.IdDatosCementerioNavigation.Ciudad,
                            IdPais = item.IdDatosCementerioNavigation.IdPais,
                            IdDepartamento = item.IdDatosCementerioNavigation.IdDepartamento,
                            IdMunicipio = item.IdDatosCementerioNavigation.IdMunicipio
                        },
                        //lufar de defuncion validado
                        LugarDefuncion = new Entities.DTOs.LugarDefuncionDTO
                        {
                            IdLugarDefuncion = resultLugarDefuncion.IdLugarDefuncion,
                            IdPais = resultLugarDefuncion.IdPais,
                            IdDepartamento = resultLugarDefuncion.IdDepartamento,
                            IdMunicipio = resultLugarDefuncion.IdMunicipio,
                            IdAreaDefuncion = resultLugarDefuncion.IdAreaDefuncion,
                            IdSitioDefuncion = resultLugarDefuncion.IdSitioDefuncion
                        },

                        //datos intitucion certifica fallecimiento ok
                        InstitucionCertificaFallecimiento = new Entities.DTOs.InstitucionCertificaFallecimientoDTO
                        {
                            IdInstitucionCertificaFallecimiento = item.IdInstitucionCertificaFallecimiento,
                            TipoIdentificacion = item.IdInstitucionCertificaFallecimientoNavigation.TipoIdentificacion,
                            NumeroIdentificacion =
                                item.IdInstitucionCertificaFallecimientoNavigation.NumeroIdentificacion,
                            RazonSocial = item.IdInstitucionCertificaFallecimientoNavigation.RazonSocial,
                            NumeroProtocolo = item.IdInstitucionCertificaFallecimientoNavigation.NumeroProtocolo,
                            NumeroActaLevantamiento = item.IdInstitucionCertificaFallecimientoNavigation
                                .NumeroActaLevantamiento,
                            FechaActa = item.IdInstitucionCertificaFallecimientoNavigation.FechaActa,
                            SeccionalFiscalia = item.IdInstitucionCertificaFallecimientoNavigation.SeccionalFiscalia,
                            NoFiscal = item.IdInstitucionCertificaFallecimientoNavigation.NoFiscal,
                            IdTipoInstitucion = item.IdInstitucionCertificaFallecimientoNavigation.IdTipoInstitucion
                        }                       
                    };

                    resultSol.Add(solicitudDTO);

                    foreach (var rsp in item.Persona)
                    {
                        //datos persona validado
                        Entities.DTOs.PersonaDTO personaDTO = new Entities.DTOs.PersonaDTO
                        {

                            IdPersona = rsp.IdPersona,
                            TipoIdentificacion = rsp.TipoIdentificacion,
                            NumeroIdentificacion = rsp.NumeroIdentificacion,
                            PrimerNombre = rsp.PrimerNombre,
                            SegundoNombre = rsp.SegundoNombre,
                            PrimerApellido = rsp.PrimerApellido,
                            SegundoApellido = rsp.SegundoApellido,
                            FechaNacimiento = rsp.FechaNacimiento,
                            Nacionalidad = rsp.Nacionalidad,
                            OtroParentesco = rsp.OtroParentesco,
                            Estado = rsp.Estado,
                            IdEstadoCivil = rsp.IdEstadoCivil,
                            IdNivelEducativo = rsp.IdNivelEducativo,
                            IdEtnia = rsp.IdEtnia,
                            IdRegimen = rsp.IdRegimen,
                            IdTipoPersona = rsp.IdTipoPersona,
                            IdSolicitud = rsp.IdSolicitud,
                            IdParentesco = rsp.IdParentesco,
                            IdLugarExpedicion = rsp.IdLugarExpedicion,
                            IdTipoProfesional = rsp.IdTipoProfesional,
                            IdUbicacionPersona = rsp.IdUbicacionPersona
                        };
                        solicitudDTO.Persona.Add(personaDTO);
                    }
                }

                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(code: System.Net.HttpStatusCode.OK,
                    message: "Solicitud OK", data: resultSol.ToList());
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<List<Entities.DTOs.SolicitudDTO>>(
                    code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }

        }

        public async Task<ResponseBase<List<ResumenSolicitud>>> GetResumenSolicitud(string idSolicitud)
        {
            try
            {
                var result = await _repositoryResumenSolicitud.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idSolicitud)));

                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<List<ResumenSolicitud>>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<List<ResumenSolicitud>>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result.ToList(), count: result.Count());
                }
                
            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<List<ResumenSolicitud>>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }
        

        #endregion
    }
}
