using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.InhumacionCremacion.Entities.Responses;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface IFormatoBusiness
    {
        /// <summary>
        /// GetFormatoByTipoPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        Task<ResponseBase<dynamic>> GetFormatoByTipoPlantilla(string idPlantilla);
    }
}