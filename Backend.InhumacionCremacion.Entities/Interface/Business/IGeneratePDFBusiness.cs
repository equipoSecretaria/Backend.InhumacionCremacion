using Backend.InhumacionCremacion.Entities.Responses;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    /// <summary>
    /// IGeneratePDF
    /// </summary>
    public interface IGeneratePDFBusiness
    {
        /// <summary>
        /// GeneratePDF
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        Task<ResponseBase<dynamic>> GeneratePDF(string idSolicitud, string idValidador, string nombreValidar);
        Task<ResponseBase<dynamic>> GeneratePDFPrev(string idSolicitud, string idValidador, string nombreValidar);
        
    }
}
