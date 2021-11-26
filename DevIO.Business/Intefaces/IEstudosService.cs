using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IEstudosService : IDisposable
    {
        Task<bool> Adicionar(Estudos estudos);
        Task<bool> Atualizar(Estudos estudos);
        Task<bool> Remover(Guid id);

        Task AtualizarTipoEstudo(TipoEstudo tipoestudo);
    }
}
