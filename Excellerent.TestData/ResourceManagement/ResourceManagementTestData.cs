using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Excellerent.TestData.ResourceManagement
{
    public static class ResourceManagementTestData
    {
        public static async Task Clear(IEmployeeRepository employeeRepository)
        {
            await EmployeeTestData.Clear(employeeRepository);
        }
        public static async Task Add(IEmployeeRepository employeeRepository)
        {
            await EmployeeTestData.Add(employeeRepository);
        }
    }
}
