using Megarender.Domain;
using AutoMapper;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile() {
            CreateMap<CreateOrganizationCommand,Organization>();
        }
    }
}