using Backend.InhumacionCremacion.Entities.Interface.Business;
using Backend.InhumacionCremacion.Entities.Responses;
using Backend.InhumacionCremacion.Utilities.Telemetry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Wkhtmltopdf.NetCore;

namespace Backend.InhumacionCremacion.BusinessRules
{
    public class GeneratePDFBusiness : IGeneratePDFBusiness
    {
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
        /// Initializes a new instance of the <see cref="GeneratePDFBusiness"/> class.
        /// </summary>
        /// <param name="telemetryException">The telemetry exception.</param>
        /// <param name="generatePdf">The generate PDF.</param>
        public GeneratePDFBusiness(ITelemetryException telemetryException,
                                   IGeneratePdf generatePdf,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Solicitud> repositorySolicitud,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Persona> repositoryPersona,
                                   Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<Entities.Models.InhumacionCremacion.Formatos> repositoryFormato)
        {
            _telemetryException = telemetryException;
            _generatePdf = generatePdf;
            _repositorySolicitud = repositorySolicitud;
            _repositoryPersona = repositoryPersona;
            _repositoryFormato = repositoryFormato;
        }

        /// <summary>
        /// GeneratePDF
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public async Task<ResponseBase<dynamic>> GeneratePDF(string idSolicitud)
        {
            try
            {

                //var resultSolicitud = await _repositorySolicitud.GetAllAsync(predicate: p => p.IdSolicitud.Equals(Guid.Parse(idSolicitud)), include: inc => inc
                //                                                                                                     .Include(i => i.IdDatosCementerioNavigation)
                //                                                                                                     .Include(i => i.IdInstitucionCertificaFallecimientoNavigation)
                //                                                                                                     .Include(i => i.Persona));

                //datos generales

                //fechaActual
                //hora
                //numeroLicencia
                //certificadoDefuncion
                //funeraria
                //datos del tramitador nombres apellidos

                //datos del fallecido
                //nombres y apellidos
                //fecha fallecido
                //tipo de identificacion
                //Hora
                //muerte
                //genero
                //numero de documento
                //edad


                const string idInhumacionIndividual = "A289C362-E576-4962-962B-1C208AFA0273";
                const string idInhumacionFetal = "AD5EA0CB-1FA2-4933-A175-E93F2F8C0060";
                const string idCremacionIndividual = "E69BDA86-2572-45DB-90DC-B40BE14FE020";
                const string idCremacionFetal = "F4C4F874-1322-48EC-B8A8-3B0CAC6FCA8E";

                const string LicenciaInhumacionIndividual = "201E2CE5-FC99-4032-970E-18B8D8251656";
                const string LicenciaInhumacionFetal = "88FD1E95-DDD5-436C-ABB1-90EEA4976AD0";
                const string LicenciaCremacionFetal = "9FF9E542-7AEF-4A04-9594-3CCABB5E8DD1";
                const string LicenciaCremacionIndividual = "517E24F5-BFA5-4339-BB4D-9D6EA7261A4B";

                //ResponseBase<dynamic> HTML_PDF = null;
                Task<string> HTML_PDF = null;

                string[] datosLLavesInhumacionIndividual = {"~:~fecha_actual~:~, ~:~hora_actual~:~",
                        "~:~numero_de_licencia~:~","~:~numero_de_certificado_de_defuncion~:~", "~:~funeraria~:~",
                        "~:~fecha_actual~:~, ~:~hora_actual~:~","~:~nombre_completo_del_tramitador~:~", "~:~nombre_completo_del_fallecido~:~",
                        "~:~nacionalidad~:~", "~:~fecha_fallecido~:~, ~:~hora_fallecido~:~","~:~genero_fallecido~:~", "~:~tipo_de_identificacion~:~",
                    "~:~numero_de_identificacion~:~", "~:~tipo_de_muerte~:~", "~:~años_del_fallecido~:~", "~:~nombre_completo_del_medico~:~",
                        "~:~nombre_completo_del_cementerio~:~", "~:~nombre_de_quien_autoriza_la_cremacion~:~", "~:~parentesco_de_quien_autoriza_la_cremacion~:~",
                        "~:~firma_del_aprobador~:~", "~:~firma_del_validador~:~"};

                Persona datosPersonaFallecida;
                Persona datosMedico;
                Solicitud solicitud = new Solicitud();

               Solicitud datoSolitud = null;

               datoSolitud = await _repositorySolicitud.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)));

