using System;
using Megarender.Features.Modules.UserModule;
using Xunit;

namespace Megarender.Features.UnitTests.Modules.UserModule
{
    public class CreateUserCommandValidatorTests:TestBaseFixture
    {
        [Theory]
        [ClassData(typeof(CreateUserCommandValidatorTestData))]
        public void CreateUserCommandValidationTests(CreateUserCommand command, Action<CreateUserCommand, CreateUserCommandValidator> assertation)
        {
            var validator = new CreateUserCommandValidator(Context);
            assertation(command, validator);
        }
    }
}