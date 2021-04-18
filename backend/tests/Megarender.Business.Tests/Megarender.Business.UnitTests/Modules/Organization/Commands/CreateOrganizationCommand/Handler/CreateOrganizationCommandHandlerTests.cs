using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.Specifications;
using Megarender.Business.UnitTests;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Megarender.Business.Modules.OrganizationModule.UnitTests
{
    public class CreateOrganizationCommandHandlerTests:TestBaseFixture
    {
        private readonly IMapper _mapper;

        public CreateOrganizationCommandHandlerTests()
        {
            _mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateUserCommand, User>();
                    cfg.CreateMap<CreateOrganizationCommand, Organization>()
                        .ForMember(dest => dest.CreatedBy,
                            opt =>
                                opt.MapFrom(src => Context.Users.Single(
                                    new FindByIdSpecification<User>(src.CreatedBy).IsSatisfiedByExpression)));
                }
            ).CreateMapper();
        }

        [Fact]
        public async Task CreateOrganizationCommandHandler_ShouldCreateOrganizationAndBindToUser()
        {
            var user = Fixture.Build<CreateUserCommand>().Create();

            var userHandler = new CreateUserCommandHandler(Context, _mapper);    
            
            var resultUser = await userHandler.Handle(user);
            
            await Context.SaveChangesAsync();

            var organization = Fixture.Build<CreateOrganizationCommand>().With(x=>x.CreatedBy,resultUser.Id).Create();

            var organizationHandler = new CreateOrganizationCommandHandler(Context, _mapper);

            await organizationHandler.Handle(organization);

            await Context.SaveChangesAsync();

            var createdOrganization =
                await Context.Organizations.SingleAsync(new FindByIdSpecification<Organization>(organization.Id)
                    .IsSatisfiedByExpression);

            createdOrganization.Should().NotBeNull();
            createdOrganization.CreatedBy.Id.Should().Be(user.Id);

            var createdUser =
                await Context.Users.SingleAsync(new FindByIdSpecification<User>(user.Id).IsSatisfiedByExpression);
            createdUser.Should().NotBeNull();
            createdUser.UserOrganizations.Should().HaveCount(1);
            createdUser.UserOrganizations.First().Organization.CreatedBy.Should().Be(organization.Id);
        }
        
        
    }
}