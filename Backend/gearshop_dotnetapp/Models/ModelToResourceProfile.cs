using AutoMapper;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Models
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<RegisterResource, User>();
        }
    }
}
