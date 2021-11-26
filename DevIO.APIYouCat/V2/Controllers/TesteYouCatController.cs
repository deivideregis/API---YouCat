using DevIO.APIYouCat.Controllers;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevIO.APIYouCat.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/testeYouCat")]

    public class TesteYouCatController : MainController
    {
        private readonly ILogger _logger;

        public TesteYouCatController(INotificador notificador, IUser appUser, ILogger<TesteYouCatController> logger) : base(notificador, appUser)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Valor()
        {

            //throw new Exception("Error");

            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{
            //    e.Ship(HttpContext);
            //}

            _logger.LogTrace("Log de Trace");
            _logger.LogDebug("Log de Debug");
            _logger.LogInformation("Log de Informação");
            _logger.LogWarning("Log de Aviso");
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de Problema Critico");

            return "Está na versão - V2";
        }
    } 
}
