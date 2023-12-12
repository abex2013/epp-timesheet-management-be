using System;
using System.ComponentModel.DataAnnotations.Schema;
using Excellerent.SharedModules.Seed;
using System.ComponentModel.DataAnnotations;


namespace Excellerent.ResourceManagement.Domain.Models
{
    public class EmployeeOrganization : BaseAuditModel
    {
       

        public string Country { get; set; }

        [Required]
        public string DutyBranch { get; set; }

        public DutyBranch Branch { get; set; }

        [ForeignKey("Employees")]
        public Guid EmployeeId { get; set; }

        [Required]
        [EmailAddress]
        public string CompaynEmail { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        [Required]
        public string EmploymentType { get; set; }

        [Required]
        public string BusinessUnit { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string ReportingManager { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
