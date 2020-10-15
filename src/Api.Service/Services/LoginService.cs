using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration;

        private IUserRepository _repository;

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);
                if (baseUser == null)
                {
                    // Retorna um objeto caso não ache o email no banco
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti é um id do token
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email)
                            // São tipos de claims para retornarmos no JWT
                        }
                    );
                    // Data de criação do token
                    DateTime createDate = DateTime.Now;
                    // Data de expiração do token usando a configuração definida no appsettings
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(
                        Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds"))); // Ajuste para var de ambiente

                    // Nova instância para geração do token
                    var handler = new JwtSecurityTokenHandler();
                    //Criação do token
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    //Retorno de um objeto customizado com o token e demais propriedades
                    return SuccessObject(createDate, expirationDate, token, baseUser);

                }
            }
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };
        }

        /// <summary>
        /// Retorno customizado quando token é gerado com sucesso
        /// </summary>        
        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                name = user.Name,
                message = "Usuário Logado com sucesso"
            };
        }

        /// <summary>
        /// Método para facilitar a criação do token
        /// </summary>        
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"), // Ajuste para var de ambiente
                Audience = Environment.GetEnvironmentVariable("Audience"), // Ajuste para var de ambiente
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
