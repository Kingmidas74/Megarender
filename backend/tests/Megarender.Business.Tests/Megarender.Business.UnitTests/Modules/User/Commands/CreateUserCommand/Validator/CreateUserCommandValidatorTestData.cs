using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Megarender.Business.Modules.UserModule;
using Megarender.Business.UnitTests.Models;
using Megarender.Business.UnitTests.Modules.User.Validators;

namespace Megarender.Business.UnitTests.Modules.User.Commands
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
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldNotHaveAnyValidationErrors");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.Id).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.Id);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForId");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.FirstName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.FirstName);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForFirstName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.SecondName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SecondName);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForSecondName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.SurName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SurName);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForSurName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.Birthdate).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.Birthdate);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForBirthdate");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.CommandId).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.CommandId);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorForCommandId");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.FirstName).Without(u=>u.SecondName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(2);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.FirstName);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SecondName);
                },
                $"{nameof(CreateuserCommandValidatorTests)}_ShouldHaveValidationErrorsForFirstAndSecondName");
        }
    }
}