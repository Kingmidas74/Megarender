using Megarender.Domain;
using AutoMapper;

namespace Megarender.Business.Modules.UserModule
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile() {
            CreateMap<CreateOrganizationCommand,Organization>();
        }
    }
}