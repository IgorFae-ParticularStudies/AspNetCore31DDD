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
    public class UfGets : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UfGets(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Gets de UF")]
        [Trait("Gets", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_Uf()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repo = new UfImplementation(context);
                UfEntity _uf = new UfEntity
                {
                    Id = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    Sigla = "SP",
                    Nome = "São Paulo",
                };

                var _registroExiste = await _repo.ExistsAsync(_uf.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repo.SelectAsync(_uf.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_uf.Nome, _registroSelecionado.Nome);
                Assert.Equal(_uf.Sigla, _registroSelecionado.Sigla);
                Assert.Equal(_uf.Id, _registroSelecionado.Id);

                var _todosRegistro = await _repo.SelectAsync();
                Assert.NotNull(_todosRegistro);
                Assert.True(_todosRegistro.Count() == 27);

            }
        }


    }
}
