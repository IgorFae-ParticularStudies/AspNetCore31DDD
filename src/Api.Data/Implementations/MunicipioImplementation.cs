using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repositoty;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {   
        private DbSet<MunicipioEntity> _dataset;
        public MunicipioImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<MunicipioEntity>();
        }
        public async Task<MunicipioEntity> GetCompleteByIBGE(int codIBGE)
        {
            // O Include faz o papel de um join, ou seja, tenho a FK na tabela municipio que me liga com
            // a tabela de Uf, assim consigo trazer um entidade completa Municipio + Uf
            return await _dataset.Include(m => m.Uf).FirstOrDefaultAsync(m => m.CodIBGE.Equals(codIBGE));
        }

        public async Task<MunicipioEntity> GetCompleteById(Guid id)
        {
            return await _dataset.Include(m => m.Uf).FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}
