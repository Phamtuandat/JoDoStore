using AutoMapper;
using App.Enums;
using App.Models.Identity;
using App.Dtos;

namespace App.Enums
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