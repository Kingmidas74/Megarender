using System.Linq;
using AutoMapper;
using Megarender.Business.Specifications;
using Megarender.DataAccess;
using Megarender.Domain;

namespace Megarender.Business.Modules.OrganizationModule
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile()
        {
            CreateMap<CreateOrganizationCommand, Organization>()
                .ConvertUsing<OrganizationConverter>();
        }
    }
    
    public class OrganizationConverter : ITypeConverter<CreateOrganizationCommand, Organization>
    {
        private readonly IAPIContext _apiContext;
 
        public OrganizationConverter(IAPIContext apiContext)
        {
            _apiContext = apiContext;
        }

        public Organization Convert(CreateOrganizationCommand source, Organization destination, ResolutionContext context)
        {
            var user = _apiContext.Users.Single(
                new FindByIdSpecification<User>(source.CreatedBy).ToExpression());
            
            return new Organization
            {
                Id = source.Id,
                CreatedBy = user,
                UniqueIdentifier = source.UniqueIdentifier
            };
        }
    }
}