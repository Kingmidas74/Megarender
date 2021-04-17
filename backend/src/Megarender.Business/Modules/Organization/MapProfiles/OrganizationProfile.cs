using System.Linq;
using Megarender.Domain;
using AutoMapper;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile(IAPIContext apiContext)
        {
            // CreateMap<CreateOrganizationCommand, Organization>()
            //     .ForMember(dest => dest.CreatedBy,
            //         opt =>
            //             opt.MapFrom(src => apiContext.Users.Single(
            //                 new FindByIdSpecification<User>(src.CreatedBy).IsSatisfiedByExpression)));
        }
    }
}