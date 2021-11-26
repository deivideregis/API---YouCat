using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface ITipoEstudoRepository : IRepository<TipoEstudo>
    {
        Task<TipoEstudo> ObterTipoEstudoPorEstudo(Guid estudoid);
    }
}
