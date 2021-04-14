using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Megarender.Business.Exceptions;
using Megarender.Business.Modules.UserModule;
using Megarender.Domain;
using NUnit.Framework;

namespace Megarender.Business.IntegrationTests.UserModule.Commands
{
    using static Testing;

    public class CreateUserCommandTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().ThrowAsync<BusinessValidationException>();
        }
        
        [Test]
        [TestCaseSource(nameof(CreateUserCommandsData))]
        public void ShouldCreateUser(Guid id, string firstName, string secondName, string surName, DateTime birthdate)
        {
            FluentActions.Invoking(async () =>
                await SendAsync(new CreateUserCommand
                {
                    Id = id,
                    FirstName = firstName,
                    SecondName = secondName,
                    SurName = surName,
                    Birthdate = birthdate
                })).Should().BeOfType<User>();
        }
        
        private static IEnumerable<TestCaseData> CreateUserCommandsData()
        {
            yield return new TestCaseData(Guid.NewGuid(), "Denis", "Ed", "Suleymanov", new DateTime(1992, 10, 01));
        }
    }
}