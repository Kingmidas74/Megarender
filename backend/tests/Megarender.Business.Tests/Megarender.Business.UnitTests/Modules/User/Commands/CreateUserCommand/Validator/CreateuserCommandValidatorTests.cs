using System;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.UnitTests.Modules.User.Commands;
using Xunit;

namespace Megarender.Business.UnitTests.Modules.User.Validators
{
    public class CreateuserCommandValidatorTests:TestBaseFixture
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