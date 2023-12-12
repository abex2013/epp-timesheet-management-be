using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using Excellerent.Usermanagement.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellerent.Usermanagement.Domain.Models
{
    public class User: BaseAuditModel
    {
       public string UserName { get; set; }
       public string Password { get; set; }
       public string Email { get; set; }
       public string Tel { get; set; }
       public string FirstName { get; set; }
       public string MiddleName { get; set; }
       public string LastName { get; set; }
       public UserStatus Status { get; set; }
       public DateTime? LastActivityDate { get; set; }
       [ForeignKey(nameof(EmployeeId))]
       public virtual Employee Employee { get; set; }
       public Guid EmployeeId { get; set; }

       public User()
        {
            Status = UserStatus.Active;
        }
    }
}
