using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class ProjectStatusTestData
    {
        public static readonly List<ProjectStatus> _sampleData = new List<ProjectStatus>()
        {
            new ProjectStatus()
            {
                Guid = Guid.Parse("8443747E-D2FA-ACD8-E940-25A035B6AF73"),
                StatusName = "Active",
                AllowResource = true,
            },
            new ProjectStatus()
            {
                Guid = Guid.Parse("B9966412-58E8-25C6-87B9-4C237D012E69"),
                StatusName = "Inactive",
                AllowResource = false,
            },
            new ProjectStatus()
            {
                Guid = Guid.Parse("229B8CEB-93F2-AD65-CC9B-598B8711B038"),
                StatusName = "Terminated",
                AllowResource = false,
            },
            new ProjectStatus()
            {
                Guid = Guid.Parse("7D8A5DA6-2E78-168E-6D55-1E5F7E34A64D"),
                StatusName = "Finished",
                AllowResource = false,
            },

        };

        public static async Task Clear(IProjectStatusRepository repo)
        {
            IEnumerable<ProjectStatus> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IProjectStatusRepository repo)
        {
            IEnumerable<ProjectStatus> data = await repo.GetAllAsync();
            for (int i = 0; i < _sampleData.Count; i++)
            {
                Guid guid;
                var dataIn = data.Where(x => x.StatusName.Equals(_sampleData[i].StatusName));
                if (dataIn.Count() == 0)
                {
                    guid = Guid.NewGuid();

                    _sampleData[i].Guid = guid;
                    _sampleData[i] = await repo.AddAsync(_sampleData[i]);
                }
                else
                {
                    guid = dataIn.FirstOrDefault().Guid;
                    _sampleData[i] = dataIn.FirstOrDefault();
                }

            }
        }
    }
}
