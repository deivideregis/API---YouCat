using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class EstudosService : BaseService, IEstudosService
    {
        private readonly IEstudosRepository _estudosRepository;
        private readonly ITipoEstudoRepository _tipoestudoRepository;

        public EstudosService(IEstudosRepository estudosRepository,
                              ITipoEstudoRepository tipoestudoRepository,
                              INotificador notificador) : base(notificador)
        {
            _estudosRepository = estudosRepository;
            _tipoestudoRepository = tipoestudoRepository;
        }

        public async Task<bool> Adicionar(Estudos estudos)
        {
            if (!ExecutarValidacao(new EstudosValidation(), estudos)) return false;

            if (_estudosRepository.Buscar(f => f.Descricao == estudos.Descricao).Result.Any())
            {
                Notificar("Já existe a descrição informado.");
                return false;
            }

            if (_estudosRepository.Buscar(f => f.Pergunta == estudos.Pergunta).Result.Any())
            {
                Notificar("Já existe pergunta informado.");
                return false;
            }

            if (_estudosRepository.Buscar(f => f.Resposta == estudos.Resposta).Result.Any())
            {
                Notificar("Já existe resposta informado.");
                return false;
            }

            await _estudosRepository.Adicionar(estudos);

            return true;
        }

        public async Task<bool> Atualizar(Estudos estudos)
        {
            if (!ExecutarValidacao(new EstudosValidation(), estudos)) return false;

            if (_estudosRepository.Buscar(f => f.Descricao == estudos.Descricao && f.Id != estudos.Id).Result.Any())
            {
                Notificar("Já existe descrição informado.");
                return false;
            }

            else if (_estudosRepository.Buscar(f => f.Pergunta == estudos.Pergunta && f.Id != estudos.Id).Result.Any())
            {
                Notificar("Já existe pergunta informado.");
                return false;
            }

            else if (_estudosRepository.Buscar(f => f.NumeroYouCat.ToString() == estudos.NumeroYouCat.ToString() && f.Id != estudos.Id).Result.Any())
            {
                Notificar("Já existe número da questão YouCat informado.");
                return false;
            }

            await _estudosRepository.Atualizar(estudos);

            return true;
        }

        public async Task AtualizarTipoEstudo(TipoEstudo tipoestudo)
        {
            if (!ExecutarValidacao(new TipoEstudoValidation(), tipoestudo)) return;

            await _tipoestudoRepository.Atualizar(tipoestudo);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!_estudosRepository.ObterEstudosTipoEstudo(id).Result.Descricao.Any())
            {
                Notificar("Não há informação da descrição YoutCat cadastrados!");
                return false;
            }

            await _estudosRepository.Remover(id);

            return true;
        }

        public void Dispose()
        {
            _estudosRepository?.Dispose();
            _tipoestudoRepository?.Dispose();
        }
    }
}
