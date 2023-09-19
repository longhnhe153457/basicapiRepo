using APISecurityBasic.Models;
using APISecurityBasic.Models.DTO;
using AutoMapper;

namespace APISecurityBasic
{
    public class MappingConfig: Profile
    {
        public MappingConfig() {
        CreateMap<Villa,VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }  
    }
}
