using AutoMapper;
using DevIO.APIYouCat.Controllers;
using DevIO.APIYouCat.Extensions;
using DevIO.APIYouCat.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.APIYouCat.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/estudos")]
    public class EstudosController : MainController
    {
        private readonly IEstudosRepository _estudosRepository;
        private readonly IEstudosService _estudosService;
        private readonly ITipoEstudoRepository _tipoestudoRepository;
        private readonly IMapper _mapper;

        public EstudosController(IEstudosRepository estudoRepository,
                                 IMapper mapper,
                                 IEstudosService estudoService,
                                 INotificador notificador,
                                 ITipoEstudoRepository tipoestudoRepository,
                                 IUser user) : base(notificador, user)
        {
            _estudosRepository = estudoRepository;
            _mapper = mapper;
            _estudosService = estudoService;
            _tipoestudoRepository = tipoestudoRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<EstudosViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<EstudosViewModel>>(await _estudosRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstudosViewModel>> ObterPorId(Guid id)
        {
            var estudos = await ObterEstudosTipoEstudo(id);

            if (estudos == null) return NotFound();

            return estudos;
        }

        [ClaimsAuthorize("Estudos", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<EstudosViewModel>> Adicionar(EstudosViewModel estudosViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _estudosService.Adicionar(_mapper.Map<Estudos>(estudosViewModel));

            return CustomResponse(estudosViewModel);
        }

        [ClaimsAuthorize("Estudos", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EstudosViewModel>> Atualizar(Guid id, EstudosViewModel estudosViewModel)
        {
            if (id != estudosViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(estudosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _estudosService.Atualizar(_mapper.Map<Estudos>(estudosViewModel));

            return CustomResponse(estudosViewModel);
        }

        [ClaimsAuthorize("Estudos", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EstudosViewModel>> Excluir(Guid id)
        {
            var estudosViewModel = await ObterEstudosTipoEstudo(id);

            if (estudosViewModel == null) return NotFound();

            await _estudosService.Remover(id);

            return CustomResponse(estudosViewModel);
        }

        [HttpGet("tipoestudo/{id:guid}")]
        public async Task<TipoEstudoViewModel> ObterLogPorId(Guid id)
        {
            return _mapper.Map<TipoEstudoViewModel>(await _tipoestudoRepository.ObterPorId(id));
        }

        [ClaimsAuthorize("Estudos", "Atualizar")]
        [HttpPut("log/{id:guid}")]
        public async Task<IActionResult> AtualizarLog(Guid id, TipoEstudoViewModel tipoestudoViewModel)
        {
            if (id != tipoestudoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(tipoestudoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _estudosService.AtualizarTipoEstudo(_mapper.Map<TipoEstudo>(tipoestudoViewModel));

            return CustomResponse(tipoestudoViewModel);
        }

        private async Task<EstudosViewModel> ObterEstudosTipoEstudo(Guid id)
        {
            return _mapper.Map<EstudosViewModel>(await _estudosRepository.ObterEstudosTipoEstudo(id));
        }
    }
}
