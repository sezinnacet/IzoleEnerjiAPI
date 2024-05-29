using AutoMapper;
using Entities.Concrete.DBModels;
using Entities.Concrete.RequestModels;
using Entities.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection ConfigureMapping(this IServiceCollection service)
        {
            var mappingConfig = new MapperConfiguration(i => i.AddProfile(new AutoMapperMappingProfile()));

            IMapper mapper = mappingConfig.CreateMapper();
            service.AddSingleton(mapper);

            return service;
        }

        public class AutoMapperMappingProfile : Profile
        {
            public AutoMapperMappingProfile()
            {
                // USER
                CreateMap<AddUserRequestModel, User>()
                    .ForMember(dest => dest.Id, src => src.MapFrom(x => 0))
                    .ForMember(dest => dest.Status, src => src.MapFrom(x => Status.Active))
                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                    .ForMember(dest => dest.Surname, src => src.MapFrom(x => x.Surname))
                    .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                    .ForMember(dest => dest.Password, src => src.MapFrom(x => x.Password))
                    .ForMember(dest => dest.InsertDate, src => src.MapFrom(x => DateTime.Now));
            }
        }
    }
}
