using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class TipoEstudoRepository : Repository<TipoEstudo>, ITipoEstudoRepository
    {
        public TipoEstudoRepository(YouCatDbContext context) : base(context) { }

        public async Task<TipoEstudo> ObterTipoEstudoPorEstudo(Guid estudosId)
        {
            return await Db.TipoEstudo.AsNoTracking()
                .FirstOrDefaultAsync(f => f.EstudoId == estudosId);
        }
    }
}
