using Excellerent.ResourceManagement.Domain.Interfaces.Services;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeEntryValidationService : ITimeEntryValidationService
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITimeEntryService _timeEntryService;
        private readonly ITimesheetApprovalService _timesheetApprovalService;
        private readonly ITimeSheetConfigService _timeSheetConfigService;
        private readonly IEmployeeService _employeeService;

        public TimeEntryValidationService(
            ITimeEntryService timeEntryService,
            ITimeSheetService timeSheetService,
            ITimesheetApprovalService timesheetAprovalService,
            ITimeSheetConfigService timeSheetConfigService,
            IEmployeeService employeeService)
        {
            _timeSheetService = timeSheetService;
            _timeEntryService = timeEntryService;
            _timesheetApprovalService = timesheetAprovalService;
            _timeSheetConfigService = timeSheetConfigService;
            _employeeService = employeeService;
        }

        //Validate time entry for new time entry
        public async Task ValidateTimeEntry(Guid employeeId, TimeEntryDto timeEntry)
        {
            if (timeEntry.Date > DateTime.Now)
            {
                throw new Exception("Can not add time entry for future date.");
            }
            else if (timeEntry.Hour <= 0)
            {
                throw new Exception("Can not add time entry with 0 or less than 0 hour.");
            }
            else if (timeEntry.Hour > 24)
            {
                throw new Exception("Can not add time entry with more than 24 hour.");
            }

            ResponseDTO responseDto = await _timeSheetService.GetTimeSheet(employeeId, timeEntry.Date);

            if (responseDto.Data == null)
            {
                return;
            }

            Guid timesheetId = (responseDto.Data as TimeSheetDto).Guid;

            responseDto = await _timeEntryService.GetTimeEntries(timesheetId, timeEntry.Date, null);

            IEnumerable<TimeEntryDto> timeEntryDtos = (responseDto.Data as IEnumerable<TimeEntryDto>);

            if (timeEntryDtos == null || timeEntryDtos.Count() == 0)
            {
                return;
            }

            float totalHour = 0;

            foreach (TimeEntryDto timeEntryDto in timeEntryDtos)
            {
                totalHour += timeEntryDto.Hour;
            }
            totalHour += timeEntry.Hour;

            if (totalHour > 24)
            {
                throw new Exception("Can not add time entry with more than 24 hour.");
            }

            IEnumerable<TimesheetApprovalEntity> timesheetApprovals = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetId);

            if (timesheetApprovals == null)
            {
                return;
            }

            if (timesheetApprovals.Count() > 0)
            {
                throw new Exception("Can not add time entry for timesheets that are approved or requested for approval.");
            }
        }

        // validate time entry for existing time entry
        public async Task ValidateTimeEntry(TimeEntryDto timeEntry)
        {
            if (timeEntry.Date > DateTime.Today)
            {
                throw new Exception("Can not add time entry for future date.");
            }
            else if (timeEntry.Hour <= 0)
            {
                throw new Exception("Can not add time entry with 0 or less than 0 hour.");
            }
            else if (timeEntry.Hour > 24)
            {
                throw new Exception("Can not add time entry with more than 24 hour.");
            }

            Guid timesheetId = timeEntry.TimeSheetId;

            ResponseDTO responseDto = await _timeEntryService.GetTimeEntryForUpdateOrDelete(timeEntry.Guid);

            TimeEntryDto[] timeEntryDtos = (responseDto.Data as TimeEntryDto[]);

            if (timeEntryDtos == null || timeEntryDtos.Length == 0)
            {
                return;
            }

            float totalHour = 0;

            foreach (TimeEntryDto timeEntryDto in timeEntryDtos)
            {
                totalHour += timeEntryDto.Hour;
            }
            totalHour += timeEntry.Hour;

            if (totalHour > 24)
            {
                throw new Exception("Can not add time entry with more than 24 hour.");
            }

            IEnumerable<TimesheetApprovalEntity> timesheetApprovals = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetId);

            if (timesheetApprovals == null || timesheetApprovals.Count() == 0)
            {
                return;
            }

            foreach (TimesheetApprovalEntity timesheetApproval in timesheetApprovals)
            {
                if (timesheetApproval.ProjectId == timeEntry.ProjectId && timesheetApproval.Status != ApprovalStatus.Rejected)
                {
                    throw new Exception("Can not add time entry for timesheets that are approved or requested for approval.");
                }
            }
        }
        public async Task ValidateDeleteTimeEntry(TimeEntryDto timeEntryDto)
        {
            if (_timesheetApprovalService.GetTimesheetApprovalStatus(timeEntryDto.TimeSheetId).Result.ToList().Find(x => x.Status == ApprovalStatus.Approved || x.Status == ApprovalStatus.Requested) != null)
            {
                throw new Exception("Cannot delete time entry that is approved or submitted for approval");
            }
        }

        public async Task ValidateMinimumWorkingDayAndWorkingHourForApproval(Guid timesheetId)
        {

            var timesheetConfig = (await _timeSheetConfigService.GetTimeSheetConfiguration())?.Data as ConfigurationDto;
            var timeEntries = (await _timeEntryService.GetTimeEntries(timesheetId, null, null))?.Data as List<TimeEntryDto>;

            try
            {
                var empId = _timeSheetService.GetTimeSheet(timesheetId)?.Result?.Data?.EmployeeId;
                if (_employeeService.GetEmployeesById(empId) != null && _employeeService.GetEmployeesById(empId).EmployeeOrganization.EmploymentType == "contract")
                {
                    return;
                }
            }
            catch (Exception ex) {}

            try
            {
                if (timeEntries == null || timeEntries.Count == 0)
                {
                    throw new Exception("No Time Entry to request for approval. Please add Time Entry First.");
                }
                
                if (timesheetConfig == null)
                {
                    throw new Exception("Please add timesheet configuration before timesheet approval.");
                }

                if (timesheetConfig.WorkingDays == null)
                {
                    timesheetConfig.WorkingDays = new List<string>();
                }
                timesheetConfig.WorkingDays = timesheetConfig.WorkingDays.Select(wd => wd.ToUpper()).ToList();

                List<string> weekDays = timeEntries.Select(te => te.Date.DayOfWeek.ToString().ToUpper()).ToList();

                foreach (string workingDay in timesheetConfig.WorkingDays)
                {
                    if (!weekDays.Contains(workingDay))
                    {
                        throw new Exception($"Pelase add time entry for {workingDay} before requesting for approval.");
                    }
                }

                if (timesheetConfig.WorkingHours.Min <= 0)
                {
                    return;
                }

                List<DateTime> timeEntryDates = timeEntries.Select(te => te.Date).ToList();

                foreach (var date in timeEntryDates)
                {
                    if (!timesheetConfig.WorkingDays.Contains(date.DayOfWeek.ToString().ToUpper()))
                    {
                        continue;
                    }

                    var totalHour = timeEntries.FindAll(te => te.Date == date).Select(te => te.Hour).Sum();

                    if (totalHour < timesheetConfig.WorkingHours.Min)
                    {
                        throw new Exception("The minimum hours per day should be filled");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
    

