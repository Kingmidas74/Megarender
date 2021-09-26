using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Megarender.Features.Exceptions;
using Megarender.Features.Modules.OrganizationModule;
using Megarender.Features.Modules.UserModule;
using NUnit.Framework;

namespace Megarender.Features.IntegrationTests.OrganizationModule.Commands
{
    using static Testing;
    
    public class DisableOrganizationCommandTests : TestBase
    {
        private static readonly Fixture Fixture = new();

        [Test]
        public async Task DisableOrganizationCommand_ShouldDisableCreatedOrganization()
        {
            var currentUser = await SendAsync(Fixture.Create<CreateUserCommand>());
            var createOrganizationCommand = Fixture.Create<CreateOrganizationCommand>();
            createOrganizationCommand.CreatedBy = currentUser.Id;
            var currentOrganization = await SendAsync(createOrganizationCommand);
            currentOrganization.OrganizationUsers.Should().HaveCount(1);
            currentOrganization.CreatedBy.Id.Should().Be(currentUser.Id);

            await SendAsync(Fixture.Build<DisableOrganizationCommand>().With(x => x.Id, currentOrganization.Id).Create());
            
            var createdUser = await SendAsync(Fixture.Build<GetUserQuery>().With(x => x.Id, currentUser.Id).Create());
            var createdOrganizations = await SendAsync(Fixture.Build<GetOrganizationsQuery>().Create());

            createdUser.UserOrganizations.Should().BeEmpty();
            createdOrganizations.Should().BeEmpty();
        }

        [Test]
        [TestCaseSource(nameof(DisableOrganizationCommands))]
        public async Task DisableOrganizationCommand_ShouldThrowAnErrors(DisableOrganizationCommand command,
            Action<Func<Task<Unit>>> assertation)
        {
            var currentUser = await SendAsync(Fixture.Create<CreateUserCommand>());
            var createOrganizationCommand = Fixture.Create<CreateOrganizationCommand>();
            createOrganizationCommand.CreatedBy = currentUser.Id;
            var currentOrganization = await SendAsync(createOrganizationCommand);
            command.Id = currentOrganization.Id;
            assertation(async () => await SendAsync(command));
        }
        
        private static IEnumerable<TestCaseData> DisableOrganizationCommands()
        {
            yield return new TestCaseData(
                Fixture.Build<DisableOrganizationCommand>().Without(o => o.ModifyBy).Create(),
                new Action<Func<Task<Unit>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("ModifyBy name is null")
                        .Result.And.Properties.Should().HaveCountGreaterOrEqualTo(1).And
                        .ContainKey(nameof(DisableOrganizationCommand.ModifyBy));
                }));
            yield return new TestCaseData(
                Fixture.Build<DisableOrganizationCommand>().Without(o => o.CommandId).Create(),
                new Action<Func<Task<Unit>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("CommandId name is null")
                        .Result.And.Properties.Should().HaveCountGreaterOrEqualTo(1).And
                        .ContainKey(nameof(DisableOrganizationCommand.CommandId));
                }));
            yield return new TestCaseData(
                Fixture.Build<DisableOrganizationCommand>().Without(o => o.ModifyBy).Without(o => o.CommandId).Create(),
                new Action<Func<Task<Unit>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("ModifyBy and command id is null")
                        .Result.And.Properties.Should().HaveCountGreaterOrEqualTo(2).And.ContainKey(nameof(DisableOrganizationCommand.ModifyBy))
                        .And.ContainKey(nameof(DisableOrganizationCommand.CommandId));
                }));
        }
    }
}