using Backend.InhumacionCremacion.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface ISupportDocumentsBusiness
    {
        /// <summary>
        /// Adds the support documents.
        /// </summary>
        /// <param name="requestSupportDocumentsDTOs">The request support documents dt os.</param>
        /// <returns></returns>
        Task<ResponseBase<bool>> AddSupportDocuments(List<DTOs.RequestSupportDocumentsDTO> requestSupportDocumentsDTOs);

        /// <summary>
        /// Gets all suport by request identifier.
        /// </summary>
        /// <param name="idSolicitud">The identifier solicitud.</param>
        /// <returns></returns>
        Task<ResponseBase<List<Models.InhumacionCremacion.DocumentosSoporte>>> GetAllSuportByRequestId(string idSolicitud);

        /// <summary>
        /// Updates the suport.
        /// </summary>
        /// <param name="documentosSoporte">The documentos soporte.</param>
        /// <returns></returns>
        Task<ResponseBase<bool>> UpdateSuport(List<Entities.Models.InhumacionCremacion.DocumentosSoporte> documentosSoporte);
    }
}
