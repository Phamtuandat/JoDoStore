using AutoMapper;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Enums
{
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}
public class GenderValueResolver : IValueResolver<User, UserResource, string>
{
    public string Resolve(User source, UserResource destination, string destMember, ResolutionContext context)
    {
        return source.Gender.ToString();
    }
}