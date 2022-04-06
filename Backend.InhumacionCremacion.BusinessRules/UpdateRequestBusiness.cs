using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using System;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class UpdateRequestBusiness : Entities.Interface.Business.IUpdateRequestBusiness
    {
        #region Attributes
        /// <summary>
        /// _telemetryException
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

        #endregion

        #region Constructor
        /// <summary>
        /// UpdateRequestBusiness
        /// </summary>
        /// <param name="telemetryException"></param>
        /// <param name="repositoryDominio"></param>
        /// <param name="repositorySolicitud"></param>
        /// <param name="repositoryDatosCementerio"></param>
        /// <param name="repositoryInstitucionCertificaFallecimiento"></param>
        /// <param name="repositoryLugarDefuncion"></param>
        /// <param name="repositoryPersona"></param>
        /// <param name="repositoryUbicacionPersona"></param>
        public UpdateRequestBusiness(ITelemetryException telemetryException,
                                Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> repositoryDominio,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> repositorySolicitud,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> repositoryDatosCementerio,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.InstitucionCertificaFallecimiento> repositoryInstitucionCertificaFallecimiento,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.LugarDefuncion> repositoryLugarDefuncion,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> repositoryPersona,
                                Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.UbicacionPersona> repositoryUbicacionPersona)
        {
            _telemetryException = telemetryException;
            _repositorySolicitud = repositorySolicitud;
            _repositoryDatosCementerio = repositoryDatosCementerio;
            _repositoryInstitucionCertificaFallecimiento = repositoryInstitucionCertificaFallecimiento;
            _repositoryLugarDefuncion = repositoryLugarDefuncion;
            _repositoryPersona = repositoryPersona;
            _repositoryUbicacionPersona = repositoryUbicacionPersona;
        }
        #endregion

        #region Methods
        /// <summary>
        /// UpdateRequest
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        public async Task<ResponseBase<string>> UpdateRequest(SolicitudDTO requestDTO)
        {
            try
            {
                var datosCementerioDB = await _repositoryDatosCementerio.GetAsync(x => x.IdDatosCementerio == requestDTO.DatosCementerio.IdDatosCementerio);

                if (datosCementerioDB == null)
                {
                    return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                }

                datosCementerioDB.EnBogota = requestDTO.DatosCementerio.EnBogota;
                datosCementerioDB.FueraBogota = requestDTO.DatosCementerio.FueraBogota;
                datosCementerioDB.FueraPais = requestDTO.DatosCementerio.FueraPais;
                datosCementerioDB.Cementerio = requestDTO.DatosCementerio.Cementerio;
                datosCementerioDB.OtroSitio = requestDTO.DatosCementerio.OtroSitio;
                datosCementerioDB.Ciudad = requestDTO.DatosCementerio.Ciudad;
                datosCementerioDB.IdPais = requestDTO.DatosCementerio.IdPais;
                datosCementerioDB.IdDepartamento = requestDTO.DatosCementerio.IdDepartamento;
                datosCementerioDB.IdMunicipio = requestDTO.DatosCementerio.IdMunicipio;

                await _repositoryDatosCementerio.UpdateAsync(datosCementerioDB);

                var datosInstitucionCertificaFallecimiento = await _repositoryInstitucionCertificaFallecimiento.GetAsync(x => x.IdInstitucionCertificaFallecimiento == requestDTO.InstitucionCertificaFallecimiento.IdInstitucionCertificaFallecimiento);

                if (datosInstitucionCertificaFallecimiento == null)
                {
                    return new ResponseBase<string>(code: System.Net.HttpStatusCode.BadRequest, message: "No se encontró el codigo para actualizar");
                }

                datosInstitucionCertificaFallecimiento.TipoIdentificacion = requestDTO.InstitucionCertificaFallecimiento.TipoIdentificacion;
                datosInstitucionCertificaFallecimiento.NumeroIdentificacion = requestDTO.InstitucionCertificaFallecimiento.NumeroIdentificacion;
                datosInstitucionCertificaFallecimiento.RazonSocial = requestDTO.InstitucionCertificaFallecimiento.RazonSocial;
                datosInstitucionCertificaFallecimiento.NumeroProtocolo = requestDTO.InstitucionCertificaFallecimiento.NumeroProtocolo;
                datosInstitucionCertificaFallecimiento.NumeroActaLevantamiento = requestDTO.InstitucionCertificaFallecimiento.NumeroActaLevantamiento;
                datosInstitucionCertificaFallecimiento.FechaActa = requestDTO.InstitucionCertificaFallecimiento.FechaActa;
                datosInstitucionCertificaFallecimiento.SeccionalFiscalia = requestDTO.InstitucionCertificaFallecimiento.SeccionalFiscalia;
                datosInstitucionCertificaFallecimiento.NoFiscal = requestDTO.InstitucionCertificaFallecimiento.NoFiscal;
                datosInstitucionCertificaFallecimiento.IdTipoInstitucion = requestDTO.InstitucionCertificaFallecimiento.IdTipoInstitucion;

                //institucion que certifica el fallecemiento ok
                await _repositoryInstitucionCertificaFallecimiento.UpdateAsync(datosInstitucionCertificaFallecimiento);

                var datosLugarDefuncionDB = await _repositoryLugarDefuncion.GetAsync(x => x.IdLugarDefuncion == requestDTO.LugarDefuncion.IdLugarDefuncion);

                if (datosLugarDefuncionDB == null)
                {
                    return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                }

                datosLugarDefuncionDB.IdPais = requestDTO.LugarDefuncion.IdPais;
                datosLugarDefuncionDB.IdDepartamento = requestDTO.LugarDefuncion.IdDepartamento;
                datosLugarDefuncionDB.IdMunicipio = requestDTO.LugarDefuncion.IdMunicipio;
                datosLugarDefuncionDB.IdAreaDefuncion = requestDTO.LugarDefuncion.IdAreaDefuncion;
                datosLugarDefuncionDB.IdSitioDefuncion = requestDTO.LugarDefuncion.IdSitioDefuncion;

                //lugar de defuncion ok
                await _repositoryLugarDefuncion.UpdateAsync(datosLugarDefuncionDB);

                var solicitudDB = await _repositorySolicitud.GetAsync(x => x.IdSolicitud == requestDTO.IdSolicitud);

                if (solicitudDB == null)
                {
                    return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                }

                solicitudDB.NumeroCertificado = requestDTO.NumeroCertificado;
                solicitudDB.FechaDefuncion = requestDTO.FechaDefuncion;
                solicitudDB.SinEstablecer = requestDTO.SinEstablecer;
                solicitudDB.Hora = requestDTO.Hora;
                solicitudDB.IdSexo = requestDTO.IdSexo;
                solicitudDB.FechaSolicitud = DateTime.Now;
                solicitudDB.EstadoSolicitud = requestDTO.EstadoSolicitud;
                solicitudDB.IdPersonaVentanilla = requestDTO.IdPersonaVentanilla;
                solicitudDB.IdUsuarioSeguridad = requestDTO.IdUsuarioSeguridad;
                solicitudDB.IdTramite = requestDTO.IdTramite;
                solicitudDB.IdLugarDefuncion = requestDTO.LugarDefuncion.IdLugarDefuncion;
                solicitudDB.IdTipoMuerte = requestDTO.IdTipoMuerte;
                solicitudDB.IdDatosCementerio = requestDTO.DatosCementerio.IdDatosCementerio;
                solicitudDB.IdInstitucionCertificaFallecimiento = requestDTO.InstitucionCertificaFallecimiento.IdInstitucionCertificaFallecimiento;

                //pendiente validacion de campos obligatorios ok
                await _repositorySolicitud.UpdateAsync(solicitudDB);


                var resultUbicacionPersona = new Entities.Models.InhumacionCremacion.UbicacionPersona();

                if (requestDTO.UbicacionPersona != null)
                {
                    resultUbicacionPersona = await _repositoryUbicacionPersona.GetAsync(p => p.IdUbicacionPersona.Equals(requestDTO.UbicacionPersona.IdUbicacionPersona));
                }

                //ubicacion persona
                foreach (var persona in requestDTO.Persona)
                {
                    if (persona.IdTipoPersona == Guid.Parse("342d934b-c316-46cb-a4f3-3aac5845d246") &&
                        requestDTO.UbicacionPersona.IdPaisResidencia != Guid.Empty &&
                        requestDTO.UbicacionPersona.IdDepartamentoResidencia != Guid.Empty &&
                        requestDTO.UbicacionPersona.IdCiudadResidencia != Guid.Empty &&
                        requestDTO.UbicacionPersona.IdLocalidadResidencia != Guid.Empty &&
                        requestDTO.UbicacionPersona.IdAreaResidencia != Guid.Empty &&
                        requestDTO.UbicacionPersona.IdBarrioResidencia != Guid.Empty)
                    {
                        // si el tipo de persona es madre y los valores son diferentes de: "00000000-0000-0000-0000-000000000000" se inserta la ubicacion


                        var ubicacionPersonaDB = await _repositoryUbicacionPersona.GetAsync(x => x.IdUbicacionPersona == requestDTO.UbicacionPersona.IdUbicacionPersona);

                        if (ubicacionPersonaDB == null)
                        {
                            return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                        }

                        ubicacionPersonaDB.IdPaisResidencia = resultUbicacionPersona.IdPaisResidencia;
                        ubicacionPersonaDB.IdDepartamentoResidencia = resultUbicacionPersona.IdDepartamentoResidencia;
                        ubicacionPersonaDB.IdCiudadResidencia = resultUbicacionPersona.IdCiudadResidencia;
                        ubicacionPersonaDB.IdLocalidadResidencia = resultUbicacionPersona.IdLocalidadResidencia;
                        ubicacionPersonaDB.IdAreaResidencia = resultUbicacionPersona.IdAreaResidencia;
                        ubicacionPersonaDB.IdBarrioResidencia = resultUbicacionPersona.IdBarrioResidencia;

                        await _repositoryUbicacionPersona.UpdateAsync(ubicacionPersonaDB);

                        var personaDB = await _repositoryPersona.GetAsync(x => x.IdPersona == persona.IdPersona);

                        if (personaDB == null)
                        {
                            return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                        }

                        personaDB.TipoIdentificacion = persona.TipoIdentificacion;
                        personaDB.NumeroIdentificacion = persona.NumeroIdentificacion;
                        personaDB.PrimerNombre = persona.PrimerNombre;
                        personaDB.SegundoNombre = persona.SegundoNombre;
                        personaDB.PrimerApellido = persona.PrimerApellido;
                        personaDB.SegundoApellido = persona.SegundoApellido;
                        personaDB.FechaNacimiento = persona.FechaNacimiento;
                        personaDB.Nacionalidad = persona.Nacionalidad;
                        personaDB.OtroParentesco = persona.OtroParentesco;
                        personaDB.Estado = persona.Estado;
                        personaDB.IdEstadoCivil = persona.IdEstadoCivil;
                        personaDB.IdNivelEducativo = persona.IdNivelEducativo;
                        personaDB.IdEtnia = persona.IdEtnia;
                        personaDB.IdRegimen = persona.IdRegimen;
                        personaDB.IdTipoPersona = persona.IdTipoPersona;
                        personaDB.IdSolicitud = personaDB.IdSolicitud;
                        personaDB.IdParentesco = persona.IdParentesco;
                        personaDB.IdLugarExpedicion = persona.IdLugarExpedicion;
                        personaDB.IdTipoProfesional = persona.IdTipoProfesional;
                        personaDB.IdUbicacionPersona = requestDTO.UbicacionPersona.IdUbicacionPersona;
                        await _repositoryPersona.UpdateAsync(personaDB);
                    }
                    else // si el tipo de persona es diferente de la madre no tiene ubicacion
                    {
                        var personaDB = await _repositoryPersona.GetAsync(x => x.IdPersona == persona.IdPersona);
                        if (personaDB == null)
                        {
                            return new ResponseBase<string>(System.Net.HttpStatusCode.BadRequest, "No se encontró el codigo para actualizar");
                        }
                        personaDB.TipoIdentificacion = persona.TipoIdentificacion;
                        personaDB.NumeroIdentificacion = persona.NumeroIdentificacion;
                        personaDB.PrimerNombre = persona.PrimerNombre;
                        personaDB.SegundoNombre = persona.SegundoNombre;
                        personaDB.PrimerApellido = persona.PrimerApellido;
                        personaDB.SegundoApellido = persona.SegundoApellido;
                        personaDB.FechaNacimiento = persona.FechaNacimiento;
                        personaDB.Nacionalidad = persona.Nacionalidad;
                        personaDB.OtroParentesco = persona.OtroParentesco;
                        personaDB.Estado = persona.Estado;
                        personaDB.IdEstadoCivil = persona.IdEstadoCivil;
                        personaDB.IdNivelEducativo = persona.IdNivelEducativo;
                        personaDB.IdEtnia = persona.IdEtnia;
                        personaDB.IdRegimen = persona.IdRegimen;
                        personaDB.IdTipoPersona = persona.IdTipoPersona;
                        personaDB.IdSolicitud = persona.IdSolicitud;
                        personaDB.IdParentesco = persona.IdParentesco;
                        personaDB.IdLugarExpedicion = persona.IdLugarExpedicion;
                        personaDB.IdTipoProfesional = persona.IdTipoProfesional;
                        personaDB.IdUbicacionPersona = persona.IdUbicacionPersona;
                    }
                }
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.OK, data: requestDTO.IdSolicitud.ToString(), message: "Solicitud");
            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        ///*
        public async Task<ResponseBase<string>> UpdateMedico(string idMedico, string campo, string cambio)
        {
            try
            {

                var personaDB = await _repositoryPersona.GetAsync(x => x.NumeroIdentificacion == idMedico);

                if (personaDB == null)
                {
                    return new ResponseBase<string>(code: System.Net.HttpStatusCode.NotFound, message: "No se encontró el registro para actualizar");
                }
                else
                {
                    switch (campo) {

                        case "primerNombre":
                            personaDB.PrimerNombre = cambio;
                            break;
                        case "segundoNombre":
                            personaDB.SegundoNombre = cambio;
                            break;
                        case "primerApellido":
                            personaDB.PrimerApellido = cambio;
                            break;
                        case "segundoApellido":
                            personaDB.SegundoApellido = cambio;
                            break;
                        case null:
                            return new ResponseBase<string>(code: System.Net.HttpStatusCode.NotFound, message: "No se encontró el registro para actualizar, incorrecta llave de actualización");
                            break;
                    }
                    await _repositoryPersona.UpdateAsync(personaDB);
                    return new ResponseBase<string>(code: System.Net.HttpStatusCode.OK, message: "registro modificado");
                } 

            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new ResponseBase<string>(code: System.Net.HttpStatusCode.InternalServerError, message: "registro no modificado");
            }
        }
        //*/
        #endregion
    }
}
