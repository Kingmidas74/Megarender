using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation.TestHelper;
using Megarender.Business.Modules.OrganizationModule;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.Specifications;
using Megarender.Domain;
using Xunit;

namespace Megarender.Business.UnitTests.Modules.OrganizationModule
{
    public class CreateOrganizationCommandValidatorTests:TestBaseFixture
    {
        private readonly IMapper _mapper;

        public CreateOrganizationCommandValidatorTests()
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
        
        [Theory]
        [ClassData(typeof(CreateOrganizationCommandValidatorTestData))]
        public async Task CreateOrganizationCommandValidationTests(CreateOrganizationCommand command, Action<CreateOrganizationCommand, CreateOrganizationCommandValidator> assertation)
        {
            var user = Fixture.Build<CreateUserCommand>().Create();

            var handler = new CreateUserCommandHandler(Context, _mapper);    
            
            var result = await handler.Handle(user);
            await Context.SaveChangesAsync();
            
            var validator = new CreateOrganizationCommandValidator(Context);
            command.CreatedBy = result.Id;
            assertation(command, validator);
        }
        
        [Fact]
        public async Task CreateOrganizationCommandValidationTests_ShouldFailedForUsersWhoCreatedOrganizations()
        {
            var user = Fixture.Build<CreateUserCommand>().Create();

            var userHandler = new CreateUserCommandHandler(Context, _mapper);    
            
            var resultUser = await userHandler.Handle(user);
            
            await Context.SaveChangesAsync();

            var organization = Fixture.Build<CreateOrganizationCommand>().With(x=>x.CreatedBy,resultUser.Id).Create();

            var organizationHandler = new CreateOrganizationCommandHandler(Context, _mapper);

            await organizationHandler.Handle(organization);

            await Context.SaveChangesAsync();
            
            var newOrganization = Fixture.Build<CreateOrganizationCommand>().With(x=>x.CreatedBy,resultUser.Id).Create();
            
            var validator = new CreateOrganizationCommandValidator(Context);
            
            var validationResult = await validator.TestValidateAsync(newOrganization);
            validationResult.Errors.Should().HaveCount(1);
            validationResult.ShouldHaveValidationErrorFor(u=>u.CreatedBy);
        }
    }
}