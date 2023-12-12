using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimeSheetRepository : IAsyncRepository<TimeSheet>
    {
        Task<TimeSheet> GetLastEmployeeSheet(Guid employeeId);
        Task<TimeSheet> GetTimeSheet(Expression<Func<TimeSheet, bool>> predicate);
        Task<TimeSheet> AddTimeSheet(TimeSheet timesheet);

        Task<bool> Update(TimeSheet timesheet);
    }
}
