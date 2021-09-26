using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Megarender.Features.Exceptions;
using Megarender.Features.Modules.OrganizationModule;
using Megarender.Features.Modules.UserModule;
using Megarender.Domain;
using NUnit.Framework;

namespace Megarender.Features.IntegrationTests.OrganizationModule.Commands
{
    using static Testing;
    
    public class CreateOrganizationCommandTests : TestBase
    {
        private static readonly Fixture Fixture = new();

        [Test]
        public async Task CreateOrganizationCommand_ShouldCreateOrganizationAndBindToUser()
        {
            var currentUser = await SendAsync(Fixture.Create<CreateUserCommand>());
            var createOrganizationCommand = Fixture.Create<CreateOrganizationCommand>();
            createOrganizationCommand.CreatedBy = currentUser.Id;
            var currentOrganization = await SendAsync(createOrganizationCommand);
            currentOrganization.OrganizationUsers.Should().HaveCount(1);
            currentOrganization.CreatedBy.Id.Should().Be(currentUser.Id);
        }

        [Test]
        [TestCaseSource(nameof(CreateOrganizationCommands))]
        public async Task CreateOrganizationCommand_ShouldThrowAnErrors(CreateOrganizationCommand command, Action<Func<Task<Organization>>> assertation)
        {
            var currentUser = await SendAsync(Fixture.Create<CreateUserCommand>());
            command.CreatedBy = currentUser.Id;
            assertation(async () => await SendAsync(command));
        }

        private static IEnumerable<TestCaseData> CreateOrganizationCommands()
        {
            yield return new TestCaseData(
                Fixture.Build<CreateOrganizationCommand>().Without(o => o.Id).Create(),
                new Action<Func<Task<Organization>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("Id is null")
                        .Result.And.Properties.Should().HaveCount(1).And
                        .ContainKey(nameof(CreateOrganizationCommand.Id));
                }));
            yield return new TestCaseData(
                Fixture.Build<CreateOrganizationCommand>().Without(o => o.UniqueIdentifier).Create(),
                new Action<Func<Task<Organization>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("UniqueIdentifier is null")
                        .Result.And.Properties.Should().HaveCount(1).And.ContainKey(nameof(CreateOrganizationCommand.UniqueIdentifier));
                }));
            yield return new TestCaseData(
                Fixture.Build<CreateOrganizationCommand>().Without(o => o.CommandId).Create(),
                new Action<Func<Task<Organization>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("CommandId name is null")
                        .Result.And.Properties.Should().HaveCount(1).And
                        .ContainKey(nameof(CreateOrganizationCommand.CommandId));
                }));
            yield return new TestCaseData(
                Fixture.Build<CreateOrganizationCommand>().Without(o => o.UniqueIdentifier).Without(o => o.CommandId).Create(),
                new Action<Func<Task<Organization>>>(result =>
                {
                    FluentActions.Awaiting(result).Should()
                        .ThrowAsync<BusinessValidationException>("UniqueIdentifier and command id is null")
                        .Result.And.Properties.Should().HaveCount(2).And.ContainKey(nameof(CreateOrganizationCommand.UniqueIdentifier))
                        .And.ContainKey(nameof(CreateOrganizationCommand.CommandId));
                }));
        }
    }
}