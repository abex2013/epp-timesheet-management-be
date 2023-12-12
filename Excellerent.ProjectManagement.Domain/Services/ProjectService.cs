using Excellerent.ClientManagement.Domain.DTOs;
using Excellerent.ClientManagement.Domain.Entities;
using Excellerent.ClientManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ProjectManagement.Domain.DTOs;
using Excellerent.ProjectManagement.Domain.Entities;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.ServiceInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.ResourceManagement.Domain.DTOs;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.ProjectManagement.Domain.Services
{
    public class ProjectService : CRUD<ProjectEntity, Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ResourceManagement.Domain.Interfaces.Services.IEmployeeService _employeeService;
        private readonly IAssignResourceService _assignResourceService;

        private readonly IClientDetailsService _clientDetailsService;
       
        public ProjectService(IProjectRepository ProjectRepository, ResourceManagement.Domain.Interfaces.Services.IEmployeeService employeeService, IAssignResourceService assignResourceService, IClientDetailsService clientDetailsService) : base(ProjectRepository)

        {
            _projectRepository = ProjectRepository;
            _employeeService = employeeService;
            _assignResourceService = assignResourceService;
            _clientDetailsService = clientDetailsService;
        }
        public async Task<IEnumerable<ProjectEntity>> GetProjectByName(string projectName)
        {
            var data = _projectRepository.GetProjectByName(projectName);
            return (await data).Select(p => new ProjectEntity(p));
        }
        public async Task<Project> GetProjectById(Guid id)
        {
            return (await _projectRepository.GetProjectById(id));
        }
        public async Task<IEnumerable<ProjectEntity>> GetProjectFullData()
        {
            var data = _projectRepository.GetProjectFullData();
            return (await data).Select(p => new ProjectEntity(p));
        }

        public async Task<PredicatedResponseDTO> GetPaginatedProject(Guid? id, string searchKey, int? pageIndex, int? pageSize)
        {
            int itemPerPage = pageSize ?? 10;
            int PageIndex = pageIndex ?? 1;
            EmployeeDTO employee;
            var predicate = PredicateBuilder.True<Project>();
            List<ProjectEntity> projectEntities = new List<ProjectEntity>();

            if (id != null)
                predicate = predicate.And(p => p.Guid == id);
            else
                predicate = string.IsNullOrEmpty(searchKey) ? null
                           : predicate.And
                            (
                                p => p.ProjectName.ToLower().Contains(searchKey.ToLower())
                            );
            var projectData = (await _projectRepository.GetPaginatedProject(predicate, PageIndex, itemPerPage))
                    .Select(p => new ProjectEntity(p)
                    ).ToList();
            foreach (var data in projectData) {
                try
                {
                    var supervisor = _employeeService.GetSelection(data.SupervisorGuid);
                   
                    data.Supervisor = supervisor.Result;
                }
                catch (Exception ex) { }

                projectEntities.Add(data);
            }
            int TotalRowCount = await _projectRepository.CountAsync();
            return new PredicatedResponseDTO
            {
                Data = projectEntities,
                TotalRecord = TotalRowCount,   //total row count
                PageIndex = PageIndex,
                PageSize = itemPerPage,  // itemPerPage,
                TotalPage = TotalRowCount % itemPerPage == 0 ? TotalRowCount / itemPerPage : TotalRowCount / itemPerPage + 1
            };

        }

        public async Task<IEnumerable<ProjectEntity>> GetEmployeeProjects(Guid empId)
        {
            var projIds = (await _assignResourceService.GetProjectIdsByEmployee(empId)).ToList();
            List<ProjectEntity> projects = new List<ProjectEntity>();
            foreach (var id in projIds)
            {

                var projectData = (await _projectRepository.GetByGuidAsync(id.ProjectGuid));
                ProjectEntity projEnt = new ProjectEntity(projectData);
                projects.Add(projEnt);
            }
            return projects;
        }
        public async Task<IEnumerable<ProjectDTO>> GetAllSupervisorProjects(Guid supervisorGuid)
        {
           return (await GetProjectFullData()).Where(x => x.SupervisorGuid == supervisorGuid).Select(x => new ProjectDTO(x));
        }

        public async Task<IEnumerable<ClientDTO>> GetEmployeeClient(Guid empId)
        {
            var projIds = (await _assignResourceService.GetProjectIdsByEmployee(empId)).ToList();
            List<ProjectEntity> projects = new List<ProjectEntity>();
            List<ClientDetailsEntity> client = new List<ClientDetailsEntity>();
            foreach (var id in projIds)
            {

                var projectData = (await _projectRepository.GetByGuidAsync(id.ProjectGuid));
                ProjectEntity projEnt = new ProjectEntity(projectData);
                projects.Add(projEnt);
            }
            foreach (var pro in projects)
            {
                var selectedClient = (await _clientDetailsService.GetClientById(pro.ClientGuid));
                ClientDetailsEntity clientDetailsEntity = new ClientDetailsEntity(selectedClient);
                client.Add(clientDetailsEntity);
            }
            return client.Select(p => new ClientDTO(p));
        }

        public async Task<IEnumerable<ProjectDTO>> GetClientProjects(Guid clientGuid)
        {
            return (await GetProjectFullData())
                .Where(x => x.ClientGuid == clientGuid)
                .Select(x => new ProjectDTO(x));
        }

        public async Task<IEnumerable<ClientDTO>> GetProjectClient(Guid projectID)
        {
            List<ClientDetailsEntity> client = new List<ClientDetailsEntity>();
            var projectData = (await _projectRepository.GetByGuidAsync(projectID));
            var selectedClient = (await _clientDetailsService.GetClientById(projectData.ClientGuid));
            ClientDetailsEntity clientDetailsEntity = new ClientDetailsEntity(selectedClient);
            client.Add(clientDetailsEntity);
           
            return client.Select(p => new ClientDTO(p));
        }

        Task<IEnumerable<ClientManagement.Domain.DTOs.ClientDTO>> IProjectService.GetEmployeeClient(Guid empId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ClientManagement.Domain.DTOs.ClientDTO>> IProjectService.GetProjectClient(Guid projectID)
        {
            throw new NotImplementedException();
        }
    }
} 

