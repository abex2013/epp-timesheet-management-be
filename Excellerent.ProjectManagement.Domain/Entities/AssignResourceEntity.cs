using Excellerent.ProjectManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ProjectManagement.Domain.Entities
{
    public class AssignResourceEntity : BaseEntity<AssignResourcEntity>
    {
        public Guid ProjectGuid { get; set; }
        public virtual Project Project { get; set; }
        public Guid EmployeeGuid { get; set; }
        //public virtual Employee Empolyee { get; set; }
        public DateTime AssignDate { get; set; }
        public AssignResourceEntity()
        {
            this.IsActive = true;
        }

        public AssignResourceEntity(AssignResourcEntity assignResource) : base(assignResource)
        {
            IsActive = assignResource.IsActive;
            IsDeleted = assignResource.IsDeleted;
            Guid = assignResource.Guid;
            ProjectGuid = assignResource.ProjectGuid;
            Project = assignResource.Project;
            EmployeeGuid = assignResource.EmployeeGuid;
            //Empolyee = assignResource.Empolyee;
            AssignDate = assignResource.AssignDate;


        }
        public override AssignResourcEntity MapToModel()
        {
            AssignResourcEntity assignResource = new AssignResourcEntity();
            assignResource.Guid = Guid;
            assignResource.ProjectGuid = ProjectGuid;
            assignResource.Project = Project;
            assignResource.EmployeeGuid = EmployeeGuid;
           // assignResource.Empolyee = Empolyee;
            assignResource.IsActive = IsActive;
            assignResource.IsDeleted = IsDeleted;
            assignResource.AssignDate = AssignDate;
            return assignResource;
        }

        public override AssignResourcEntity MapToModel(AssignResourcEntity t)
        {
            AssignResourcEntity assignResource = t;
            assignResource.ProjectGuid = ProjectGuid;
            assignResource.Project = Project;
            assignResource.EmployeeGuid = EmployeeGuid;
            //assignResource.Empolyee = Empolyee;
            assignResource.IsActive = IsActive;
            assignResource.IsDeleted = IsDeleted;
            assignResource.AssignDate = AssignDate;

            return assignResource;

        }
    }
}
