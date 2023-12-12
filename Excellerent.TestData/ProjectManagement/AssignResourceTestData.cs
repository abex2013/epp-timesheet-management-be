using Excellerent.ProjectManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.TestData.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ProjectManagement
{
    public static class AssignResourceTestData
    {
        public static readonly List<AssignResourcEntity> _sampleData = new List<AssignResourcEntity>();

        public static async Task Clear(IAssignResourceRepository repo)
        {
            IEnumerable<AssignResourcEntity> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IAssignResourceRepository repo)
        {
            List<AssignResourcEntity> sampleData = new List<AssignResourcEntity>();

            int noofEmployees = EmployeeTestData._sampleEmployees.Count;
            int j = 1;
            foreach (Project project in ProjectTestData._sampleData)
            {
                for(int i = 0; i < 3; i++)
                {
                    sampleData.Add(new AssignResourcEntity()
                    {
                        ProjectGuid = project.Guid,
                        EmployeeGuid = EmployeeTestData._sampleEmployees[((i + 1) * j) % noofEmployees].Guid,
                        AssignDate = new DateTime()
                    });
                }
                j++;
            }
            
            IEnumerable<AssignResourcEntity> data = await repo.GetAllAsync();
            for (int i = 0; i < sampleData.Count; i++)
            {
                var dataIn = data.Where(x => x.ProjectGuid.Equals(sampleData[i].ProjectGuid) && x.EmployeeGuid.Equals(sampleData[i].EmployeeGuid));
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
