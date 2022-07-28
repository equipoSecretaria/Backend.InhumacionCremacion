using Backend.InhumacionCremacion.Entities.DTOs;
using Backend.InhumacionCremacion.Entities.Models.Commons;
using Backend.InhumacionCremacion.Entities.Responses;
using System.Threading.Tasks;

namespace Backend.InhumacionCremacion.Entities.Interface.Business
{
    public interface IPoliticaSeguridadBusiness
    {
        Task<ResponseBase<int>> AddPoliticaSeguridad(PoliticaSeguridadDTO politicaSeguridadDTO);

        Task<ResponseBase<PoliticaSeguridad>> GetPoliticaSeguridad(string idPoliticaSeguridad);

    }
}
