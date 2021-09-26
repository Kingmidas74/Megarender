using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Megarender.Features.Modules.OrganizationModule;
using Megarender.Features.Modules.UserModule;
using Megarender.Features.Specifications;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Megarender.Features.UnitTests.Modules.OrganizationModule
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
                                    new FindByIdSpecification<User>(src.CreatedBy).ToExpression())));
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
                    .ToExpression());

            createdOrganization.Should().NotBeNull();
            createdOrganization.CreatedBy.Id.Should().Be(user.Id);

            var createdUser =
                await Context.Users.SingleAsync(new FindByIdSpecification<User>(user.Id).ToExpression());
            createdUser.Should().NotBeNull();
            createdUser.UserOrganizations.Should().HaveCount(1);
            createdUser.UserOrganizations.First().Organization.Id.Should().Be(organization.Id);
            createdUser.UserOrganizations.First().User.Id.Should().Be(user.Id);
        }
    }
}