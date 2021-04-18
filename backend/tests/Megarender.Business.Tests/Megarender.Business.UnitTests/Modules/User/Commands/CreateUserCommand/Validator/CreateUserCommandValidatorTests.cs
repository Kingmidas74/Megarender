using System;
using Megarender.Business.Modules.UserModule;
using Xunit;

namespace Megarender.Business.UnitTests.Modules.UserModule
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