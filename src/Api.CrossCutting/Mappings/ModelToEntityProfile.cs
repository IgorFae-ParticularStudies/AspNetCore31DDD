using Api.Domain.Entities;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserModel, UserEntity>().ReverseMap();
            CreateMap<UfEntity, UfModel>().ReverseMap();
            CreateMap<MunicipioEntity, MunicipioModel>().ReverseMap();
            CreateMap<CepEntity, CepModel>().ReverseMap();
        }
    }
}
