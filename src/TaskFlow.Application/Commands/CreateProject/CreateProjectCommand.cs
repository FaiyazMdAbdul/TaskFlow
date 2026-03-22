using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Commands.CreateProject;

public class CreateProjectCommand : IRequest<ProjectDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}