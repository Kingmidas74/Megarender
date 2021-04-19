using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.Specifications;
using Megarender.Business.Modules.OrganizationModule;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Megarender.Business.UnitTests.Modules.OrganizationModule
{   
    public class DisableOrganizationCommandHandlerTests:TestBaseFixture
    {
        private readonly IMapper _mapper;

        public DisableOrganizationCommandHandlerTests()
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
        public async Task DisableOrganizationCommandHandler_ShouldDisableOrganization()
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

            var disableOrganizationCommand = Fixture.Build<DisableOrganizationCommand>()
                .With(x => x.Id, createdOrganization.Id)
                .With(x => x.ModifyBy, createdUser.Id)
                .Create();
            var disableOrganizationCommandHandler = new DisableOrganizationCommandHandler(Context);
            await disableOrganizationCommandHandler.Handle(disableOrganizationCommand);
            await Context.SaveChangesAsync();
            
            createdOrganization =
                await Context.Organizations.SingleOrDefaultAsync(new FindByIdSpecification<Organization>(organization.Id).And(new FindActiveSpecification<Organization>())
                    .ToExpression());

            createdOrganization.Should().BeNull();
            
            createdUser =
                await Context.Users.SingleAsync(new FindByIdSpecification<User>(user.Id).ToExpression());
            createdUser.Should().NotBeNull();
            //TODO: Many-to-Many relationships doesn't clear in unit test but correct work in integration test. SO: https://stackoverflow.com/questions/56736652/efcore-unable-to-remove-many-to-many-relation-in-memory-sqlite-but-works-in-pro
            //createdUser.UserOrganizations.Should().BeEmpty();
        }
    }
}