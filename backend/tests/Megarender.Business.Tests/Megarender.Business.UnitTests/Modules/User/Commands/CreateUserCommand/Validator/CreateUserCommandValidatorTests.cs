using System;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.UnitTests;
using Xunit;

namespace Megarender.Business.Modules.UserModule.UnitTests
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