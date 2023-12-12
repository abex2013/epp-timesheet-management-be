using Excellerent.SharedModules.DTO;
using Excellerent.SharedModules.Services;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Interfaces.Repository;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Domain.Services
{
    public class TimeSheetService : CRUD<TimeSheetEntity, TimeSheet>, ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;

        public TimeSheetService(ITimeSheetRepository timeSheetRepository) : base(timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public async Task<ResponseDTO> GetTimeSheet(Guid id)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetTimeSheet((ts => ts.Guid == id));

                return new ResponseDTO(ResponseStatus.Success, "Time Shee by Id", timeSheet?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }
        public async Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime? dateTime)
        {
            DateTime localDateTime = dateTime ?? DateTime.Now;
            localDateTime = (DateTime)localDateTime.Date;

            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);

            return await GetTimeSheet(employeeId, fromDate, toDate);
        }
        public async Task<ResponseDTO> GetTimeSheet(Guid employeeId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetTimeSheet((
                        ts => ts.EmployeeId == employeeId && ts.FromDate == fromDate && ts.ToDate == toDate
                    ));

                return new ResponseDTO(ResponseStatus.Success, "Time Sheet by employee Id, from Date, and to Date", timeSheet?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<ResponseDTO> AddTimeSheet(Guid employeeId, TimeEntryDto timeEntryDto)
        {
            TimeSheetDto timeSheetDto = new TimeSheetDto();
            DateTime localDateTime = (DateTime)timeEntryDto.Date.Date;
            DateTime fromDate = DateTimeUtility.GetWeeksFirstDate(localDateTime);
            DateTime toDate = DateTimeUtility.GetWeeksLastDate(localDateTime);

            timeSheetDto.Guid = Guid.NewGuid();
            timeSheetDto.FromDate = fromDate;
            timeSheetDto.ToDate = toDate;
            timeSheetDto.TotalHours = 0;
            timeSheetDto.Status = 0;
            timeSheetDto.EmployeeId = employeeId;

            return await AddTimeSheet(timeSheetDto, timeEntryDto);
        }
        public async Task<ResponseDTO> AddTimeSheet(TimeSheetDto timeSheetDto, TimeEntryDto timeEntryDto)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.AddTimeSheet(timeSheetDto.MapToModel(timeEntryDto));

                return new ResponseDTO(ResponseStatus.Success, "Timesheet Added Successfully", timeSheet?.MapToDto());
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        public async Task<TimeSheet> GetTimeSheetById(Guid id)
        {
            try
            {
                var timeSheet = await _timeSheetRepository.GetTimeSheet((
                        ts => ts.Guid == id
                    ));

                return timeSheet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> Update(TimeSheet t)
        {
            return await _timeSheetRepository.Update(t);
        }
    }
}
