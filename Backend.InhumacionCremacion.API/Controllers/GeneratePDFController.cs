﻿using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Interface.Business;
using Microsoft.AspNetCore.Mvc;

namespace Backend.InhumacionCremacion.API.Controllers
{
    /// <summary>
    ///     GeneratePDFController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratePDFController : ControllerBase
    {
        /// <summary>
        ///     The generate PDF business
        /// </summary>
        private readonly IGeneratePDFBusiness _generatePDFBusiness;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GeneratePDFController" /> class.
        /// </summary>
        /// <param name="generatePDFBusiness">The generate PDF business.</param>
        public GeneratePDFController(IGeneratePDFBusiness generatePDFBusiness)
        {
            _generatePDFBusiness = generatePDFBusiness;
        }

        /// <summary>
        ///     Generates the PDF.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GeneratePDF/{idSolicitud}")]
        public async Task<ActionResult> GeneratePDF(string idSolicitud)
        {
            var result = await _generatePDFBusiness.GeneratePDF(idSolicitud);

            return new FileStreamResult(result.Data, "application/pdf");
        }

        /// <summary>
        /// Visualizar the PDF.
        /// </summary>
        /// <returns></returns>
        [HttpGet("VisualizarPDF/{pathPDF}")]
        public ActionResult VisualizarPDF(string pathPDF)
        {
            string defaultPath = "C:\\Users\\afcan\\Downloads\\";

            defaultPath += pathPDF;
            var pdf = System.IO.File.ReadAllBytes(defaultPath);

            var pdfStream = new System.IO.MemoryStream();

            pdfStream.Write(pdf, 0, pdf.Length);

            pdfStream.Position = 0;

            return new FileStreamResult(pdfStream, "application/pdf");
        }


    }
}