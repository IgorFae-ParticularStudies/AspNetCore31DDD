using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repositoty;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataset;
        public CepImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            // Aqui estou fazendo um join entre 3 tabelas
            // No include ligo a tabela de Cep a Municipio
            // No ThenInclude ligo a tabela de Muncipio a Uf
            return await _dataset.Include(c => c.Municipio)
                                .ThenInclude(m => m.Uf)
                                .FirstOrDefaultAsync(c => c.Cep.Equals(cep));
        }
    }
}
