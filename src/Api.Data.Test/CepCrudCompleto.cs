using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;
        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Cep")]
        [Trait("CRUD", "CepEntity")]
        public async Task E_Possivel_Realizar_CRUD_Cep()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                // Preciso de um municipio para cadastrar um cep
                // e como é teste unitário, o que foi usado no teste
                // do municipio foi apagado lá
                MunicipioImplementation _repo = new MunicipioImplementation(context);
                MunicipioEntity _municipio = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
                };

                var _municipioCriado = await _repo.InsertAsync(_municipio);
                Assert.NotNull(_municipioCriado);
                Assert.Equal(_municipio.Nome, _municipioCriado.Nome);
                Assert.Equal(_municipio.CodIBGE, _municipioCriado.CodIBGE);
                Assert.Equal(_municipio.UfId, _municipioCriado.UfId);
                Assert.False(_municipioCriado.Id == Guid.Empty);

                CepImplementation _repoCep = new CepImplementation(context);
                CepEntity _cep = new CepEntity
                {
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 até 2000",
                    MunicipioId = _municipioCriado.Id
                };

                var _cepCriado = await _repoCep.InsertAsync(_cep);
                Assert.NotNull(_cepCriado);
                Assert.Equal(_cep.Cep, _cepCriado.Cep);
                Assert.Equal(_cep.Logradouro, _cepCriado.Logradouro);
                Assert.Equal(_cep.Numero, _cepCriado.Numero);
                Assert.Equal(_cep.MunicipioId, _cepCriado.MunicipioId);
                Assert.False(_cepCriado.Id == Guid.Empty);

                _cep.Logradouro = Faker.Address.StreetName();
                var _cepAtualizado = await _repoCep.UpdateAsync(_cep);
                Assert.NotNull(_cepAtualizado);
                Assert.Equal(_cep.Cep, _cepAtualizado.Cep);
                Assert.Equal(_cep.Logradouro, _cepAtualizado.Logradouro);
                Assert.Equal(_cep.Numero, _cepAtualizado.Numero);
                Assert.Equal(_cep.MunicipioId, _cepAtualizado.MunicipioId);
                Assert.True(_cepCriado.Id == _cepAtualizado.Id);

                var _cepExiste = await _repoCep.ExistsAsync(_cepAtualizado.Id);
                Assert.True(_cepExiste);

                var _cepSelecionado = await _repoCep.SelectAsync(_cepAtualizado.Id);
                Assert.NotNull(_cepSelecionado);
                Assert.Equal(_cepAtualizado.Cep, _cepSelecionado.Cep);
                Assert.Equal(_cepAtualizado.Logradouro, _cepSelecionado.Logradouro);
                Assert.Equal(_cepAtualizado.Numero, _cepSelecionado.Numero);
                Assert.Equal(_cepAtualizado.MunicipioId, _cepSelecionado.MunicipioId);

                // Aqui o cep retorna completo com municipio e uf
                _cepSelecionado = await _repoCep.SelectAsync(_cepAtualizado.Cep);
                Assert.NotNull(_cepSelecionado);
                Assert.Equal(_cepAtualizado.Cep, _cepSelecionado.Cep);
                Assert.Equal(_cepAtualizado.Logradouro, _cepSelecionado.Logradouro);
                Assert.Equal(_cepAtualizado.Numero, _cepSelecionado.Numero);
                Assert.Equal(_cepAtualizado.MunicipioId, _cepSelecionado.MunicipioId);
                Assert.NotNull(_cepSelecionado.Municipio);

                var _todosCeps = await _repo.SelectAsync();
                Assert.NotNull(_todosCeps);
                Assert.True(_todosCeps.Count() > 0);

                var _cepDeletado = await _repoCep.DeletetAsync(_cepSelecionado.Id);
                Assert.True(_cepDeletado);

                _todosCeps = await _repo.SelectAsync();
                Assert.NotNull(_todosCeps);
                Assert.True(_todosCeps.Count() == 0);
            }
        }
    }
}
