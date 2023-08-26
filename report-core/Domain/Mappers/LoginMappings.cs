using AutoMapper;
using report_core.Domain.DTOs.Login;
using report_core.Domain.DTOs.Project;
using report_core.Domain.Entities.Login;
using report_core.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report_core.Domain.Mappers
{
    public class LoginMappings:Profile
    {
        public LoginMappings() {
            CreateMap<Login, LoginDTO>() //Map from Developer Object to DeveloperDTO Object
               .ForMember(dest => dest.ID, source => source.MapFrom(source => source.Id))
               .ForMember(dest => dest.USER_NAME, source => source.MapFrom(source => source.UserName))
               .ForMember(dest => dest.PASSWORD, source => source.MapFrom(source => source.Password));

            CreateMap<LoginDTO, Login>() //Map from Developer Object to DeveloperDTO Object
               .ForMember(dest => dest.Id, source => source.MapFrom(source => source.ID))
               .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.USER_NAME))
               .ForMember(dest => dest.Password, source => source.MapFrom(source => source.PASSWORD));
        }
    }
}
