using Backend.InhumacionCremacion.Entities.Interface.Business;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Wkhtmltopdf.NetCore;
using System.Collections.Generic;
using System.Linq;
using Backend.InhumacionCremacion.Entities.Models.Commons;
using System.IO;
using System.Globalization;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class GeneratePDFBusiness : IGeneratePDFBusiness
    {


        /// <summary>
        /// The repository Resumen Solicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.ResumenSolicitud> _repositoryResumenSolicitud;

        /// <summary>
        /// The repository datos cementerio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosFuneraria> _repositoryDatosFuneraria;
        /// <summary>
        /// The generate PDF
        /// </summary>
        readonly IGeneratePdf _generatePdf;

        /// <summary>
        /// The telemetry exception
        /// </summary>
        private readonly ITelemetryException _telemetryException;

        /// <summary>
        /// _repositorySolicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> _repositorySolicitud;



        /// <summary>
        /// The repository solicitud
        /// </summary>
        private readonly
            Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Formatos> _repositoryFormato;

        /// <summary>
        /// _repositoryPersona
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> _repositoryPersona;

        /// <summary>
        /// _repositoryDominio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> _repositoryDominio;

        /// <summary>
        /// _repositoryDominio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Departamento> _repositoryDepartamento;

        /// <summary>
        /// _repositoryDominio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Municipio> _repositoryMunicipio;

        /// <summary>
        /// The repository solicitud
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.FirmaUsuarios> _repositoryFirmaUsuarios;

        /// <summary>
        /// The repository datos cementerio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> _repositoryDatosCementerio;

        /// <summary>
        /// The repository datos cementerio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Licencia> _repositoryLicencia;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratePDFBusiness"/> class.
        /// </summary>
        /// <param name="telemetryException">The telemetry exception.</param>
        /// <param name="generatePdf">The generate PDF.</param>
        public GeneratePDFBusiness(ITelemetryException telemetryException,
                                   IGeneratePdf generatePdf,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> repositorySolicitud,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.FirmaUsuarios> repositoryFirmaUsuarios,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> repositoryPersona,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Formatos> repositoryFormato,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.ResumenSolicitud> repositoryResumenSolicitud,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosFuneraria> repositoryDatosFuneraria,
                                   Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> repositoryDominio,
                                   Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Municipio> repositoryMunicipio,
                                   Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Departamento> repositoryDepartamento,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> repositoryDatosCementerio,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Licencia> repositoryLicencia)
        {
            _telemetryException = telemetryException;
            _generatePdf = generatePdf;
            _repositorySolicitud = repositorySolicitud;
            _repositoryFirmaUsuarios = repositoryFirmaUsuarios;
            _repositoryPersona = repositoryPersona;
            _repositoryFormato = repositoryFormato;
            _repositoryResumenSolicitud = repositoryResumenSolicitud;
            _repositoryDatosFuneraria = repositoryDatosFuneraria;
            _repositoryDominio = repositoryDominio;
            _repositoryMunicipio = repositoryMunicipio;
            _repositoryDepartamento = repositoryDepartamento;
            _repositoryDatosCementerio = repositoryDatosCementerio;
            _repositoryLicencia = repositoryLicencia;
        }

        /// <summary>
        /// GeneratePDF
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<ResponseBase<string>> GeneratePDF(string idSolicitud, string idValidador, string nombreValidador, string codigo)
        {


            try
            {

                
                Persona datosPersonaFallecida;
                Persona datosPersonaMadre;
                Persona datosMedico;
                Persona datosAutorizadorCremacion;

                Solicitud datoSolitud = null;

                datoSolitud = await _repositorySolicitud.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)));
                datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));

                string nombreMedico = "";

                //validacion de nombres de medico

                if (datosMedico.SegundoNombre == null)
                {
                    nombreMedico = datosMedico.PrimerNombre + ' ' + datosMedico.PrimerApellido;

                }
                else
                {
                    nombreMedico = datosMedico.PrimerNombre + ' ' + datosMedico.SegundoNombre + ' ' + datosMedico.PrimerApellido;
                }

                if (datosMedico.SegundoApellido != null)
                {
                    nombreMedico = nombreMedico + ' ' + datosMedico.SegundoApellido;

                }


                var firmaAprobadorDB = await _repositoryFirmaUsuarios.GetAsync(x => x.ID_FIrma == Guid.Parse("20B71C38-EF61-4AEC-0037-08DA32040274"));
                var firmaValidadorDB = await _repositoryFirmaUsuarios.GetAsync(x => x.ID_Usuario == Guid.Parse(idValidador));


                if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")) || datoSolitud.IdTramite.Equals(Guid.Parse("E69BDA86-2572-45DB-90DC-B40BE14FE020")))
                {

                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("01F64F02-373B-49D4-8CB1-CB677F74292C")));

                    string nombreFallecido = "";


                    //validacion de nombres de fallecido

                    if (datosPersonaFallecida.SegundoNombre == null)
                    {
                        nombreFallecido = datosPersonaFallecida.PrimerNombre + ' ' + datosPersonaFallecida.PrimerApellido;

                    }
                    else
                    {
                        nombreFallecido = datosPersonaFallecida.PrimerNombre + ' ' + datosPersonaFallecida.SegundoNombre + ' ' + datosPersonaFallecida.PrimerApellido;
                    }

                    if (datosPersonaFallecida.SegundoApellido != null)
                    {
                        nombreFallecido = nombreFallecido + ' ' + datosPersonaFallecida.SegundoApellido;

                    }



                    var resumen = await GetResumenSolicitud(idSolicitud);
                    var funeraria = await GetFuneraria(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaFallecida.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaFallecida.TipoIdentificacion).ToString());
                    var tipoTramite = await GetDescripcionDominio((datoSolitud.IdTramite).ToString());
                    int numeroTramite = datoSolitud.ID_Control_Tramite.Value;
                    DateTime fechaActual = DateTime.Now;
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var genero = await GetDescripcionDominio(datoSolitud.IdSexo.ToString());
                    var firmaAprobador = firmaAprobadorDB.Firma;
                    var firmaValidador = firmaValidadorDB.Firma;

                    String nombreCementeio = "";

                    if (cementerio.Data.EnBogota == true)
                    {
                        nombreCementeio = cementerio.Data.Cementerio.ToUpper();
                    }
                    else if (cementerio.Data.FueraBogota == true)
                    {
                        var departamento = await GetDescripcionDepartamento(cementerio.Data.IdDepartamento.ToString());
                        var municipio = await GetDescripcionMunicipio(cementerio.Data.IdMunicipio.ToString());
                        nombreCementeio = nombreCementeio = "FUERA DE BOGOTá, "+ departamento.Data.Descripcion.ToUpper() +" "+ municipio.Data.Descripcion.ToUpper();
                    }
                    else
                    {
                        var pais = await GetDescripcionDominio(cementerio.Data.IdPais.ToString());
                        nombreCementeio = "FUERA DEL PAÍS, "+ pais.Data.Descripcion.ToUpper() +" "+ cementerio.Data.Ciudad.ToUpper();
                    }

                    String label = " ";


                    if (resumen.Data[0].CumpleCausa)
                    {
                        label = "Observación: ";
                    }


                    if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")))
                    {

                        //INHUMACION INDIVIDUAL

                        

                        var dataInhumacionIndividual = new Entities.DTOs.DetallePdfInhumacionIndividualDto
                        {


                            FechaActual = fechaActual,
                            Hora = fechaActual.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido =   datoSolitud.FechaDefuncion,
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion.ToUpper(),
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            Edad = Utilities.ConvertTypes.GetdifFechas(DateTime.Parse(datosPersonaFallecida.FechaNacimiento), datoSolitud.FechaDefuncion),
                            //Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento), Convert.ToDateTime(datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"))),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            ObservacionCausaLabel = label,
                            ObservacionCausa = resumen.Data[0].ObservacionCausa,
                            CodigoVerificacion = codigo
                        };


                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionIndividual.cshtml", dataInhumacionIndividual);

                        Licencia licencia = await _repositoryLicencia.GetAsync(e => e.NumeroLicencia == int.Parse(resumen.Data[0].NumeroLicencia));

                        if (licencia == null)
                        {
                            await _repositoryLicencia.AddAsync(new Licencia
                            {
                                ID_Tabla = Guid.NewGuid(),
                                ID_Documento = Guid.NewGuid(),
                                NombreDocumento = "Licencia_" + tipoTramite.Data.Descripcion.Replace(" ", String.Empty) + "_" + resumen.Data[0].NumeroLicencia,
                                NumeroTramite = numeroTramite,
                                NumeroLicencia = int.Parse(resumen.Data[0].NumeroLicencia),
                                FechaGeneracion = fechaActual,
                                DocumentoBase64 = Convert.ToBase64String(pdf)
                            });
                        }

                       // var pdfStream = new System.IO.MemoryStream();

                        //pdfStream.Write(pdf, 0, pdf.Length);

                      //  pdfStream.Position = 0;

                        return new ResponseBase<string>(code: HttpStatusCode.OK, message: "Solicitud OK", data: Convert.ToBase64String(pdf));

                    }
                    else
                    {
                        //CREMACION INDIVIDUAL

                        datosAutorizadorCremacion = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("CC4C8C4D-B557-4A5A-A2B3-520D757C5D06")));

                        var parentesco = await GetDescripcionDominio((datosAutorizadorCremacion.IdParentesco).ToString());

                        string nombreAutorizadorCremacion = "";

                        //validacion de nombre de Autorizador Cremacion

                        if (datosAutorizadorCremacion.SegundoNombre == null)
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;

                        }
                        else
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.SegundoNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;
                        }

                        if (datosAutorizadorCremacion.SegundoApellido != null)
                        {
                            nombreAutorizadorCremacion = nombreAutorizadorCremacion + ' ' + datosAutorizadorCremacion.SegundoApellido;

                        }


                        var dataCremacionIndividual = new Entities.DTOs.DetallePdfCremacionIndividualDto
                        {


                            FechaActual = fechaActual,
                            Hora = fechaActual.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            FechaCremacion = datoSolitud.FechaDefuncion.AddDays(1),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion.ToUpper(),
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            Edad = Utilities.ConvertTypes.GetdifFechas(DateTime.Parse(datosPersonaFallecida.FechaNacimiento), datoSolitud.FechaDefuncion),
                            //Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento), Convert.ToDateTime(datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"))),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            AutorizadorCremacion = nombreAutorizadorCremacion.ToUpper(),
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            ObservacionCausaLabel = label,
                            ObservacionCausa = resumen.Data[0].ObservacionCausa,
                            CodigoVerificacion = codigo
                        };


                        var pdf = await _generatePdf.GetByteArray("Views/CremacionIndividual.cshtml", dataCremacionIndividual);

                        Licencia licencia = await _repositoryLicencia.GetAsync(e => e.NumeroLicencia == int.Parse(resumen.Data[0].NumeroLicencia));

                        if (licencia == null)
                        {
                            await _repositoryLicencia.AddAsync(new Licencia
                            {
                                ID_Tabla = Guid.NewGuid(),
                                ID_Documento = Guid.NewGuid(),
                                NombreDocumento = "Licencia_" + tipoTramite.Data.Descripcion.Replace(" ", String.Empty) + "_" + resumen.Data[0].NumeroLicencia,
                                NumeroTramite = numeroTramite,
                                NumeroLicencia = int.Parse(resumen.Data[0].NumeroLicencia),
                                FechaGeneracion = fechaActual,
                                DocumentoBase64 = Convert.ToBase64String(pdf)
                            });
                        }

                        //var pdfStream = new System.IO.MemoryStream();

                        //pdfStream.Write(pdf, 0, pdf.Length);

                        //pdfStream.Position = 0;

                        return new ResponseBase<string>(code: HttpStatusCode.OK, message: "Solicitud OK", data: Convert.ToBase64String(pdf));
                    }

                }
                else
                {  //proceso fetal


                    datosPersonaMadre = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("342D934B-C316-46CB-A4F3-3AAC5845D246")));
                   
                    string nombreMadre = "";
                    string nombreFallecido = "";


                    //validacion de nombres de madre

                    if (datosPersonaMadre.SegundoNombre == null)
                    {
                        nombreMadre = datosPersonaMadre.PrimerNombre + ' ' + datosPersonaMadre.PrimerApellido;

                    }
                    else
                    {
                        nombreMadre = datosPersonaMadre.PrimerNombre + ' ' + datosPersonaMadre.SegundoNombre + ' ' + datosPersonaMadre.PrimerApellido;
                    }

                    if (datosPersonaMadre.SegundoApellido != null)
                    {
                        nombreMadre = nombreMadre + ' ' + datosPersonaMadre.SegundoApellido;

                    }

                    


                    var funeraria = await GetFuneraria(idSolicitud);
                    var resumen = await GetResumenSolicitud(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaMadre.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaMadre.TipoIdentificacion).ToString());
                    var tipoTramite = await GetDescripcionDominio((datoSolitud.IdTramite).ToString());
                    int numeroTramite = datoSolitud.ID_Control_Tramite.Value;
                    DateTime fechaActual = DateTime.Now;
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var genero = await GetDescripcionDominio(datoSolitud.IdSexo.ToString());
                    var firmaAprobador = firmaAprobadorDB.Firma;
                    var firmaValidador = firmaValidadorDB.Firma;


                    //validacion de nombres de fallecido

                    if (genero.Data.Descripcion.ToUpper() == "MUJER")
                    {
                        nombreFallecido = "MORTINATO FEMENINO";

                    }
                    else if (genero.Data.Descripcion.ToUpper() == "HOMBRE")
                    {
                        nombreFallecido = "MORTINATO MASCULINO";
                    }
                    else
                    {
                        nombreFallecido = "MORTINATO";
                    }


                    String nombreCementeio = "";

                    if (cementerio.Data.EnBogota == true)
                    {
                        nombreCementeio = cementerio.Data.Cementerio.ToUpper();
                    }
                    else if (cementerio.Data.FueraBogota == true)
                    {
                        var departamento = await GetDescripcionDepartamento(cementerio.Data.IdDepartamento.ToString());
                        var municipio = await GetDescripcionMunicipio(cementerio.Data.IdMunicipio.ToString());
                        nombreCementeio = nombreCementeio = nombreCementeio = "FUERA DE BOGOTá, "+ departamento.Data.Descripcion.ToUpper() +" "+ municipio.Data.Descripcion.ToUpper();
                    }
                    else
                    {
                        var pais = await GetDescripcionDominio(cementerio.Data.IdPais.ToString());
                        nombreCementeio = "FUERA DEL PAÍS, "+ pais.Data.Descripcion.ToUpper() +" "+ cementerio.Data.Ciudad.ToUpper();
                    }

                    if (datoSolitud.IdTramite.Equals(Guid.Parse("AD5EA0CB-1FA2-4933-A175-E93F2F8C0060")))
                    {
                        //IHUMACION FETAL

                        var dataInhumacionFetal = new Entities.DTOs.DetallePdfInhumacionFetalDto
                        {


                            FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                            Hora = fechaActual.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            NombreMadre = nombreMadre.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            CodigoVerificacion = codigo
                        };


                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionFetal.cshtml", dataInhumacionFetal);

                        Licencia licencia = await _repositoryLicencia.GetAsync(e => e.NumeroLicencia == int.Parse(resumen.Data[0].NumeroLicencia));

                        if (licencia == null)
                        {
                            await _repositoryLicencia.AddAsync(new Licencia
                            {
                                ID_Tabla = Guid.NewGuid(),
                                ID_Documento = Guid.NewGuid(),
                                NombreDocumento = "Licencia_" + tipoTramite.Data.Descripcion.Replace(" ", String.Empty) + "_" + resumen.Data[0].NumeroLicencia,
                                NumeroTramite = numeroTramite,
                                NumeroLicencia = int.Parse(resumen.Data[0].NumeroLicencia),
                                FechaGeneracion = fechaActual,
                                DocumentoBase64 = Convert.ToBase64String(pdf)
                            });
                        }

                        //var pdfStream = new System.IO.MemoryStream();

                        //pdfStream.Write(pdf, 0, pdf.Length);

                        //pdfStream.Position = 0;

                        return new ResponseBase<string>(code: HttpStatusCode.OK, message: "Solicitud OK", data: Convert.ToBase64String(pdf));

                    }
                    else
                    {

                        //CREMACION FETAL

                        datosAutorizadorCremacion = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("CC4C8C4D-B557-4A5A-A2B3-520D757C5D06")));

                        var parentesco = await GetDescripcionDominio((datosAutorizadorCremacion.IdParentesco).ToString());

                        string nombreAutorizadorCremacion = "";

                        //validacion de nombre de Autorizador Cremacion

                        if (datosAutorizadorCremacion.SegundoNombre == null)
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;

                        }
                        else
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.SegundoNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;
                        }

                        if (datosAutorizadorCremacion.SegundoApellido != null)
                        {
                            nombreAutorizadorCremacion = nombreAutorizadorCremacion + ' ' + datosAutorizadorCremacion.SegundoApellido;

                        }

                        var dataCremacionFetal = new Entities.DTOs.DetallePdfCremacionFetalDto
                        {


                            FechaActual = fechaActual.ToString("dd/MM/yyyy"),
                            Hora = fechaActual.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            NombreMadre = nombreMadre.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"),
                            FechaCremacion = datoSolitud.FechaDefuncion.AddDays(1).ToString("dd/MM/yyyy"),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            AutorizadorCremacion = nombreAutorizadorCremacion.ToUpper(),
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            CodigoVerificacion = codigo
                        };

                        var pdf = await _generatePdf.GetByteArray("Views/CremacionFetal.cshtml", dataCremacionFetal);

                        Licencia licencia = await _repositoryLicencia.GetAsync(e => e.NumeroLicencia == int.Parse(resumen.Data[0].NumeroLicencia));

                        if (licencia == null)
                        {
                            await _repositoryLicencia.AddAsync(new Licencia
                            {
                                ID_Tabla = Guid.NewGuid(),
                                ID_Documento = Guid.NewGuid(),
                                NombreDocumento = "Licencia_" + tipoTramite.Data.Descripcion.Replace(" ", String.Empty) + "_" + resumen.Data[0].NumeroLicencia,
                                NumeroTramite = numeroTramite,
                                NumeroLicencia = int.Parse(resumen.Data[0].NumeroLicencia),
                                FechaGeneracion = fechaActual,
                                DocumentoBase64 = Convert.ToBase64String(pdf)
                            });
                        }

                       // var pdfStream = new System.IO.MemoryStream();

                        //pdfStream.Write(pdf, 0, pdf.Length);

                        //pdfStream.Position = 0;

                        return new ResponseBase<string>(code: HttpStatusCode.OK, message: "Solicitud OK", data: Convert.ToBase64String(pdf));
                    }


                }

            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<string>(code: HttpStatusCode.InternalServerError, message: ex.Message);
            }

        }

        public async Task<ResponseBase<dynamic>> GeneratePDFPrev(string idSolicitud, string idValidador, string nombreValidador)
        {
            try
            {


                Persona datosPersonaFallecida;
                Persona datosPersonaMadre;
                Persona datosMedico;
                Persona datosAutorizadorCremacion;

                Solicitud datoSolitud = null;

                datoSolitud = await _repositorySolicitud.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)));
                datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));

                string nombreMedico = "";

                //validacion de nombres de medico

                if (datosMedico.SegundoNombre == null)
                {
                    nombreMedico = datosMedico.PrimerNombre + ' ' + datosMedico.PrimerApellido;

                }
                else
                {
                    nombreMedico = datosMedico.PrimerNombre + ' ' + datosMedico.SegundoNombre + ' ' + datosMedico.PrimerApellido;
                }

                if (datosMedico.SegundoApellido != null)
                {
                    nombreMedico = nombreMedico + ' ' + datosMedico.SegundoApellido;

                }


                var firmaAprobadorDB = await _repositoryFirmaUsuarios.GetAsync(x => x.ID_FIrma == Guid.Parse("20B71C38-EF61-4AEC-0037-08DA32040274"));
                var firmaValidadorDB = await _repositoryFirmaUsuarios.GetAsync(x => x.ID_Usuario == Guid.Parse(idValidador));


                if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")) || datoSolitud.IdTramite.Equals(Guid.Parse("E69BDA86-2572-45DB-90DC-B40BE14FE020")))
                {

                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("01F64F02-373B-49D4-8CB1-CB677F74292C")));
                   
                    string nombreFallecido = "";
                    

                    //validacion de nombres de fallecido

                    if (datosPersonaFallecida.SegundoNombre == null)
                    {
                        nombreFallecido = datosPersonaFallecida.PrimerNombre + ' ' + datosPersonaFallecida.PrimerApellido;

                    }
                    else
                    {
                        nombreFallecido = datosPersonaFallecida.PrimerNombre + ' '+ datosPersonaFallecida.SegundoNombre + ' '+ datosPersonaFallecida.PrimerApellido;
                    }

                    if (datosPersonaFallecida.SegundoApellido != null)
                    {
                        nombreFallecido = nombreFallecido + ' ' + datosPersonaFallecida.SegundoApellido;

                    }

                    

                    var resumen = await GetResumenSolicitud(idSolicitud);
                    var funeraria = await GetFuneraria(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaFallecida.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaFallecida.TipoIdentificacion).ToString());
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var genero = await GetDescripcionDominio(datoSolitud.IdSexo.ToString());
                    var firmaAprobador = firmaAprobadorDB.Firma;
                    var firmaValidador = firmaValidadorDB.Firma;

                    String nombreCementeio = "";

                    if (cementerio.Data.EnBogota==true)
                    {
                        nombreCementeio = cementerio.Data.Cementerio.ToUpper();
                    }
                    else if (cementerio.Data.FueraBogota==true)
                    {
                        var departamento = await GetDescripcionDepartamento(cementerio.Data.IdDepartamento.ToString());
                        var municipio = await GetDescripcionMunicipio(cementerio.Data.IdMunicipio.ToString());
                        nombreCementeio = nombreCementeio = "FUERA DE BOGOTá, "+ departamento.Data.Descripcion.ToUpper() +" "+ municipio.Data.Descripcion.ToUpper();
                    }
                    else
                    {
                        var pais = await GetDescripcionDominio(cementerio.Data.IdPais.ToString());
                        nombreCementeio = "FUERA DEL PAÍS, "+ pais.Data.Descripcion.ToUpper() +" "+ cementerio.Data.Ciudad.ToUpper();
                    }

                    
                    String label = " ";


                    if (resumen.Data[0].CumpleCausa)
                    {
                        label = "Observación: ";
                    }


                    if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")))
                    {
                        //DateTime x = DateTime.ParseExact(datoSolitud.FechaDefuncion.ToString(), "dd/MM/yyyy HH:mm:ss tt", CultureInfo.InvariantCulture);
                        //DateTime x = 
                        //INHUMACION INDIVIDUAL
                        
                        //Console.WriteLine(datoSolitud.FechaDefuncion.ToString());
                        string fechaPrueba = datoSolitud.FechaDefuncion.ToString().Substring(0, 10);
                        //Console.WriteLine(fechaPrueba);
                        //Console.WriteLine(DateTime.Parse(fechaPrueba));

                        var dataInhumacionIndividual = new Entities.DTOs.DetallePdfInhumacionIndividualDto
                        {


                            FechaActual = DateTime.Now,
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = " ",
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = DateTime.Parse(fechaPrueba),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion.ToUpper(),
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            Edad = Utilities.ConvertTypes.GetdifFechas(DateTime.Parse(datosPersonaFallecida.FechaNacimiento), datoSolitud.FechaDefuncion),
                            //Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento), Convert.ToDateTime(datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"))),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            ObservacionCausaLabel = label,
                            ObservacionCausa = resumen.Data[0].ObservacionCausa,
                            CodigoVerificacion = " "
                        };


                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionIndividual.cshtml", dataInhumacionIndividual);

                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);

                    }
                    else
                    {
                        //CREMACION INDIVIDUAL

                        datosAutorizadorCremacion = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("CC4C8C4D-B557-4A5A-A2B3-520D757C5D06")));

                        var parentesco = await GetDescripcionDominio((datosAutorizadorCremacion.IdParentesco).ToString());

                        string nombreAutorizadorCremacion = "";

                        //validacion de nombre de Autorizador Cremacion

                        if (datosAutorizadorCremacion.SegundoNombre == null)
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;

                        }
                        else
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.SegundoNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;
                        }

                        if (datosAutorizadorCremacion.SegundoApellido != null)
                        {
                            nombreAutorizadorCremacion = nombreAutorizadorCremacion + ' ' + datosAutorizadorCremacion.SegundoApellido;

                        }


                        var dataCremacionIndividual = new Entities.DTOs.DetallePdfCremacionIndividualDto
                        {


                            FechaActual = DateTime.Now,
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = " ",
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            FechaCremacion = datoSolitud.FechaDefuncion.AddDays(1),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion.ToUpper(),
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            Edad = Utilities.ConvertTypes.GetdifFechas(DateTime.Parse(datosPersonaFallecida.FechaNacimiento), datoSolitud.FechaDefuncion), // GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento), Convert.ToDateTime(datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"))),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            AutorizadorCremacion = nombreAutorizadorCremacion.ToUpper(),
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            ObservacionCausaLabel = label,
                            ObservacionCausa = resumen.Data[0].ObservacionCausa,
                            CodigoVerificacion = " "
                        };

                       
                        var pdf = await _generatePdf.GetByteArray("Views/CremacionIndividual.cshtml", dataCremacionIndividual);
                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);
                    }

                }
                else
                {  //proceso fetal


                    datosPersonaMadre = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("342D934B-C316-46CB-A4F3-3AAC5845D246")));
                    string nombreMadre = "";
                    string nombreFallecido = "";
                   

                    //validacion de nombres de madre

                    if (datosPersonaMadre.SegundoNombre == null)
                    {
                        nombreMadre = datosPersonaMadre.PrimerNombre + ' ' + datosPersonaMadre.PrimerApellido;

                    }
                    else
                    {
                        nombreMadre = datosPersonaMadre.PrimerNombre + ' ' + datosPersonaMadre.SegundoNombre + ' ' + datosPersonaMadre.PrimerApellido;
                    }

                    if (datosPersonaMadre.SegundoApellido != null)
                    {
                        nombreMadre = nombreMadre + ' ' + datosPersonaMadre.SegundoApellido;

                    }

                    var funeraria = await GetFuneraria(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaMadre.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaMadre.TipoIdentificacion).ToString());
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var genero = await GetDescripcionDominio(datoSolitud.IdSexo.ToString());

                    var firmaAprobador = firmaAprobadorDB.Firma;
                    var firmaValidador = firmaValidadorDB.Firma;


                    //validacion de nombres de fallecido

                    if (genero.Data.Descripcion.ToUpper() == "MUJER")
                    {
                        nombreFallecido = "MORTINATO FEMENINO";

                    }
                    else if (genero.Data.Descripcion.ToUpper() == "HOMBRE")
                    {
                        nombreFallecido = "MORTINATO MASCULINO";
                    }
                    else
                    {
                        nombreFallecido = "MORTINATO";
                    }


                    String nombreCementeio = "";

                    if (cementerio.Data.EnBogota == true)
                    {
                        nombreCementeio = cementerio.Data.Cementerio.ToUpper();
                    }
                    else if (cementerio.Data.FueraBogota == true)
                    {
                        var departamento = await GetDescripcionDepartamento(cementerio.Data.IdDepartamento.ToString());
                        var municipio = await GetDescripcionMunicipio(cementerio.Data.IdMunicipio.ToString());
                        nombreCementeio = nombreCementeio = nombreCementeio = "FUERA DE BOGOTá, "+ departamento.Data.Descripcion.ToUpper() +" "+ municipio.Data.Descripcion.ToUpper();
                    }
                    else
                    {
                        var pais = await GetDescripcionDominio(cementerio.Data.IdPais.ToString());
                        nombreCementeio = "FUERA DEL PAÍS, "+ pais.Data.Descripcion.ToUpper() +" "+ cementerio.Data.Ciudad.ToUpper();
                    }

                    if (datoSolitud.IdTramite.Equals(Guid.Parse("AD5EA0CB-1FA2-4933-A175-E93F2F8C0060")))
                    {
                        //IHUMACION FETAL

                        var dataInhumacionFetal = new Entities.DTOs.DetallePdfInhumacionFetalDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = " ",
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            NombreMadre = nombreMadre.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            CodigoVerificacion = " "
                        };


                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionFetal.cshtml", dataInhumacionFetal);

                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);

                    }
                    else
                    {

                        //CREMACION FETAL

                        datosAutorizadorCremacion = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("CC4C8C4D-B557-4A5A-A2B3-520D757C5D06")));

                        var parentesco = await GetDescripcionDominio((datosAutorizadorCremacion.IdParentesco).ToString());

                        string nombreAutorizadorCremacion = "";

                        //validacion de nombre de Autorizador Cremacion

                        if (datosAutorizadorCremacion.SegundoNombre == null)
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;

                        }
                        else
                        {
                            nombreAutorizadorCremacion = datosAutorizadorCremacion.PrimerNombre + ' ' + datosAutorizadorCremacion.SegundoNombre + ' ' + datosAutorizadorCremacion.PrimerApellido;
                        }

                        if (datosAutorizadorCremacion.SegundoApellido != null)
                        {
                            nombreAutorizadorCremacion = nombreAutorizadorCremacion + ' ' + datosAutorizadorCremacion.SegundoApellido;

                        }

                        var dataCremacionFetal = new Entities.DTOs.DetallePdfCremacionFetalDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = " ",
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria.ToUpper(),
                            FullNameSolicitante = datoSolitud.RazonSocialSolicitante.ToUpper(),
                            FullNameTramitador = nombreValidador.ToUpper(),
                            FullNameFallecido = nombreFallecido.ToUpper(),
                            NombreMadre = nombreMadre.ToUpper(),
                            Nacionalidad = nacionalidad.Data.Descripcion.ToUpper(),
                            FechaFallecido = datoSolitud.FechaDefuncion.ToString("dd/MM/yyyy"),
                            FechaCremacion = datoSolitud.FechaDefuncion.AddDays(1).ToString("dd/MM/yyyy"),
                            HoraFallecido = datoSolitud.Hora,
                            Genero = genero.Data.Descripcion.ToUpper(),
                            Muerte = tipoMuerte.Data.Descripcion.ToUpper(),
                            FullNameMedico = nombreMedico.ToUpper(),
                            Cementerio = nombreCementeio.ToUpper(),
                            AutorizadorCremacion = nombreAutorizadorCremacion.ToUpper(),
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador,
                            CodigoVerificacion = " "
                        };

                        var pdf = await _generatePdf.GetByteArray("Views/CremacionFetal.cshtml", dataCremacionFetal);                       

                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);
                    }


                }

            }
            catch (System.Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<dynamic>(code: HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }


        public async Task<string> GetFormatoByTipoPlantilla(string IdPlantilla)
        {
            try
            {
                var result = await _repositoryFormato.GetAllAsync(p => p.IdPlantilla.Equals(Guid.Parse(IdPlantilla)));
                if (result == null)
                {
                    return null;

                }
                Formatos nuevo = new Formatos();
                nuevo = result[0];

                return nuevo.valor;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string agregarValoresDinamicos(string HTML, string[] llavesAReemplazar, string[] valoresDinamicos)
        {
            string nuevoHTML = HTML;
            string prueba = "Hola mundo " + llavesAReemplazar[0];

            for (int index = 0; index < llavesAReemplazar.Length; index++)
            {

                Console.WriteLine(nuevoHTML);

                nuevoHTML = nuevoHTML.Replace(llavesAReemplazar[index], valoresDinamicos[index]);
                Console.WriteLine(nuevoHTML);
            }

            return nuevoHTML;
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

        public async Task<ResponseBase<List<DatosFuneraria>>> GetFuneraria(string idSolicitud)
        {
            try
            {
                var result = await _repositoryDatosFuneraria.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idSolicitud)));

                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<List<DatosFuneraria>>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<List<DatosFuneraria>>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result.ToList(), count: result.Count());
                }

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<List<DatosFuneraria>>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }

        public async Task<ResponseBase<Dominio>> GetDescripcionDominio(string idDominio)
        {
            try
            {
                var result = await _repositoryDominio.GetAsync(predicate: p => p.Id.Equals(Guid.Parse(idDominio))); //.GetAllAsync(predicate: p => p.Id.Equals(Guid.Parse(idPais)));
                //var result = await _repositoryDatosFuneraria.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idPais)));

                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<Dominio>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<Dominio>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result, count: 1);
                }

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<Dominio>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }

        public async Task<ResponseBase<Departamento>> GetDescripcionDepartamento(string idDepartamento)
        {
            try
            {
                var result = await _repositoryDepartamento.GetAsync(predicate: p => p.IdDepartamento.Equals(Guid.Parse(idDepartamento))); //.GetAllAsync(predicate: p => p.Id.Equals(Guid.Parse(idPais)));
                //var result = await _repositoryDatosFuneraria.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idPais)));

                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<Departamento>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<Departamento>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result, count: 1);
                }

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<Departamento>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }

        public async Task<ResponseBase<Municipio>> GetDescripcionMunicipio(string idMunicipio)
        {
            try
            {
                var result = await _repositoryMunicipio.GetAsync(predicate: p => p.IdMunicipio.Equals(Guid.Parse(idMunicipio))); //.GetAllAsync(predicate: p => p.Id.Equals(Guid.Parse(idPais)));
                //var result = await _repositoryDatosFuneraria.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idPais)));

                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<Municipio>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<Municipio>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result, count: 1);
                }

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<Municipio>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }

        public async Task<ResponseBase<DatosCementerio>> GetCementerio(string idCementerio)
        {
            try
            {
                var result = await _repositoryDatosCementerio.GetAsync(predicate: p => p.IdDatosCementerio.Equals(Guid.Parse(idCementerio)));
                if (result == null)
                {
                    return new Entities.Responses.ResponseBase<DatosCementerio>(code: HttpStatusCode.OK, message: "No se encontraron registros");
                }
                else
                {
                    return new Entities.Responses.ResponseBase<DatosCementerio>(code: HttpStatusCode.OK, message: Middle.Messages.GetOk, data: result, count: 1);
                }

            }
            catch (Exception ex)
            {
                _telemetryException.RegisterException(ex);
                return new Entities.Responses.ResponseBase<DatosCementerio>(code: HttpStatusCode.InternalServerError, message: Middle.Messages.ServerError);
            }

        }

    }
}
