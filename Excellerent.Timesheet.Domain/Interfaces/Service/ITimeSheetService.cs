using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Interface.Service;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Interfaces.Service
{
    public interface ITimeSheetService : ICRUD<TimeSheetEntity, TimeSheet>
    {        
        // Get Timesheet by Timesheet Id
        Task<ResponseDTO> GetTimeSheet(Guid id);
        Task<TimeSheet> GetTimeSheetById(Guid id);
        // Get Timesheet by Employee Id and Date
        Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime? date);
        // Get Timesheet by Employee Id, fromDate, and toDate
        Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime fromDate, DateTime toDate);

        // Add Timesheet
        Task<ResponseDTO> AddTimeSheet(Guid employeeId, TimeEntryDto timeEntryDto);
        Task<ResponseDTO> AddTimeSheet(TimeSheetDto timeSheetDto, TimeEntryDto timeEntryDto);

        Task<bool> Update(TimeSheet timesheet);
    }
}
