using Excellerent.SharedModules.Seed;

namespace Excellerent.ClientManagement.Domain.Models
{
    public class OperatingAddress : BaseAuditModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
    }
}