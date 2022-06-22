using Backend.InhumacionCremacion.Entities.Responses;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface IUpdateRequestBusiness
    {
        /// <summary>
        /// UpdateRequest
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<ResponseBase<string>> UpdateRequest(DTOs.SolicitudDTO requestDTO);
        Task<ResponseBase<string>> UpdateMedico(string idMedico, string campo, string cambio);

        Task<ResponseBase<string>> UpdateConstante(string idConstante, string value);

        Task<ResponseBase<string>> UpdateStateRequest(string idSolicitud, string idEstado);


    }
}
