using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Repositories
{
    public class TimeEntryRepository : AsyncRepository<TimeEntry>, ITimeEntryRepository
    {
        private readonly EPPContext _context;
        public TimeEntryRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TimeEntry> GetTimeEntry(Expression<Func<TimeEntry, bool>> predicate)
        {
            return await FindOneAsync(predicate);
        }

        public async Task<TimeEntry> GetTimeEntryForUpdateOrDelete(Expression<Func<TimeEntry, bool>> predicate)
        {
            return await FindOneAsyncForDelete(predicate);
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntries(Expression<Func<TimeEntry, bool>> predicate)
        {
            return await FindAsync(predicate);
        }

        public async Task<TimeEntry> AddTimeEntry(TimeEntry timeEntry)
        {
            return await AddAsync(timeEntry);
        }

        public async Task UpdateTimeEntry(TimeEntry timeEntry)
        {
            await UpdateAsync(timeEntry);
        }
    }
}
