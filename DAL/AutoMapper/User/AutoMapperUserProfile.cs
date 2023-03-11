using AutoMapper;
using DAL.Models.VM;
using DAL.Models;

namespace DAL.AutoMapper.User
{
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<DAL.Models.User, LoginUserVM>().ReverseMap();
            CreateMap<DAL.Models.User, RegisterUserVM>().ReverseMap();
            CreateMap<DAL.Models.User, UserVM>().ReverseMap();
            CreateMap<DAL.Models.User, UserProfileVM>().ReverseMap();
		}
    }
}
