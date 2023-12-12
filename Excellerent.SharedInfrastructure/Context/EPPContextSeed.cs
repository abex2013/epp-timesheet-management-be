using Excellerent.APIModularization.Providers;
using Excellerent.Usermanagement.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellerent.SharedInfrastructure.Context
{
   public class EPPContextSeed
    {
        public static async Task SeedAsync(EPPContext orderContext, ILogger<EPPContextSeed> logger)
        {
            //if (!orderContext.Permissions.Any())
            //{
            //    orderContext.Permissions.AddRange(GetPreconfiguredPermissions());
            //    await orderContext.SaveChangesAsync();
            //    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(EPPContext).Name);

            //}
        }
        public  List<Permission> GetPreconfiguredPermissions()
        { 
            return new List<Permission>
            {
                //Timesheet Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddTimeEntry, KeyValue ="create_timesheet", PermissionCode ="00101", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetTimesheet, KeyValue ="View_Timesheet", PermissionCode ="00102", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetTimeEntries, KeyValue ="GetTimeEntries", PermissionCode ="00103", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateTimeEntry, KeyValue ="Update_TimeEntry", PermissionCode ="00104", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.DeleteTimeEntry, KeyValue ="Delete_TimeEntry", PermissionCode ="00105", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetApprovalStatus, KeyValue ="GetApprovalStatus", PermissionCode ="00106", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddApprovalStatus, KeyValue ="Approve_timesheet", PermissionCode ="00107", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.RejectTimeSheet, KeyValue ="Reject_TimeSheet", PermissionCode ="00108", ParentCode="001"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.RequestForReview, KeyValue ="Request_ForReview", PermissionCode ="00109", ParentCode="001"},
               //TimeSheet Configuration Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetTimeSheetConfiguration, KeyValue ="GetTimeSheetConfiguration", PermissionCode ="00110", ParentCode="001"},
               
                //project management Module
                //Assign Resource Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddAssignResource, KeyValue ="Assign_Resource", PermissionCode ="00201", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetAssignResource, KeyValue ="View_Resources", PermissionCode ="00202", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetAssignResourceById, KeyValue ="GetAssignedResourceById", PermissionCode ="00203", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateAssignResource, KeyValue ="Update_Resources", PermissionCode ="00204", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.DeleteAssignResource, KeyValue ="Remove_Resource", PermissionCode ="00205", ParentCode="002"},
                //Project Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Get, KeyValue ="View_Project", PermissionCode ="00206", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Add, KeyValue ="Create_Project", PermissionCode ="00207", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Edit, KeyValue ="Update_Project", PermissionCode ="00208", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Remove, KeyValue ="Remove_Project", PermissionCode ="00209", ParentCode="002"},
               //Project Status Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Add, KeyValue ="AddProjectStatus", PermissionCode ="00210", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetAll, KeyValue ="ViewProjectStatus", PermissionCode ="00211", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetProjectStatusById, KeyValue ="GetProjectStatusById", PermissionCode ="00212", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Delete, KeyValue ="RemoveProjectStatus ", PermissionCode ="00213", ParentCode="002"},
                //Client Api Controller
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Get, KeyValue ="GetClient", PermissionCode ="00214", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Add, KeyValue ="AddClient", PermissionCode ="00215", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Edit, KeyValue ="EditClient", PermissionCode ="00216", ParentCode="002"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.Remove, KeyValue ="RemoveClient", PermissionCode ="00217", ParentCode="002"},          
                // Employees Api Controller
                new Permission(){Guid = Guid.NewGuid(),Level= "1", Name =ActionNames.GetAll, KeyValue ="GetAllEmployees" , PermissionCode ="00218", ParentCode="002"},

                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.GetById, KeyValue ="GetEmployeesById", PermissionCode ="00219", ParentCode="002"},

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Employee
                new Permission(){Guid = Guid.NewGuid(), Level= "0", Name =ActionNames.EmployeeAdmin, KeyValue ="Employee_Admin", PermissionCode ="003", ParentCode=""},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.CreateEmployee, KeyValue ="Create_Employee", PermissionCode ="00301", ParentCode="003"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.ViewEmployee, KeyValue ="View_Employee", PermissionCode ="00302", ParentCode="003"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateEmployee, KeyValue ="Update_Employee", PermissionCode ="00303", ParentCode="003"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.CreateMyProfile, KeyValue ="Create_My_Profile", PermissionCode ="00304", ParentCode="003"}, 
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.ViewMyProfile, KeyValue ="View_My_Profile", PermissionCode ="00305", ParentCode="003"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateMyProfile, KeyValue ="Update_My_Profile", PermissionCode ="00306", ParentCode="003"},

                //User Management
                new Permission(){Guid = Guid.NewGuid(), Level= "0", Name =ActionNames.UserManagementAdmin, KeyValue ="User_Management_Admin", PermissionCode ="004", ParentCode=""},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.CreateGroup, KeyValue ="Create_Group", PermissionCode ="00401", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.ViewGroup, KeyValue ="View_Group", PermissionCode ="00402", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateGroup, KeyValue ="Update_Group", PermissionCode ="00403", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.AddUser, KeyValue ="Add_User", PermissionCode ="00404", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.ViewUser, KeyValue ="View_User", PermissionCode ="00405", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.UpdateUser, KeyValue ="Update_User", PermissionCode ="00406", ParentCode="004"},
                new Permission(){Guid = Guid.NewGuid(), Level= "1", Name =ActionNames.DeleteUser, KeyValue ="Delete_User", PermissionCode ="00407", ParentCode="004"},

            };
        }
    }
}
