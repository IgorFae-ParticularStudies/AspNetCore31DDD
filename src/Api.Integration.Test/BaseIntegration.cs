using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Dtos;
using application;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        // Para acessar o banco
        public MyContext myContext { get; set; }

        // Para fazer as requisições
        public HttpClient client { get; set; }

        public IMapper mapper { get; set; }

        // Para fixar nossa url base
        public string hostApi { get; set; }

        // Para receber a resposta da requisição
        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            // Passando a url base
            hostApi = "http://localhost:5000/api/";
            // Configuração para usar o Startup da 
            // Application, passando Testing para
            // usar as configurações
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            // Para subir um server em memória
            var server = new TestServer(builder);

            // Passando o contexto para meu server
            myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;

            // Para Criar a database
            myContext.Database.Migrate();

            // Instanciar um novo mapeamento
            mapper = new AutoMapperFixture().GetMapper();

            // Instanciar o client das requisições
            client = server.CreateClient();

        }

        public async Task AdicionarToken()
        {
            // Objeto de login
            var loginDto = new LoginDto()
            {
                Email = "igor.sfae@gmail.com"
            };

            // Para receber o objeto logado
            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}login", client);

            // Para ler esse resultado como string
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();

            //Para converter em um objeto tipado LoginResponseDto
            var loginObject = JsonConvert.DeserializeObject<LoginReponseDto>(jsonLogin);

            // Passando o token para o caberçalho da requisição
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginObject.accessToken);

        }

        /// <summary>
        /// Método para facilitar o post das requisições
        /// </summary>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostJsonAsync(object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataClass), System.Text.Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Destruir o contexto e o client da memória
        /// </summary>
        public void Dispose()
        {
            myContext.Dispose();
            client.Dispose();
        }
    }

    /// <summary>
    /// Classe para configurar os mapeamentos do AutoMapper
    /// </summary>
    public class AutoMapperFixture : IDisposable
    {
        public void Dispose() { }

        public IMapper GetMapper()
        {
            // Configurações da injeção de dependência do AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }
    }

}
