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

            Mapper.CreateMap<PartDto, PartViewModel>().ReverseMap();

            Mapper.CreateMap<ProductDto, ProductViewModel>().ReverseMap();

            Mapper.CreateMap<InstructionDto, InstructionViewModel>().ReverseMap();

            Mapper.CreateMap<InstructionPageDto, InstructionPageViewModel>().ReverseMap();
        }
    }
}