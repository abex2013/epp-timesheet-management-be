using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.TestData.ClientManagement;
using Excellerent.TestData.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class ProjectTestData
    {
        private static readonly DateTime _startingDate = new DateTime();
        public static readonly List<Project> _sampleData = new List<Project>();

        public static async Task Clear(IProjectRepository repo)
        {
            IEnumerable<Project> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IProjectRepository repo)
        {
            List<Project> sampleData = new List<Project>()
            {
                new Project()
                {
                    Guid = Guid.Parse("24F9BF46-5DA6-B517-DC89-E05CDBAE541D"),
                    ProjectName = "Application Tracking",
                    SupervisorGuid = EmployeeTestData._managingEmployees[0].Guid,
                    ClientGuid = ClientDetailTestData._sampleData[0].Guid,
                    ProjectStatusGuid = ProjectStatusTestData._sampleData[0].Guid,
                    StartDate = _startingDate,
                },
                new Project()
                {
                    Guid = Guid.Parse("3EBAAA5B-4B48-384E-9D91-41A29A4EC4BB"),
                    ProjectName = "Resource Management",
                    SupervisorGuid = EmployeeTestData._managingEmployees[0].Guid,
                    ClientGuid = ClientDetailTestData._sampleData[1].Guid,
                    ProjectStatusGuid = ProjectStatusTestData._sampleData[0].Guid,
                    StartDate = _startingDate,
                },
                new Project()
                {
                    Guid = Guid.Parse("177C52A7-15DD-9608-48CA-5C348F5CE56E"),
                    ProjectName = "Client Management",
                    SupervisorGuid = EmployeeTestData._managingEmployees[1].Guid,
                    ClientGuid = ClientDetailTestData._sampleData[0].Guid,
                    ProjectStatusGuid = ProjectStatusTestData._sampleData[0].Guid,
                    StartDate = _startingDate,
                },
                new Project()
                {
                    Guid = Guid.Parse("1BB26E5E-9DF0-C5CE-19B6-C85836D73722"),
                    ProjectName = "Project Management",
                    SupervisorGuid = EmployeeTestData._managingEmployees[1].Guid,
                    ClientGuid = ClientDetailTestData._sampleData[0].Guid,
                    ProjectStatusGuid = ProjectStatusTestData._sampleData[0].Guid,
                    StartDate = _startingDate,
                },
                new Project()
                {
                    Guid = Guid.Parse("23551C5A-734F-4603-2A61-2B33ABC43316"),
                    ProjectName = "Timesheet Tracking",
                    SupervisorGuid = EmployeeTestData._managingEmployees[1].Guid,
                    ClientGuid = ClientDetailTestData._sampleData[1].Guid,
                    ProjectStatusGuid = ProjectStatusTestData._sampleData[0].Guid,
                    StartDate = _startingDate,
                }
            };
            IEnumerable<Project> data = await repo.GetAllAsync();
            for (int i = 0; i < sampleData.Count; i++)
            {
                var dataIn = data.Where(x => x.ProjectName.Equals(sampleData[i].ProjectName));
                if (dataIn.Count() == 0)
                {
                    _sampleData.Add(await repo.AddAsync(sampleData[i]));
                }
                else
                {
                    _sampleData.Add(dataIn.FirstOrDefault());
                }

            }
        }
    }
}
