namespace Api.Domain.Security
{
    public class TokenConfigurations
    {        
        public string Audience { get; set; } //Audience - Público
        public string Issuer { get; set; } // Issuer = Emissor
        public int Seconds { get; set; } // Segundo de duração do Token
    }
}
