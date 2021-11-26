using AutoMapper;
using DevIO.APIYouCat.ViewModels;
using DevIO.Business.Models;

namespace DevIO.APIYouCat.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //ReverseMap: faz o mesmo (Estudos, EstudosViewModel) ou (EstudosViewModel, Estudos)
            CreateMap<Estudos, EstudosViewModel>().ReverseMap();
            CreateMap<TipoEstudo, TipoEstudoViewModel>().ReverseMap();
        }
    }
}
