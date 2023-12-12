using Excellerent.SharedModules.Seed;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Repository
{
    public interface ITimeEntryRepository : IAsyncRepository<TimeEntry>
    {
        // Get one/single timesheet
        Task<TimeEntry> GetTimeEntry(Expression<Func<TimeEntry, bool>> predicate);

        Task<TimeEntry> GetTimeEntryForUpdateOrDelete(Expression<Func<TimeEntry, bool>> predicate);

        // Get multiple timesheets
        Task<IEnumerable<TimeEntry>> GetTimeEntries(Expression<Func<TimeEntry, bool>> predicate);

        Task<TimeEntry> AddTimeEntry(TimeEntry timeEntry);

        Task UpdateTimeEntry(TimeEntry timeEntry);
    }
}
