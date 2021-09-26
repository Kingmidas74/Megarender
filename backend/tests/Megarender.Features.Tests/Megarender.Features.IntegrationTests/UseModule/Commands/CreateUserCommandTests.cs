using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Megarender.Features.Exceptions;
using Megarender.Features.Modules.UserModule;
using Megarender.Domain;
using NUnit.Framework;

namespace Megarender.Features.IntegrationTests.UserModule.Commands
{
    using static Testing;

    public class CreateUserCommandTests : TestBase
    {
        private static readonly Fixture Fixture = new();
        
        [Test]
        [TestCaseSource(nameof(CreateUserCommands))]
        public void CreateUserCommandRunner(CreateUserCommand command, Action<Func<Task<User>>> assertation)
        {
            assertation(async () => await SendAsync(command));
        }
        
        private static IEnumerable<TestCaseData> CreateUserCommands()
        {
            yield return new TestCaseData(
                Fixture.Create<CreateUserCommand>(),
                new Action<Func<Task<User>>>(result =>
                {
                    FluentActions.Awaiting(result).Invoke().Result.Should().BeOfType<User>("User was created successfully");
                }));
            yield return new TestCaseData(
                Fixture.Build<CreateUserCommand>().Without(o => o.Id).Create(),
                new Action<Func<Task<User>>>(result =>
                {
                    FluentActions.Awaiting(result).Should().ThrowAsync<BusinessValidationException>("Id is null")
                        .Result.And.Properties.Should().HaveCount(1).And.ContainKey(nameof(CreateUserCommand.Id));
                }));
            yield return new TestCaseData(
                Fixture.Build<CreateUserCommand>().Without(o=>o.CommandId).Create(),
                new Action<Func<Task<User>>>(result =>
                {
                    FluentActions.Awaiting(result).Should().ThrowAsync<BusinessValidationException>("Command id is null")
                        .Result.And.Properties.Should().HaveCount(1).And.ContainKey(nameof(CreateUserCommand.CommandId));
                }));
        }
    }
}