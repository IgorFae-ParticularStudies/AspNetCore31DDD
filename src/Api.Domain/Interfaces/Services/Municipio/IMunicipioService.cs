using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;

namespace Api.Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        Task<MunicipioDto> Get(Guid Id);
        Task<MunicipioDtoCompleto> GetCompleteById(Guid Id);
        Task<MunicipioDtoCompleto> GetCompleteByIBGE(int codIBGE);
        // Um pouco arriscado ter esse método,
        // devemos avaliar o crescimento da aplicação
        Task<IEnumerable<MunicipioDto>> GetAll(); 
        Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipio);
        Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipio);
        Task<bool> Delete(Guid Id);
    }
}
