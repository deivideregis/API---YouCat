using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IEstudosRepository : IRepository<Estudos>
    {
        Task<Estudos> ObterEstudosTipoEstudo(Guid id);
    }
}
