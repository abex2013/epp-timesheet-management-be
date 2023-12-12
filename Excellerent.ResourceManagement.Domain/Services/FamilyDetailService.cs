using Excellerent.ResourceManagement.Domain.Entities;
using Excellerent.ResourceManagement.Domain.Interfaces.Repository;
using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.ResourceManagement.Domain.Models;
using Excellerent.SharedModules.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.ResourceManagement.Domain.Services
{
    public class FamilyDetailService : CRUD<FamilyDetailsEntity, FamilyDetails>, IFamilyDetailService
    {
        private readonly IFamilyDetailRepository _familyDetailRepository;

        public FamilyDetailService(IFamilyDetailRepository familyDetailRepository) : base(familyDetailRepository)
        {
            _familyDetailRepository = familyDetailRepository;
        }
   
        public async Task<IEnumerable<FamilyDetails>> GetFamilyDetailByEmployeeId(Guid EmployeeId)
        {
            return await _familyDetailRepository.GetFamilyDetailByEmployeeId(EmployeeId);
        }

    }
}
