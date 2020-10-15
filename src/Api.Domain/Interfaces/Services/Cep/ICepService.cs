using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.CEP;

namespace Api.Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(Guid Id);
        Task<CepDto> Get(string cep);       
        Task<CepDtoCreateResult> Post(CepDtoCreate cep);
        Task<CepDtoUpdateResult> Put(CepDtoUpdate cep);
        Task<bool> Delete(Guid Id);
    }
}
