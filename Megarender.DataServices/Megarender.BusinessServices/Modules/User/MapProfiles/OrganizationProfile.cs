using Megarender.Domain;
using AutoMapper;

namespace Megarender.BusinessServices.Modules.UserModule
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile() {
            CreateMap<CreateOrganizationCommand,Organization>();
        }
    }
}