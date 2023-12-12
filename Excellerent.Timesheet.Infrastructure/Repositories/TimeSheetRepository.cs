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
    public class TimeSheetRepository : AsyncRepository<TimeSheet>, ITimeSheetRepository
    {
        private readonly EPPContext _context;
        public TimeSheetRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TimeSheet> GetLastEmployeeSheet(Guid employeeId)
        {
            return await _context.TimeSheets.Where(x => x.EmployeeId == employeeId).OrderBy(x => x.ToDate).LastOrDefaultAsync();
        }

        public async Task<TimeSheet> GetTimeSheet(Expression<Func<TimeSheet, bool>> predicate)
        {
            return await FindOneAsync(predicate);
        }

        public async Task<TimeSheet> AddTimeSheet(TimeSheet timesheet)
        {
            return await AddAsync(timesheet);
        }

        public async Task<bool> Update(TimeSheet t)
        {
            _context.TimeSheets.Update(t);
            _context.SaveChanges();
            return true;
        }
    }
}
