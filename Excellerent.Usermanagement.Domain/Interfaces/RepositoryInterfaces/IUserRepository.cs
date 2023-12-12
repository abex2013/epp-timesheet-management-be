using Excellerent.SharedModules.Seed;
using Excellerent.SharedModules.DTO;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        public Task<List<UserListView>> GetUserDashBoardList(Expression<Func<User, Boolean>> predicate, int pageIndex, int pageSize);


        public Task<int> GetUserDashBoardListCount(Expression<Func<User, Boolean>> predicate);
    }
}
