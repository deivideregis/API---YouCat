using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class EstudosRepository : Repository<Estudos>, IEstudosRepository
    {
        public EstudosRepository(YouCatDbContext context) : base(context)
        {

        }

        public async Task<Estudos> ObterEstudosTipoEstudo(Guid id)
        {
            return await Db.Estudos.AsNoTracking()
                .Include(c => c.TipoEstudo)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
