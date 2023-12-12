
using System;

namespace Excellerent.UserManagement.Presentation.Models.RequestDtos
{
    public class GroupSetPostRequestDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
