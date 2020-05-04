using AutoMapper;
using Conit.BLL.Dto;
using Conit.WEB.ViewModels;

namespace Conit.WEB.App_Start
{
    public class MappingProfileWeb : Profile
    {
        public MappingProfileWeb()
        {
            Mapper.CreateMap<CompanyDto, CompanyViewModel>().ReverseMap();
        }
    }
}