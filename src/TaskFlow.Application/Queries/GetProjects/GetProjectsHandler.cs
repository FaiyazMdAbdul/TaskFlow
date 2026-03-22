using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Queries.GetProjects;

public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IRepository<Project> _repository;

    public GetProjectsHandler(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAllAsync(cancellationToken);

        return projects
            .Where(p => p.OwnerId == query.UserId && !p.IsArchived)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OwnerId = p.OwnerId,
                CreatedAt = p.CreatedAt,
                IsArchived = p.IsArchived,
                TaskCount = p.Tasks.Count
            })
            .ToList();
    }
}