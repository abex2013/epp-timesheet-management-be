using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.Specification;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Services;
using Excellerent.Timesheet.Infrastructure.Specificationes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Infrastructure.Repositories
{
    public class TimesheetApprovalRepository : AsyncRepository<TimesheetApproval>, ITimesheetApprovalRepository
    {
        private readonly EPPContext _context;

        public TimesheetApprovalRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimesheetApproval>> AllTimesheetAproval(Expression<Func<TimesheetApproval, bool>> predicate, PaginationParams paginationParams)
        {
            int itemPerPage = paginationParams.PageSize ?? 10;
            int PageIndex = paginationParams.PageIndex ?? 1;
            if (paginationParams.status == ApprovalStatus.Requested)
            {
                if (paginationParams.SortBy != null)
                {
                    switch (paginationParams.SortBy)
                    {
                        case "Name":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.Employee.FirstName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.Employee.FirstName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "DateRange":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                                               .OrderByDescending(x => x.Timesheet.FromDate)
                                                             .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                                               .Include(y => y.Timesheet).ToListAsync())
                                                                : (await _context.TimesheetAprovals.
                                                                Where(predicate: predicate).OrderByDescending(x => x.Timesheet.FromDate)
                                                              .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.FromDate)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.FromDate)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "Client":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.Client.ClientName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.Client.ClientName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.Client.ClientName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Project.Client.ClientName)
                                   .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }

                        case "Project":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.ProjectName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.ProjectName)
                                .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderBy(x => x.Project.ProjectName)
                               .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderBy(x => x.Project.ProjectName)
                                .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        
                        default:
                            return predicate == null ? (await _context.TimesheetAprovals
                             .OrderByDescending(x => x.CreatedDate)
                            .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                             .Include(y => y.Timesheet).ToListAsync())
                              : (await _context.TimesheetAprovals.
                              Where(predicate: predicate).OrderByDescending(x => x.CreatedDate)
                              .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                    }
                }
                else
                {
                    return predicate == null ? (await _context.TimesheetAprovals.OrderBy(x => x.CreatedDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                        .Include(y => y.Timesheet).ToListAsync())
                        : (await _context.TimesheetAprovals.Where(predicate: predicate)
                        .OrderBy(x => x.CreatedDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                }

            }
            else
            {

            
                if (paginationParams.SortBy != null)
                {
                    switch (paginationParams.SortBy)
                    {
                        case "Name":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Timesheet.Employee.FirstName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                                     .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.Employee.FirstName)
                                    .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.Employee.FirstName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "DateRange":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                                               .OrderByDescending(x => x.Timesheet.FromDate)
                                                             .Skip((PageIndex - 1) * itemPerPage)
                                                         .Take(itemPerPage).Include(x => x.Project)
                                                               .Include(y => y.Timesheet).ToListAsync())
                                                                : (await _context.TimesheetAprovals.
                                                                Where(predicate: predicate).OrderByDescending(x => x.Timesheet.FromDate)
                                                                 .Skip((PageIndex - 1) * itemPerPage)
                                                        .Take(itemPerPage)
                                                        .Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Timesheet.FromDate)
                                     .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Timesheet.FromDate)
                                     .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }
                        case "Client":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                .OrderByDescending(x => x.Project.Client.ClientName)
                                .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                                .Include(y => y.Timesheet).ToListAsync())
                                 : (await _context.TimesheetAprovals.
                                 Where(predicate: predicate).OrderByDescending(x => x.Project.Client.ClientName)
                                 .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.Client.ClientName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                                   .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderBy(x => x.Project.Client.ClientName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                            }

                        case "Project":
                            if (paginationParams.sort == SharedModules.Seed.SortOrder.Descending)
                            {

                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderByDescending(x => x.Project.ProjectName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Project.ProjectName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                            else
                            {
                                return predicate == null ? (await _context.TimesheetAprovals
                                    .OrderBy(x => x.Project.ProjectName)
                                  .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project)
                                    .Include(y => y.Timesheet).ToListAsync())
                                     : (await _context.TimesheetAprovals.
                                     Where(predicate: predicate).OrderByDescending(x => x.Project.ProjectName)
                                      .Skip((PageIndex - 1) * itemPerPage)
                            .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());

                            }
                        default:
                            return predicate == null ? (await _context.TimesheetAprovals
                             .OrderByDescending(x => x.CreatedDate)
                              .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                             .Include(y => y.Timesheet).ToListAsync())
                              : (await _context.TimesheetAprovals.
                              Where(predicate: predicate).OrderByDescending(x => x.CreatedDate)
                               .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                    }
                }
                else
                {
                    return predicate == null ? (await _context.TimesheetAprovals.OrderByDescending(x => x.Timesheet.FromDate)
                         .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project)
                        .Include(y => y.Timesheet).ToListAsync())
                        : (await _context.TimesheetAprovals.Where(predicate: predicate)
                        .OrderByDescending(x => x.Timesheet.FromDate)
                        .Skip((PageIndex - 1) * itemPerPage)
                        .Take(itemPerPage).Include(x => x.Project).Include(y => y.Timesheet).ToListAsync());
                }   
            }

        }

        public async Task<IEnumerable<TimesheetApproval>> CountTotals(Expression<Func<TimesheetApproval, bool>> predicate)
        {
            return predicate == null ? (await _context.TimesheetAprovals.ToListAsync())
                                      : (await _context.TimesheetAprovals.Where(predicate: predicate).ToListAsync());
        }

        public async Task<TimesheetApproval> Get(Guid id)
        {
            return await _context.TimesheetAprovals.FindAsync(id);
        }

        public async Task<bool> Update(TimesheetApproval t)
        {
            _context.TimesheetAprovals.Update(t);
            _context.SaveChanges();
            return true;
        }

        public async Task UpdateProjectApprovalStatus(TimesheetApproval timesheetApproval)
        {
            _context.TimesheetAprovals.Attach(timesheetApproval);

            _context.Entry(timesheetApproval).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TimesheetApproval>> GetUserTimesheetApprovals(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId)
        {

            IEnumerable<TimesheetApproval> userTimeApprovalHistory = null;
            var spec = new UserTimesheetApprovalsSpec(paginationParams, employeeGuId);
            if (paginationParams.SortField == "Project")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Project.ProjectName, paginationParams.PageIndex,
                                          paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "Client")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Project.Client.ClientName, paginationParams.PageIndex,
                                         paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "Status")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Status, paginationParams.PageIndex,
                                       paginationParams.PageSize, paginationParams.Sort);
            else if (paginationParams.SortField == "DateRange")
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.Timesheet.ToDate, paginationParams.PageIndex,
                                          paginationParams.PageSize, paginationParams.Sort);
            else
                userTimeApprovalHistory = await GetAsync<object>(spec, x => x.CreatedDate, paginationParams.PageIndex,
                                           paginationParams.PageSize, SortOrder.Descending);
            return userTimeApprovalHistory;
        }

        public async Task<dynamic> GetUserTimesheetApprovalsPageData(UserTimesheetApprovalParamDto paginationParams, Guid employeeGuId)
        {
            var spec = new UserTimesheetApprovalsSpec(paginationParams, employeeGuId);
            int totalCount = await CountAsync(spec);
            if (paginationParams.PageIndex == 1 && paginationParams.ProjectFilters == null
                    && paginationParams.ClientFilters == null && paginationParams.StatusFilter == null)
            {
                var userAllTimesheetHistory = await FindAsync(spec);
                var projectFilter = userAllTimesheetHistory.GroupBy(x => x.ProjectId).Select(x => x.First())
                                                      .Select(x => new { x.ProjectId, x.Project.ProjectName })
                                                       .OrderBy(x => x.ProjectName).ToList();
                var clientFilter = userAllTimesheetHistory.GroupBy(x => x.Project.Client.ClientName).Select(x => x.First())
                                                       .Select(x => new { x.Project.Client.Guid, x.Project.Client.ClientName })
                                                        .OrderBy(x => x.ClientName).ToList();
                var statusFilter = userAllTimesheetHistory.GroupBy(x => x.Status).Select(x => x.First()).Select(x => x.Status)
                                                          .OrderBy(x => x.ToString()).ToList();

                return new
                {
                    Filters = new
                    {
                        ProjectFilter = projectFilter,
                        ClientFilter = clientFilter,
                        StatusFilter = statusFilter
                    },
                    TotalDataCount = totalCount
                };
            }

            return new
            {
                Filters = new { },
                TotalDataCount = totalCount
            };

        }
        public async Task<int> CountTimesheetApprovals(Expression<Func<TimesheetApproval, bool>> predicate)
        {
            return await _context.TimesheetAprovals.Where(predicate).CountAsync();
        }
    }
}
