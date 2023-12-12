using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;


namespace Excellerent.ResourceManagement.Domain.Entities
{
    public class EmployeeOrganizationEntity : BaseEntity<EmployeeOrganization>
    { 
        public string Country { get; set; }

        public string DutyBranch { get; set; }

        public DutyBranch Branch { get; set; }

        public Guid EmployeeId { get; set; }

        public string CompaynEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string PhoneNumber { get; set; }

        public string BusinessUnit { get; set; }

        public string EmploymentType { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public string Department { get; set; }

        public string ReportingManager { get; set; }

        public string JobTitle { get; set; }

        public string Status { get; set; }

        public EmployeeOrganizationEntity()
        {

        }

        public EmployeeOrganizationEntity(EmployeeOrganization employeeOrganization) : base(employeeOrganization)
        {
            Guid = employeeOrganization.Guid;
            EmployeeId = employeeOrganization.EmployeeId;
            Country = employeeOrganization.Country;
            DutyBranch = employeeOrganization.DutyBranch; 
            CompaynEmail = employeeOrganization.CompaynEmail;
            PhoneNumber = employeeOrganization.PhoneNumber;
            ReportingManager = employeeOrganization.ReportingManager;
            BusinessUnit = employeeOrganization.BusinessUnit;
            Department = employeeOrganization.Department;
            JobTitle = employeeOrganization.JobTitle;
            JoiningDate = employeeOrganization.JoiningDate;
            TerminationDate = employeeOrganization.TerminationDate;
            Status = employeeOrganization.Status;
            CreatedDate = employeeOrganization.CreatedDate;
            CreatedbyUserGuid = employeeOrganization.CreatedbyUserGuid;
            IsActive = employeeOrganization.IsActive;
            IsDeleted = employeeOrganization.IsDeleted;
        }

        public override EmployeeOrganization MapToModel()
        {
            EmployeeOrganization employeeOrganization = new EmployeeOrganization();
            employeeOrganization.Guid = Guid;
            employeeOrganization.EmployeeId = EmployeeId;
            employeeOrganization.Country = Country;
            employeeOrganization.DutyBranch = DutyBranch;
            employeeOrganization.CompaynEmail = CompaynEmail;
            employeeOrganization.PhoneNumber = PhoneNumber;
            employeeOrganization.BusinessUnit = BusinessUnit;
            employeeOrganization.ReportingManager = ReportingManager;
            employeeOrganization.Department = Department;
            employeeOrganization.JobTitle = JobTitle;
            employeeOrganization.JoiningDate = JoiningDate;
            employeeOrganization.TerminationDate = TerminationDate;
            employeeOrganization.Status = Status;
            //employeeOrganization.CreatedDate = CreatedDate;
            //employeeOrganization.CreatedbyUserGuid = CreatedbyUserGuid;
            //employeeOrganization.IsActive = IsActive;
            //employeeOrganization.IsDeleted = IsDeleted;

            return employeeOrganization;

        }

        public override EmployeeOrganization MapToModel(EmployeeOrganization t)
        {
            EmployeeOrganization employeeOrganization = t;
            employeeOrganization.Guid = Guid;
            employeeOrganization.EmployeeId = EmployeeId;
            employeeOrganization.Country = Country;
            employeeOrganization.DutyBranch = DutyBranch;
            employeeOrganization.CompaynEmail = CompaynEmail;
            employeeOrganization.PhoneNumber = PhoneNumber;
            employeeOrganization.ReportingManager = ReportingManager;
            employeeOrganization.BusinessUnit = BusinessUnit;
            employeeOrganization.Department = Department;
            employeeOrganization.JobTitle = JobTitle;
            employeeOrganization.JoiningDate = JoiningDate;
            employeeOrganization.TerminationDate = TerminationDate;
            employeeOrganization.Status = Status;
            employeeOrganization.CreatedDate = CreatedDate;
            employeeOrganization.CreatedbyUserGuid = CreatedbyUserGuid;
            employeeOrganization.IsActive = IsActive;
            employeeOrganization.IsDeleted = IsDeleted;

            return employeeOrganization;
        }
    }
}
