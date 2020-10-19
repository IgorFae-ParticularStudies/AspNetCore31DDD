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
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repo = new UserImplementation(context);
                UserEntity _user = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName(),
                };

                var registroCriado = await _repo.InsertAsync(_user);
                Assert.NotNull(registroCriado);
                Assert.Equal(_user.Email, registroCriado.Email);
                Assert.Equal(_user.Name, registroCriado.Name);
                Assert.False(registroCriado.Id == Guid.Empty);

                _user.Name = Faker.Name.First();
                var registroAtualizado = await _repo.UpdateAsync(_user);
                Assert.NotNull(registroCriado);
                Assert.Equal(_user.Email, registroAtualizado.Email);
                Assert.Equal(_user.Name, registroAtualizado.Name);

                var registroExiste = await _repo.ExistsAsync(registroAtualizado.Id);
                Assert.True(registroExiste);

                var registroSelecionado = await _repo.SelectAsync(registroAtualizado.Id);
                Assert.NotNull(registroSelecionado);
                Assert.Equal(registroAtualizado.Email, registroSelecionado.Email);
                Assert.Equal(registroAtualizado.Name, registroSelecionado.Name);

                var todosRegistros = await _repo.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                var registroDeletado = await _repo.DeleteAsync(registroSelecionado.Id);
                Assert.True(registroDeletado);

                var usuarioPadrao = await _repo.FindByLogin("igor.sfae@gmail.com");
                Assert.NotNull(usuarioPadrao);
                Assert.Equal("igor.sfae@gmail.com", usuarioPadrao.Email);
                Assert.Equal("Administrador", usuarioPadrao.Name);
            }
        }
    }
}
