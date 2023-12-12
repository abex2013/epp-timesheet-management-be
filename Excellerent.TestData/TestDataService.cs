using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.TestData.ClientManagement;
using Excellerent.TestData.ProjectManagement;
using Excellerent.TestData.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData
{
    public class TestDataService : ITestDataService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientStatusRepository _clientStatusRepository;
        private readonly IClientDetailsRepository _clientDetailsRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IAssignResourceRepository _assignResourceRepository;

        public TestDataService(
            IEmployeeRepository employeeRepository,
            IClientStatusRepository clientStatusRepository,
            IClientDetailsRepository clientDetailsRepository,
            IProjectStatusRepository projectStatusRepository,
            IProjectRepository projectRepository,
            IAssignResourceRepository assignResourceRepository
            )
        {
            _employeeRepository = employeeRepository;
            _clientStatusRepository = clientStatusRepository;
            _clientDetailsRepository = clientDetailsRepository;
            _projectStatusRepository = projectStatusRepository;
            _projectRepository = projectRepository;
            _assignResourceRepository = assignResourceRepository;
        }

        public async Task Add()
        {
            await ResourceManagementTestData.Add(_employeeRepository);
            await ClientManagementTestData.Add(_clientStatusRepository, _clientDetailsRepository);
            await ProjectManagementTestData.Add(_projectStatusRepository, _projectRepository, _assignResourceRepository);
        }

        public async Task Clear()
        {
            await ProjectManagementTestData.Clear(_projectStatusRepository, _projectRepository, _assignResourceRepository);
            await ClientManagementTestData.Clear(_clientStatusRepository, _clientDetailsRepository);
            await ResourceManagementTestData.Clear(_employeeRepository);
        }
    }
}
