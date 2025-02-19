using Data.Entities;
using Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }

    public async Task AddProjectAsync(Project project)
    {
        if (!project.IsValidPeriod)
        {
            throw new ArgumentException("End date must be after start date.");
        }

        if (string.IsNullOrWhiteSpace(project.ProjectNumber))
        {
            project.ProjectNumber = await _projectRepository.GenerateProjectNumberAsync();
        }

        await _projectRepository.AddAsync(project);
    }

    public async Task UpdateProjectAsync(Project project)
    {
        if (!project.IsValidPeriod)
        {
            throw new ArgumentException("End date must be after start date.");
        }

        await _projectRepository.UpdateAsync(project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteAsync(id);
    }
}

