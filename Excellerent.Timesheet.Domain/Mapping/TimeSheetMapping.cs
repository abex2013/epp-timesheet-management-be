using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Models;

namespace Excellerent.Timesheet.Domain.Mapping
{
    public static class TimeSheetMapping
    {
        public static TimeSheet MapToModel(this TimeSheetDto timeSheetDto) 
        {
            TimeSheet timeSheet = new TimeSheet();

            timeSheet.Guid = timeSheetDto.Guid;
            timeSheet.FromDate = timeSheetDto.FromDate;
            timeSheet.ToDate = timeSheetDto.ToDate;
            timeSheet.TotalHours = timeSheetDto.TotalHours;
            timeSheet.Status = timeSheetDto.Status;
            timeSheet.EmployeeId = timeSheetDto.EmployeeId;

            return timeSheet;
        }

        public static TimeSheet MapToModel(this TimeSheetDto timeSheetDto, TimeEntryDto timeEntryDto)
        {
            TimeSheet timeSheet = new TimeSheet();

            timeSheet.Guid = timeSheetDto.Guid;
            timeSheet.FromDate = timeSheetDto.FromDate;
            timeSheet.ToDate = timeSheetDto.ToDate;
            timeSheet.TotalHours = timeSheetDto.TotalHours;
            timeSheet.Status = timeSheetDto.Status;
            timeSheet.EmployeeId = timeSheetDto.EmployeeId;
            timeSheet.TimeEntry.Add(timeEntryDto.MapToModel());

            return timeSheet;
        }

        public static TimeSheetDto MapToDto(this TimeSheet timeSheet) 
        {
            TimeSheetDto timeSheetDto = new TimeSheetDto();

            timeSheetDto.Guid = timeSheet.Guid;
            timeSheetDto.FromDate = timeSheet.FromDate;
            timeSheetDto.ToDate = timeSheet.ToDate;
            timeSheetDto.TotalHours = timeSheet.TotalHours;
            timeSheetDto.Status = timeSheet.Status;
            timeSheetDto.EmployeeId = timeSheet.EmployeeId;

            return timeSheetDto;
        }
    }
}
