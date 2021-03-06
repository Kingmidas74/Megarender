using AutoMapper;
using Megarender.Domain;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<CreateAndAddUserToOrganizationCommand,User>();
            CreateMap<CreateUserCommand,User>();
        }
    }
}