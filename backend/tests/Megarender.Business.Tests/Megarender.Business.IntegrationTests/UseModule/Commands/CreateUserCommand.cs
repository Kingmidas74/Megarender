using System;
using FluentAssertions;
using FluentValidation;
using Megarender.Business.Exceptions;
using Megarender.Business.Modules.UserModule;
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

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<BusinessValidationException>();
        }
    }
}