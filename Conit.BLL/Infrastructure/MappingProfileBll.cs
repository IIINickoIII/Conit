using AutoMapper;
using Conit.BLL.Dto;
using Conit.DAL.Entities;
using Conit.DAL.Entities.Identity;
using System.Linq;

namespace Conit.BLL.Infrastructure
{
    public class MappingProfileBll : Profile
    {
        public MappingProfileBll()
        {
            Mapper.CreateMap<ClientProfile, ClientProfileDto>().ReverseMap();

            Mapper.CreateMap<ApplicationRole, RoleDto>().ReverseMap();

            Mapper.CreateMap<ApplicationUser, UserDto>()
                .ForMember(m => m.RoleId, opt => opt.MapFrom(src => src.Roles.First().RoleId))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.ClientProfile.Name))
                .ForMember(m => m.Address, opt => opt.MapFrom(src => src.ClientProfile.Address))
                .ForMember(m => m.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            Mapper.CreateMap<Company, CompanyDto>()
                .ReverseMap();

            Mapper.CreateMap<Instruction, InstructionDto>()
                .ForMember(i => i.ProductDto, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();

            Mapper.CreateMap<InstructionPage, InstructionPageDto>()
                .ForMember(i => i.InstructionDto, opt => opt.MapFrom(src => src.Instruction))
                .ReverseMap();

            Mapper.CreateMap<Part, PartDto>()
                .ReverseMap();

            Mapper.CreateMap<PartProduct, PartProductDto>()
                .ForMember(p => p.PartDto, opt => opt.MapFrom(src => src.Part))
                .ForMember(p => p.ProductDto, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();

            Mapper.CreateMap<Product, ProductDto>()
                .ForMember(p => p.CompanyDto, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();
        }
    }
}
