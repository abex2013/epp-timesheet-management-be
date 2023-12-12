using Excellerent.ClientManagement.Domain.Models;
using Excellerent.SharedModules.Seed;
using System;

namespace Excellerent.ProjectManagement.Domain.Models
{
    public enum ProjectType
    {
        Internal,
        External
    }
    public class Project : BaseAuditModel 
    {
        public string ProjectName { get; set; }
        public ProjectType ProjectType { get; set; }
        public Guid SupervisorGuid { get; set; }
        public Guid ClientGuid { get; set; }
        public virtual ClientDetails Client { get; set; }
        public Guid ProjectStatusGuid { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
