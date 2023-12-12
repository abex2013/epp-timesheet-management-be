using Excellerent.ClientManagement.Domain.DTOs;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface
{
    public interface IProjectService : ICRUD<ProjectEntity, Project>
    {
        Task<IEnumerable<ProjectEntity>> GetProjectByName(string projectName);
        Task<IEnumerable<ProjectDTO>> GetAllSupervisorProjects(Guid supervisorGuid);
        Task<IEnumerable<ProjectDTO>> GetClientProjects(Guid clientGuid);
        Task<IEnumerable<ProjectEntity>> GetProjectFullData();
        Task<PredicatedResponseDTO> GetPaginatedProject(Guid? id, string searchKey, int? pageIndex, int? pageSize);
        Task<IEnumerable<ProjectEntity>> GetEmployeeProjects(Guid empId);
        Task<IEnumerable<ClientDTO>> GetEmployeeClient(Guid empId);
        Task<IEnumerable<ClientDTO>> GetProjectClient(Guid projectID);
        Task<Project> GetProjectById(Guid id);
    }
}
