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
        /// The repository datos cementerio
        /// </summary>
        private readonly Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> _repositoryDatosCementerio;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratePDFBusiness"/> class.
        /// </summary>
        /// <param name="telemetryException">The telemetry exception.</param>
        /// <param name="generatePdf">The generate PDF.</param>
        public GeneratePDFBusiness(ITelemetryException telemetryException,
                                   IGeneratePdf generatePdf,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> repositorySolicitud,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> repositoryPersona,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Formatos> repositoryFormato,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.ResumenSolicitud> repositoryResumenSolicitud,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosFuneraria> repositoryDatosFuneraria,
                                   Entities.Interface.Repository.IBaseRepositoryCommons<Entities.Models.Commons.Dominio> repositoryDominio,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.DatosCementerio> repositoryDatosCementerio)
        {
            _telemetryException = telemetryException;
            _generatePdf = generatePdf;
            _repositorySolicitud = repositorySolicitud;
            _repositoryPersona = repositoryPersona;
            _repositoryFormato = repositoryFormato;
            _repositoryResumenSolicitud = repositoryResumenSolicitud;
            _repositoryDatosFuneraria = repositoryDatosFuneraria;
            _repositoryDominio = repositoryDominio;
            _repositoryDatosCementerio = repositoryDatosCementerio;
        }

        /// <summary>
        /// GeneratePDF
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<ResponseBase<dynamic>> GeneratePDF(string idSolicitud, string tramitador)
        {
            try
            {


                const string idInhumacionIndividual = "A289C362-E576-4962-962B-1C208AFA0273";
                const string idInhumacionFetal = "AD5EA0CB-1FA2-4933-A175-E93F2F8C0060";
                const string idCremacionIndividual = "E69BDA86-2572-45DB-90DC-B40BE14FE020";
                const string idCremacionFetal = "F4C4F874-1322-48EC-B8A8-3B0CAC6FCA8E";

                const string LicenciaInhumacionIndividual = "201E2CE5-FC99-4032-970E-18B8D8251656";
                const string LicenciaInhumacionFetal = "88FD1E95-DDD5-436C-ABB1-90EEA4976AD0";
                const string LicenciaCremacionFetal = "9FF9E542-7AEF-4A04-9594-3CCABB5E8DD1";
                const string LicenciaCremacionIndividual = "517E24F5-BFA5-4339-BB4D-9D6EA7261A4B";


                Persona datosPersonaFallecida;
                Persona datosMedico;
                Solicitud solicitud = new Solicitud();

                Solicitud datoSolitud = null;

                datoSolitud = await _repositorySolicitud.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)));


                if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")) || datoSolitud.IdTramite.Equals(Guid.Parse("E69BDA86-2572-45DB-90DC-B40BE14FE020")))
                {

                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("01F64F02-373B-49D4-8CB1-CB677F74292C")));
                    datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));

                    var resumen = await GetResumenSolicitud(idSolicitud);
                    var funeraria = await GetFuneraria(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaFallecida.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaFallecida.TipoIdentificacion).ToString());
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var parentesco = await GetDescripcionDominio((datosPersonaFallecida.IdParentesco).ToString());
                    var rutaAprobador = "\\Views\\firmas\\aprobador.png";
                    var rutaValidador = "\\Views\\firmas\\validador.png";
                    var firmaAprobador = System.IO.Directory.GetCurrentDirectory() + rutaAprobador;
                    var firmaValidador = System.IO.Directory.GetCurrentDirectory() + rutaValidador;


                    if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")))
                    {

                        //INHUMACION INDIVIDUAL


                        //Console.Write(resumen.Result.Data[0].NumeroLicencia);
                        //Console.Write(funeraria.Result.Data[0].Funeraria);
                        /*
                        Console.Write(resumen.Data[0].NumeroLicencia);
                        Console.Write(funeraria.Data[0].Funeraria);
                        Console.Write(nacionalidad.Data.Descripcion);
                        Console.Write(tipoIdentificacion.Data.Descripcion);
                        Console.Write(tipoMuerte.Data.Descripcion);
                        Console.Write(cementerio.Data.Cementerio);
                        */

                        //var CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
                        //
                        //Console.WriteLine(firma1);



                        var dataInhumacionIndividual = new Entities.DTOs.DetallePdfInhumacionIndividualDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria,
                            FullNameTramitador = tramitador,
                            FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            Nacionalidad = nacionalidad.Data.Descripcion,
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion,
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion,
                            Edad = Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento)),
                            FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido,
                            Cementerio = cementerio.Data.Cementerio,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador
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

                        /*
                        Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                        Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
                        Console.WriteLine(resumen.Data[0].NumeroLicencia);
                        Console.WriteLine(datoSolitud.NumeroCertificado);
                        Console.WriteLine(funeraria.Data[0].Funeraria);
                        Console.WriteLine(tramitador);
                        Console.WriteLine(datosPersonaFallecida.PrimerNombre);
                        Console.WriteLine(nacionalidad.Data.Descripcion);
                        Console.WriteLine(datoSolitud.FechaDefuncion);
                        Console.WriteLine(tipoIdentificacion.Data.Descripcion);
                        Console.WriteLine(datosPersonaFallecida.NumeroIdentificacion);
                        Console.WriteLine(tipoMuerte.Data.Descripcion);
                        Console.WriteLine(Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento)));
                        */

                        var dataCremacionIndividual = new Entities.DTOs.DetallePdfCremacionIndividualDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria,
                            FullNameTramitador = tramitador,
                            FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            Nacionalidad = nacionalidad.Data.Descripcion,
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            TipoIdentificacion = tipoIdentificacion.Data.Descripcion,
                            NumeroIdentificacion = datosPersonaFallecida.NumeroIdentificacion,
                            Muerte = tipoMuerte.Data.Descripcion,
                            Edad = Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento)),
                            FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido,
                            Cementerio = cementerio.Data.Cementerio,
                            AutorizadorCremacion = "PENDIENTE POR DEFINIR", // Puede ser quien hace la solicitud de cremación índividual.
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador
                        };

                        


                        //var pdf = _generatePdf.GetPDF("Views/Report.cshtml");
                        //var pdf = _generatePdf.GetPDF(agregarValoresDinamicos(HTML_PDF.Result, datosLLavesInhumacionIndividual, datosDinamicosInhumacionIndividual));
                        var pdf = await _generatePdf.GetByteArray("Views/CremacionIndividual.cshtml", dataCremacionIndividual);
                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);
                    }

                }
                else
                {  //proceso fetal


                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("342D934B-C316-46CB-A4F3-3AAC5845D246")));

                    datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));


                    var resumen = await GetResumenSolicitud(idSolicitud);
                    var funeraria = await GetFuneraria(idSolicitud);
                    var nacionalidad = await GetDescripcionDominio(datosPersonaFallecida.Nacionalidad);
                    var tipoIdentificacion = await GetDescripcionDominio((datosPersonaFallecida.TipoIdentificacion).ToString());
                    var tipoMuerte = await GetDescripcionDominio((datoSolitud.IdTipoMuerte).ToString());
                    var cementerio = await GetCementerio((datoSolitud.IdDatosCementerio).ToString());
                    var parentesco = await GetDescripcionDominio((datosPersonaFallecida.IdParentesco).ToString());
                    var rutaAprobador = "\\Views\\firmas\\aprobador.png";
                    var rutaValidador = "\\Views\\firmas\\validador.png";
                    var firmaAprobador = System.IO.Directory.GetCurrentDirectory() + rutaAprobador;
                    var firmaValidador = System.IO.Directory.GetCurrentDirectory() + rutaValidador;

                    if (datoSolitud.IdTramite.Equals(Guid.Parse("AD5EA0CB-1FA2-4933-A175-E93F2F8C0060")))
                    {
                        //IHUMACION FETAL

                        var dataInhumacionFetal = new Entities.DTOs.DetallePdfInhumacionFetalDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria,
                            FullNameTramitador = tramitador,
                            FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            NombreMadre = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            Nacionalidad = nacionalidad.Data.Descripcion,
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            Muerte = tipoMuerte.Data.Descripcion,
                            FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido,
                            Cementerio = cementerio.Data.Cementerio,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador
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

                        var dataCremacionFetal = new Entities.DTOs.DetallePdfCremacionFetalDto
                        {


                            FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                            Hora = DateTime.Now.ToString("hh:mm:ss"),
                            NumeroLicencia = resumen.Data[0].NumeroLicencia,
                            CertificadoDefuncion = datoSolitud.NumeroCertificado,
                            Funeraria = funeraria.Data[0].Funeraria,
                            FullNameTramitador = tramitador,
                            FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            NombreMadre = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            Nacionalidad = nacionalidad.Data.Descripcion,
                            FechaFallecido = datoSolitud.FechaDefuncion,
                            Muerte = tipoMuerte.Data.Descripcion,
                            FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido,
                            Cementerio = cementerio.Data.Cementerio,
                            AutorizadorCremacion = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                            Parentesco = parentesco.Data.Descripcion,
                            FirmaAprobador = firmaAprobador,
                            FirmaValidador = firmaValidador
                        };

                        var pdf = await _generatePdf.GetByteArray("Views/CremacionFetal.cshtml", dataCremacionFetal);
                        //var pdf = _generatePdf.GetPDF(agregarValoresDinamicos(HTML_PDF.Result, datosLLavesInhumacionIndividual, datosDinamicosInhumacionIndividual));

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
