using DevIO.APIYouCat.Controllers;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.APIYouCat.V1.Controllers
{

    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/testeYouCat")]
    public class TesteYouCatController : MainController
    {

        public TesteYouCatController(INotificador notificador, IUser appUser) : base(notificador, appUser)
        {
        }

        [HttpGet]
        public string Valor()
        {
            return "Estou na versão: V1";
        }
    }
}
