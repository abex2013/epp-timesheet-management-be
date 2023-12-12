using Excellerent.ClientManagement.Domain.Interfaces.RepositoryInterface;
using Excellerent.ClientManagement.Domain.Models;
using Excellerent.ClientManagement.Infrastructure.Repositories;
using Excellerent.TestData.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.TestData.ClientManagement
{
    public static class ClientDetailTestData
    {
        public static readonly List<ClientDetails> _sampleData = new List<ClientDetails>();

        public static async Task Clear(IClientDetailsRepository repo)
        {
            IEnumerable<ClientDetails> data = await repo.GetAllAsync();
            var reply = data.Select(x => repo.DeleteAsync(x));
        }

        public static async Task Add(IClientDetailsRepository repo)
        {
            List<ClientDetails> sampleData = new List<ClientDetails>()
            {
                new ClientDetails()
                {
                    Guid = Guid.Parse("5449242B-626D-E894-AD16-965AB64EDB2C"),
                    SalesPersonGuid = EmployeeTestData._clientEmployees[0].Guid,
                    ClientName = EmployeeTestData._clientEmployees[0].Organization,
                    ClientStatusGuid = ClientStatusTestData._sampleData[0].Guid,
                },
                new ClientDetails()
                {
                    Guid = Guid.Parse("E47A9226-8449-1883-654B-6D629B18A332"),
                    SalesPersonGuid = EmployeeTestData._clientEmployees[1].Guid,
                    ClientName = EmployeeTestData._clientEmployees[1].Organization,
                    ClientStatusGuid = ClientStatusTestData._sampleData[0].Guid,
                }
            };
            IEnumerable<ClientDetails> data = await repo.GetAllAsync();
            for (int i = 0; i < sampleData.Count; i++)
            {
                Guid guid;
                var dataIn = data.Where(x => x.ClientName.Equals(sampleData[i].ClientName));
                if (dataIn.Count() == 0)
                {
                    guid = Guid.NewGuid();

                    sampleData[i].Guid = guid;
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
