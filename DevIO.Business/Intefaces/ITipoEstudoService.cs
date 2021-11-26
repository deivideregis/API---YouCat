using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface ITipoEstudoService : IDisposable
    {
        Task<bool> Adicionar(TipoEstudo tipoestudo);
        Task<bool> Atualizar(TipoEstudo tipoestudo);
        Task<bool> Remover(Guid id);
    }
}
