using Excellerent.SharedInfrastructure.Context;
using Excellerent.SharedInfrastructure.Repository;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Excellerent.Usermanagement.Domain.Enums;

namespace Excellerent.Usermanagement.Infrastructure.Repositories
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        private readonly EPPContext _context;

        public UserRepository(EPPContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserListView>> GetUserDashBoardList(Expression<Func<User, bool>> predicate, int pageIndex, int pageSize)
        {
            var users = (predicate == null ? (await _context.Users.Include(x => x.Employee).ThenInclude(eo => eo.EmployeeOrganization).OrderByDescending(o => o.LastActivityDate).ToListAsync())
                : (await _context.Users.Include(x => x.Employee).ThenInclude(eo => eo.EmployeeOrganization).Where(predicate).OrderByDescending(o => o.LastActivityDate).ToListAsync()));

            var paginatedUserList = users.Skip(pageIndex * pageSize).Take(pageSize);
            List<UserListView> userViewModelList = new List<UserListView>();
            if (paginatedUserList.Count() > 0)
            {
                foreach (User user in paginatedUserList)
                {
                    userViewModelList.Add(
                        new UserListView()
                        {
                            UserId = user.Guid,
                            FullName = user.FirstName + ' ' + user.LastName,
                            JobTitle = user.Employee.EmployeeOrganization == null ? string.Empty : user.Employee.EmployeeOrganization.JobTitle,
                            Department = user.Employee.EmployeeOrganization == null ? string.Empty : user.Employee.EmployeeOrganization.Department,
                            Status = user.Status == UserStatus.Active ? "Active" : "Non-Active",
                            LastActivityDate = (DateTime)user.LastActivityDate
                        }
                    );
                }
            }
            else
            {
                userViewModelList = null;
            }
            return userViewModelList;
        }

        public async Task<int> GetUserDashBoardListCount(Expression<Func<User, bool>> predicate)
        {
            var users = (predicate == null ? (await _context.Users.Include(x => x.Employee).ThenInclude(eo => eo.EmployeeOrganization).OrderByDescending(o => o.LastActivityDate).ToListAsync())
                : (await _context.Users.Include(x => x.Employee).ThenInclude(eo => eo.EmployeeOrganization).Where(predicate).OrderByDescending(o => o.LastActivityDate).ToListAsync()));

            return users.Count;
        }
    }
}
