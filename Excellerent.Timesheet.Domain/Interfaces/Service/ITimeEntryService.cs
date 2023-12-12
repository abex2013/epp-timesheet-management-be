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
    public interface ITimeEntryService : ICRUD<TimeEntryEntity, TimeEntry>
    {
        Task<ResponseDTO> GetTimeEntry(Guid timeEntryId);
        Task<ResponseDTO> GetTimeEntryForUpdateOrDelete(Guid timeEntryId);
        Task<ResponseDTO> GetTimeEntries(Guid timeSheetId, DateTime? date, Guid? projectId);
        Task<ResponseDTO> AddTimeEntry(Guid employeeId, TimeEntryDto timeEntryDto);
        Task<ResponseDTO> UpdateTimeEntry(TimeEntryDto timeEntryDto);
        Task<ResponseDTO> ApproveTimeSheet(Guid tsGuid);
        Task<ResponseDTO> AddTImeEntryForRangeOfDays(Guid employeeId, TimeEntryDto[] entries);
        Task<ResponseDTO> RemoveTimeEntryById(Guid timeEntryId);
        Task<ResponseDTO> RemoveTimeEntry(TimeEntryDto timeEntryDto);
    }
}
