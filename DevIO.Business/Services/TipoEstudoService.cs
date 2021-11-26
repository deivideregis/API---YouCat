using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class TipoEstudoService : BaseService, ITipoEstudoService
    {
        private readonly ITipoEstudoRepository _tipoestudoRepository;

        public TipoEstudoService(INotificador notificador, 
                                 ITipoEstudoRepository tipoestudoRepository) : base(notificador)
        {
            _tipoestudoRepository = tipoestudoRepository;
        }

        public async Task<bool> Adicionar(TipoEstudo tipoestudo)
        {
            if (!ExecutarValidacao(new TipoEstudoValidation(), tipoestudo)) return false;

            if (_tipoestudoRepository.Buscar(f => f.Descricao == tipoestudo.Descricao).Result.Any())
            {
                Notificar("Já existe o tipo de estudo informado.");
                return false;
            }

            await _tipoestudoRepository.Adicionar(tipoestudo);

            return true;
        }

        public async Task<bool> Atualizar(TipoEstudo tipoestudo)
        {
            if (!ExecutarValidacao(new TipoEstudoValidation(), tipoestudo)) return false;

            if (_tipoestudoRepository.Buscar(f => f.Descricao == tipoestudo.Descricao && f.Id != tipoestudo.Id).Result.Any())
            {
                Notificar("Já existe um tipo de estudo informado.");
                return false;
            }

            await _tipoestudoRepository.Atualizar(tipoestudo);

            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!_tipoestudoRepository.ObterTipoEstudoPorEstudo(id).Result.Descricao.Any())
            {
                Notificar("Não há informação do tipo de estudo cadastrado!");
                return false;
            }

            await _tipoestudoRepository.Remover(id);

            return true;
        }

        public void Dispose()
        {
            _tipoestudoRepository?.Dispose();
        }
    }
}
