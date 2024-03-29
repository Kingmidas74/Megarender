using AutoMapper;
using Megarender.Domain;

namespace Megarender.Features.Modules.UserModule
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<CreateAndAddUserToOrganizationCommand,User>();
            CreateMap<CreateUserCommand,User>();
        }
    }
}