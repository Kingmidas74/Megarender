using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Megarender.Business.UnitTests.Models;

namespace Megarender.Business.Modules.UserModule.UnitTests
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
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForId");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.FirstName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.FirstName);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForFirstName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.SecondName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SecondName);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForSecondName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.SurName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SurName);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForSurName");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.Birthdate).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.Birthdate);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForBirthdate");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.CommandId).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.CommandId);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorForCommandId");
            Add(Fixture.Build<CreateUserCommand>().Without(u=>u.FirstName).Without(u=>u.SecondName).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(2);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.FirstName);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.SecondName);
                },
                $"{nameof(CreateUserCommandValidatorTests)}_ShouldHaveValidationErrorsForFirstAndSecondName");
        }
    }
}