
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces
{
    public interface IGroupSetRepository : IAsyncRepository<GroupSet>
    {

        List<GroupSet> GetAllUserGroupsDashboardAsync(Expression<Func<GroupSet, Boolean>> predicate, int pageindex, int pageSize);

        int AllUserGroupsDashboardCountAsync(Expression<Func<GroupSet, Boolean>> predicate);

    }
}
