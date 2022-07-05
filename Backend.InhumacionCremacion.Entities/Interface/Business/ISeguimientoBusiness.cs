using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Backend.InhumacionCremacion.Entities.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface ISeguimientoBusiness
    {
        /// <summary>
        /// GetSeguimientoBySolicitud
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        Task<ResponseBase<List<Entities.DTOs.SeguimientoDto>>> GetSeguimientoBySolicitud(string idSolicitud);

        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="seguimiento"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> AddSeguimiento(Models.InhumacionCremacion.Seguimiento seguimiento);

        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="idConstante"></param>
        /// <returns></returns>
        Task<ResponseBase<Entities.DTOs.ConstanteDTO>> getConstante(string idConstante);

        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="idConstante"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> validarFirmaFuncionario(string idUsuario);

        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="idTabla"></param>
        /// <returns></returns>
        Task<ResponseBase<Licencia>> getLicencia(int numeroTramite);

        
    }
}