                // System.Threading.Thread.Sleep(5000);
                /*
                switch (datoSolitud.IdTramite.ToString().ToUpper())
                {
                    case idInhumacionIndividual:
                        HTML_PDF = GetFormatoByTipoPlantilla(LicenciaInhumacionIndividual);
                        break;
                    case idInhumacionFetal:
                        HTML_PDF = GetFormatoByTipoPlantilla(LicenciaInhumacionFetal);
                        break;
                    case idCremacionIndividual:
                        HTML_PDF = GetFormatoByTipoPlantilla(LicenciaCremacionIndividual);
                        break;
                    case idCremacionFetal:
                        HTML_PDF = GetFormatoByTipoPlantilla(LicenciaCremacionFetal);
                        break;
                    default:
                        break;
                }
                */

                if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")) || datoSolitud.IdTramite.Equals(Guid.Parse("E69BDA86-2572-45DB-90DC-B40BE14FE020")))
                {
                    Console.WriteLine("ingrese 1");

                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("01F64F02-373B-49D4-8CB1-CB677F74292C")));
                    datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));
                    var data = new Entities.DTOs.DetallePdfDto
                    {
                        //proceso individual
                        Edad = Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento)),
                        CertificadoDefuncion = datoSolitud.NumeroCertificado,
                        FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("hh:mm:ss"),
                        FechaFallecido = datoSolitud.FechaDefuncion,
                        NumeroLicencia = solicitud.NumeroCertificado,
                        Funeraria = "por definir",
                        FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                        FullNameTramitador = "por definir",
                        FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido
                    };
                    Console.WriteLine("ingrese 2.1.1.1");


                    if (datoSolitud.IdTramite.Equals(Guid.Parse("A289C362-E576-4962-962B-1C208AFA0273")))
                    {
                        Console.WriteLine("ingrese 2.1");

                        string[] datosDinamicosInhumacionIndividual = {data.FechaActual,
                        "numero de la licencia",data.CertificadoDefuncion, "~:~funeraria~:~",
                        data.FechaActual,"~:~nombre_completo_del_tramitador~:~", data.FullNameFallecido,
                        "~:~nacionalidad~:~", "~:~fecha_fallecido~:~, ~:~hora_fallecido~:~","~:~genero_fallecido~:~", "~:~tipo_de_identificacion~:~",
                    "~:~numero_de_identificacion~:~", "~:~tipo_de_muerte~:~", "~:~años_del_fallecido~:~", "~:~nombre_completo_del_medico~:~",
                        "~:~nombre_completo_del_cementerio~:~", "~:~nombre_de_quien_autoriza_la_cremacion~:~", "~:~parentesco_de_quien_autoriza_la_cremacion~:~",
                        "~:~firma_del_aprobador~:~", "~:~firma_del_validador~:~"};

                        //var pdf = _generatePdf.GetPDF("Views/Report.cshtml");
                        //var pdf = _generatePdf.GetPDF(agregarValoresDinamicos(HTML_PDF.Result, datosLLavesInhumacionIndividual, datosDinamicosInhumacionIndividual));

                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionIndividual.cshtml", datosDinamicosInhumacionIndividual);

                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);

                    }
                    else
                    {
                        Console.WriteLine("ingrese 2.2");
                        string[] datosDinamicosInhumacionIndividual = {data.FechaActual,
                        "numero de la licencia",data.CertificadoDefuncion, "~:~funeraria~:~",
                        data.FechaActual,"~:~nombre_completo_del_tramitador~:~", data.FullNameFallecido,
                        "~:~nacionalidad~:~", "~:~fecha_fallecido~:~, ~:~hora_fallecido~:~","~:~genero_fallecido~:~", "~:~tipo_de_identificacion~:~",
                    "~:~numero_de_identificacion~:~", "~:~tipo_de_muerte~:~", "~:~años_del_fallecido~:~", "~:~nombre_completo_del_medico~:~",
                        "~:~nombre_completo_del_cementerio~:~", "~:~nombre_de_quien_autoriza_la_cremacion~:~", "~:~parentesco_de_quien_autoriza_la_cremacion~:~",
                        "~:~firma_del_aprobador~:~", "~:~firma_del_validador~:~"};

                        //var pdf = _generatePdf.GetPDF("Views/Report.cshtml");
                        //var pdf = _generatePdf.GetPDF(agregarValoresDinamicos(HTML_PDF.Result, datosLLavesInhumacionIndividual, datosDinamicosInhumacionIndividual));
                        var pdf = await _generatePdf.GetByteArray("Views/CremacionIndividual.cshtml", datosDinamicosInhumacionIndividual);
                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);
                    }   
                    
                }
                else
                {  //proceso fetal
                    Console.WriteLine("madre 3");


                    datosPersonaFallecida = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("342D934B-C316-46CB-A4F3-3AAC5845D246")));

                    datosMedico = await _repositoryPersona.GetAsync(w => w.IdSolicitud.Equals(System.Guid.Parse(idSolicitud)) && w.IdTipoPersona.Equals(Guid.Parse("d8b0250b-2991-42a0-a672-8e3e45985500")));

                    var data = new Entities.DTOs.DetallePdfDto
                    {
                        Edad = Utilities.ConvertTypes.GetEdad(Convert.ToDateTime(datosPersonaFallecida.FechaNacimiento)),
                        CertificadoDefuncion = datoSolitud.NumeroCertificado,
                        FechaActual = DateTime.Now.ToString("dd/MM/yyyy"),
                        Hora = DateTime.Now.ToString("hh:mm:ss"),
                        FechaFallecido = datoSolitud.FechaDefuncion,
                        NumeroLicencia = solicitud.NumeroCertificado,
                        Funeraria = "por definir",
                        //FullNameFallecido = datosPersonaFallecida.PrimerNombre + " " + datosPersonaFallecida.SegundoNombre + " " + datosPersonaFallecida.PrimerApellido + " " + datosPersonaFallecida.SegundoApellido,
                        FullNameTramitador = "por definir",
                        FullNameMedico = datosMedico.PrimerNombre + " " + datosMedico.SegundoNombre + " " + datosMedico.PrimerApellido + " " + datosMedico.SegundoApellido
                    };

                    if (datoSolitud.IdTramite.Equals(Guid.Parse("AD5EA0CB-1FA2-4933-A175-E93F2F8C0060"))){
                        Console.WriteLine("ingrese 4");

                        string[] datosDinamicosInhumacionIndividual = {data.FechaActual,
                        "numero de la licencia",data.CertificadoDefuncion, "~:~funeraria~:~",
                        data.FechaActual,"~:~nombre_completo_del_tramitador~:~", data.FullNameFallecido,
                        "~:~nacionalidad~:~", "~:~fecha_fallecido~:~, ~:~hora_fallecido~:~","~:~genero_fallecido~:~", "~:~tipo_de_identificacion~:~",
                    "~:~numero_de_identificacion~:~", "~:~tipo_de_muerte~:~", "~:~años_del_fallecido~:~", "~:~nombre_completo_del_medico~:~",
                        "~:~nombre_completo_del_cementerio~:~", "~:~nombre_de_quien_autoriza_la_cremacion~:~", "~:~parentesco_de_quien_autoriza_la_cremacion~:~",
                        "~:~firma_del_aprobador~:~", "~:~firma_del_validador~:~"};

                        //var pdf = _generatePdf.GetPDF("Views/Report.cshtml");
                        // var pdf = _generatePdf.GetPDF(agregarValoresDinamicos(HTML_PDF.Result, datosLLavesInhumacionIndividual, datosDinamicosInhumacionIndividual));
                        var pdf = await _generatePdf.GetByteArray("Views/InhumacionFetal.cshtml", datosDinamicosInhumacionIndividual);

                        var pdfStream = new System.IO.MemoryStream();

                        pdfStream.Write(pdf, 0, pdf.Length);

                        pdfStream.Position = 0;

                        return new ResponseBase<dynamic>(code: HttpStatusCode.OK, message: "Solicitud OK", data: pdfStream);

                    }
                    else
                    {
                        string[] datosDinamicosInhumacionIndividual = {data.FechaActual,
                        "numero de la licencia",data.CertificadoDefuncion, "~:~funeraria~:~",
                        data.FechaActual,"~:~nombre_completo_del_tramitador~:~", data.FullNameFallecido,
                        "~:~nacionalidad~:~", "~:~fecha_fallecido~:~, ~:~hora_fallecido~:~","~:~genero_fallecido~:~", "~:~tipo_de_identificacion~:~",
                    "~:~numero_de_identificacion~:~", "~:~tipo_de_muerte~:~", "~:~años_del_fallecido~:~", "~:~nombre_completo_del_medico~:~",
                        "~:~nombre_completo_del_cementerio~:~", "~:~nombre_de_quien_autoriza_la_cremacion~:~", "~:~parentesco_de_quien_autoriza_la_cremacion~:~",
                        "~:~firma_del_aprobador~:~", "~:~firma_del_validador~:~"};

                        var pdf = await _generatePdf.GetByteArray("Views/CremacionFetal.cshtml", datosDinamicosInhumacionIndividual);
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


        public string agregarValoresDinamicos(string HTML, string[] llavesAReemplazar , string[] valoresDinamicos ) {
            string nuevoHTML = HTML;
            string prueba = "Hola mundo " + llavesAReemplazar[0];

            for (int index = 0; index<llavesAReemplazar.Length; index++) {

                Console.WriteLine(nuevoHTML);

                nuevoHTML = nuevoHTML.Replace(llavesAReemplazar[index], valoresDinamicos[index]);
                Console.WriteLine(nuevoHTML);
            }

            return nuevoHTML;
        }

    }
}
