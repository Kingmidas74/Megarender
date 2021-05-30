using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.UnitTests.Models;

namespace Megarender.Business.UnitTests.Modules.UserModule
{
    public class CreateUserCommandValidatorTestData: TheoryData<CreateUserCommand, Action<CreateUserCommand, CreateUserCommandValidator>>
    {
        private static readonly Fixture Fixture = new();
        public CreateUserCommandValidatorTestData()
        {
            Add(Fixture.Create<CreateUserCommand>(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.ShouldNotHaveAnyValidationErrors();
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldNotHaveAnyValidationErrors");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.Id).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.Id);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForBirthdate");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.CommandId).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.CommandId);
                },
                
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorsForFirstAndSecondName");
        }
    }
}