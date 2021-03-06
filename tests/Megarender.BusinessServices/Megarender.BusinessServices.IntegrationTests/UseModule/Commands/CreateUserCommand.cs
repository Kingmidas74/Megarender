using System;
using FluentAssertions;
using FluentValidation;
using Megarender.BusinessServices.Exceptions;
using Megarender.BusinessServices.Modules.UserModule;
using NUnit.Framework;

namespace Megarender.BusinessServices.IntegrationTests.UserModule.Commands
{
    using static Testing;

    public class CreateUserCommandTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateUserCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<BusinessValidationException>();
        }
    }
}