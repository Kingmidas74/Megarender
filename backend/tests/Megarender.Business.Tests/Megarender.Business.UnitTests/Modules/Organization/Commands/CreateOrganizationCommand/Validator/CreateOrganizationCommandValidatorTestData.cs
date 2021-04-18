using System;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Megarender.Business.UnitTests.Models;
using Megarender.Business.Modules.OrganizationModule;

namespace Megarender.Business.UnitTests.Modules.OrganizationModule
{
    public class CreateOrganizationCommandValidatorTestData: TheoryData<CreateOrganizationCommand, Action<CreateOrganizationCommand, CreateOrganizationCommandValidator>>
    {
        private static readonly Fixture Fixture = new();
        public CreateOrganizationCommandValidatorTestData()
        {
            Add(Fixture.Create<CreateOrganizationCommand>(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.ShouldNotHaveAnyValidationErrors();
                },
                $"{nameof(CreateOrganizationCommandValidatorTests)}_ShouldNotHaveAnyValidationErrors");
            Add(Fixture.Build<CreateOrganizationCommand>().Without(u=>u.Id).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.Id);
                },
                $"{nameof(CreateOrganizationCommandValidatorTests)}_ShouldHaveValidationErrorForId");
            Add(Fixture.Build<CreateOrganizationCommand>().Without(u=>u.CommandId).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.CommandId);
                },
                $"{nameof(CreateOrganizationCommandValidatorTests)}_ShouldHaveValidationErrorForCommandId");
            Add(Fixture.Build<CreateOrganizationCommand>().Without(u=>u.CreatedBy).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.CreatedBy);
                },
                $"{nameof(CreateOrganizationCommandValidatorTests)}_ShouldHaveValidationErrorForCreatedBy");
            Add(Fixture.Build<CreateOrganizationCommand>().Without(u=>u.UniqueIdentifier).Create(),
                async (command, validator) =>
                {
                    var validationResult = await validator.TestValidateAsync(command);
                    validationResult.Errors.Should().HaveCount(1);
                    validationResult.ShouldHaveValidationErrorFor(u=>u.UniqueIdentifier);
                },
                $"{nameof(CreateOrganizationCommandValidatorTests)}_ShouldHaveValidationErrorForUniqueIdentifier");
        }
    }
}