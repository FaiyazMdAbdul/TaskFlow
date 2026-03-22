using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Queries.GetProjects;

public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
    public Guid UserId { get; set; }
}