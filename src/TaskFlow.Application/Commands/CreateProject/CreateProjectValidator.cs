using FluentValidation;
using MediatR;
using System.Numerics;
using TaskFlow.Application.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskFlow.Application.Commands.CreateProject;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required")
            .MaximumLength(200).WithMessage("Project name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required");
    }
}