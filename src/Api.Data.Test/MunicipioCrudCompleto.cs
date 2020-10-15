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
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;
        public MunicipioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Municipio")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task E_Possivel_Realizar_CRUD_Municipio()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
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

                _municipio.Nome = Faker.Address.City();
                _municipio.Id = _municipioCriado.Id;
                var _municipioAtualizado = await _repo.UpdateAsync(_municipio);
                Assert.NotNull(_municipioAtualizado);
                Assert.Equal(_municipio.Nome, _municipioAtualizado.Nome);
                Assert.Equal(_municipio.CodIBGE, _municipioAtualizado.CodIBGE);
                Assert.Equal(_municipio.Id, _municipioAtualizado.Id);
                Assert.True(_municipioCriado.Id == _municipio.Id);

                var _municipioExiste = await _repo.ExistsAsync(_municipioAtualizado.Id);
                Assert.True(_municipioExiste);

                var _municipioSelecionado = await _repo.SelectAsync(_municipioAtualizado.Id);
                Assert.NotNull(_municipioSelecionado);
                Assert.Equal(_municipioAtualizado.Nome, _municipioSelecionado.Nome);
                Assert.Equal(_municipioAtualizado.CodIBGE, _municipioSelecionado.CodIBGE);
                Assert.Equal(_municipioAtualizado.Id, _municipioSelecionado.Id);
                Assert.Null(_municipioSelecionado.Uf);

                _municipioSelecionado = await _repo.GetCompleteByIBGE(_municipioAtualizado.CodIBGE);
                Assert.NotNull(_municipioSelecionado);
                Assert.Equal(_municipioAtualizado.Nome, _municipioSelecionado.Nome);
                Assert.Equal(_municipioAtualizado.CodIBGE, _municipioSelecionado.CodIBGE);
                Assert.Equal(_municipioAtualizado.Id, _municipioSelecionado.Id);
                Assert.NotNull(_municipioSelecionado.Uf);

                _municipioSelecionado = await _repo.GetCompleteById(_municipioAtualizado.Id);
                Assert.NotNull(_municipioSelecionado);
                Assert.Equal(_municipioAtualizado.Nome, _municipioSelecionado.Nome);
                Assert.Equal(_municipioAtualizado.CodIBGE, _municipioSelecionado.CodIBGE);
                Assert.Equal(_municipioAtualizado.Id, _municipioSelecionado.Id);
                Assert.NotNull(_municipioSelecionado.Uf);

                var _todosMunicipios = await _repo.SelectAsync();
                Assert.NotNull(_todosMunicipios);
                Assert.True(_todosMunicipios.Count() > 0);

                var _municipioRemovido = await _repo.DeletetAsync(_municipioSelecionado.Id);
                Assert.True(_municipioRemovido);
            }
        }
    }
}
