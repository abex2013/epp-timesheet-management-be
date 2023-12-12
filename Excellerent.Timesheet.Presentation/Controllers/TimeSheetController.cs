using Excellerent.APIModularization.Controllers;
using Excellerent.APIModularization.Logging;
using Excellerent.SharedModules.DTO;
using Excellerent.Timesheet.Domain.Dtos;
using Excellerent.Timesheet.Domain.Entities;
using Excellerent.Timesheet.Domain.Helpers;
using Excellerent.Timesheet.Domain.Interfaces.Service;
using Excellerent.Timesheet.Domain.Mapping;
using Excellerent.Timesheet.Domain.Models;
using Excellerent.Timesheet.Domain.Utilities;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellerent.Timesheet.Presentation.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TimeSheetController : AuthorizedController
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ITimeEntryService _timeEntryService;
        private readonly ITimesheetApprovalService _timesheetApprovalService;
        private readonly static string _feature = "Timesheet";

        public TimeSheetController(IHttpContextAccessor htttpContextAccessor, IConfiguration configuration, IBusinessLog _businessLog, ITimeSheetService timeSheetService, ITimeEntryService timeEntryService, ITimesheetApprovalService timesheetAprovalService) : base(htttpContextAccessor, configuration, _businessLog, _feature)
        {
            _timeSheetService = timeSheetService;
            _timeEntryService = timeEntryService;
            _timesheetApprovalService = timesheetAprovalService;
        }

        #region Timesheet

        [Authorize("admin")]
        [HttpGet("Timesheets/{id}")]
        public Task<ResponseDTO> GetTimesheet(Guid id)
        {
            return _timeSheetService.GetTimeSheet(id);
        }

        [AllowAnonymous]
        [HttpGet("Timesheets")]
        public Task<ResponseDTO> GetTimesheet(Guid employeeId, DateTime? date)
        {
            return _timeSheetService.GetTimeSheet(employeeId, date);
        }

        #endregion

        #region Time Entry

        [AllowAnonymous]
        [HttpGet("TimeEntries/{id}")]
        public Task<ResponseDTO> GetTimeEntry(Guid id)
        {
            return _timeEntryService.GetTimeEntry(id);
        }

        [AllowAnonymous]
        [HttpGet("TimeEntries")]
        public Task<ResponseDTO> GetTimeEntries(Guid timeSheetId, DateTime? date, Guid? projectId)
        {
            return _timeEntryService.GetTimeEntries(timeSheetId, date, projectId);
        }
        
        [AllowAnonymous]
        [HttpPost("TimeEntries")]
        public Task<ResponseDTO> AddTimeEntry([FromQuery]Guid employeeId, [FromBody]TimeEntryDto timeEntryDto)
        {
            return _timeEntryService.AddTimeEntry(employeeId, timeEntryDto);
        }

        [AllowAnonymous]
        [HttpPut("TimeEntries")]
        public Task<ResponseDTO> UpdateTimeEntry(TimeEntryDto timeEntryDto)
        {
            return _timeEntryService.UpdateTimeEntry(timeEntryDto);
        }
        [AllowAnonymous]
        [HttpPost("TimeEntriesForRange")]
        public Task<ResponseDTO> AddTimeEntry([FromQuery] Guid employeeId, [FromBody] TimeEntryDto[] entries)
        {
            return _timeEntryService.AddTImeEntryForRangeOfDays(employeeId, entries);
        }
        [HttpDelete("DeleteTimeEntry")]
        [AllowAnonymous]
        public async Task<ResponseDTO> DeleteTimeEntry(Guid timeEntryId)
        {
            try
            {
                return await _timeEntryService.RemoveTimeEntryById(timeEntryId);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        #endregion

        #region TimesheetApproval

        [HttpGet("TimesheetAproval")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetApprovalStatus(Guid timesheetGuid)
        {
            try
            {
                var timesheetApprovalEntities = await _timesheetApprovalService.GetTimesheetApprovalStatus(timesheetGuid);

                if (timesheetApprovalEntities == null || timesheetApprovalEntities.Count() == 0)
                {
                    return new ResponseDTO(ResponseStatus.Success, "No Timesheet Approval status for this Timesheet.", null);
                }
                else
                {
                    return new ResponseDTO(ResponseStatus.Success, "List of Timesheet Approval Status for this Timesheet", timesheetApprovalEntities.Select(tsa => tsa.MapToDto()));
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        [HttpPost("TimesheetAproval")]
        [AllowAnonymous]
        public async Task<ResponseDTO> AddApprovalStatus(Guid timesheetGuid)
        {
            try
            {
                return  await _timeEntryService.ApproveTimeSheet(timesheetGuid);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(ResponseStatus.Error, ex.Message, null);
            }
        }

        [HttpGet("TimesheetApprovals")]
        [AllowAnonymous]
        public async Task<PredicatedResponseDTO> GetTimesheetApprovals([FromQuery] PaginationParams paginationParams)
        {
            return await _timesheetApprovalService.GetTimesheetApprovals(paginationParams);
        }

        [HttpGet("GetApprovalProjectDetails")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetApprovalProject()
        {
            return await _timesheetApprovalService.GetAprovalProject();
           
        }
        [HttpGet("GetApprovalClientDetails")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetApprovalClient()
        {
            return await _timesheetApprovalService.GetAprovalClient();

        }

        [HttpPost("TimesheetApprovalBulkApprove")]
        [AllowAnonymous]
        public async Task<ResponseDTO> TimesheetApprovalBulkApprove(List<Guid> guids)
        {
            return await _timesheetApprovalService.TimesheetApprovalBulkApprove(guids);
        }

        [HttpPut("TimesheetProjectStatus")]
        [AllowAnonymous]
        public async Task<ResponseDTO> UpdateProjectApproval(TimesheetApprovalEntity entity)
        {
            return await _timesheetApprovalService.UpdateProjectApprovalStatus(entity);
            var approvedProjects = await _timesheetApprovalService.GetTimesheetApprovalStatus(entity.TimesheetId);
            var entryProjecs = await _timeEntryService.GetTimeEntries(entity.TimesheetId, null, null);
            if (approvedProjects.Count() == entryProjecs.Data.Count() && entity.Status.Equals(ApprovalStatus.Approved))
            {
                var timesheet = (await _timeSheetService.GetTimeSheet(entity.TimesheetId));
                timesheet.Data.Status = ApprovalStatus.Approved;
                _timeSheetService.Update(timesheet.Data);
            }
        }

        //all approved timesheet
        [HttpGet("TimesheetsApprovalPaginated")]
        [AllowAnonymous]
        public async Task<PredicatedResponseDTO> AllApprovalTimesheet([FromQuery] PaginationParams paginationParams)
        {
            
            DateTime toDate = new DateTime();
            DateTime fromDate = new DateTime();
            if (paginationParams.Week != null)
            {
                DateTime localDateTime = (DateTime)paginationParams.Week;
                localDateTime = (DateTime)localDateTime.Date;
                DateTime fromDates = DateTimeUtility.GetWeeksFirstDate(localDateTime);
                fromDate = fromDates;
                DateTime toDates = DateTimeUtility.GetWeeksLastDate(localDateTime);
                toDate = toDates;
            }
            var predicateProject = PredicateBuilder.New<TimesheetApproval>();
            if (paginationParams.ProjectName != null)
            {
               
                foreach (var a in paginationParams.ProjectName)
                {
                    predicateProject = predicateProject.Or(x => x.Project.ProjectName == a);
                }
            }
            var predicateClient = PredicateBuilder.New<TimesheetApproval>();
            if (paginationParams.ClientName != null)
            {
                
                foreach (var a in paginationParams.ClientName)
                {
                    predicateClient = predicateClient.Or(x => x.Project.Client.ClientName == a);
                }
            }

            var predicate = PredicateBuilder.New<TimesheetApproval>();
            if (paginationParams.status != null)
            {
                if (paginationParams.Week != null)
                {


                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))

                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                           
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else if (paginationParams.ProjectName != null)
                    {
                        predicate = predicate.And(predicateProject);

                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status)
                            .And(m => m.Timesheet.FromDate == fromDate);
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(x => x.Timesheet.FromDate == fromDate)
                            .And(n => n.Status == paginationParams.status)
                            : predicate.And(x => x.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);

                    }

                }
                else
                {
                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))

                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                           
                            .And(n => n.Status == paginationParams.status)
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);

                    }
                    else if (paginationParams.ProjectName != null)
                    {
                        predicate = predicate.And(predicateProject);

                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            : predicate
                             .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            .And(n => n.Status == paginationParams.status);
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(n => n.Status == paginationParams.status)
                            : predicate
                            .And(n => n.Status == paginationParams.status)
                             .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(n => n.Status == paginationParams.status)
                             : predicate.And(n => n.Status == paginationParams.status)
                             .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower())); ;

                    }
                }
            }
            else
            {
                if (paginationParams.Week != null)
                {


                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))
                    {

                        predicate = predicate.And(predicateProject);

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                        
                            .And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                          
                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ProjectName != null)
                    {

                        predicate = predicate.And(predicateProject);


                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate.And(m => m.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(m => m.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            .And(x => x.Timesheet.FromDate == fromDate)
                            : predicate
                            .And(x => x.Timesheet.FromDate == fromDate)
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));

                    }

                }
                else
                {
                    if ((paginationParams.ClientName != null) && (paginationParams.ProjectName != null))
                    {
                        
                        predicate = predicate.And(predicateProject);
                        
                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()))
                            ;

                    }
                    else if (paginationParams.ProjectName != null)
                    {

                        predicate = predicate.And(predicateProject);


                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                                  : predicate
                             .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else if (paginationParams.ClientName != null)
                    {

                        predicate = predicate.And(predicateClient);
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? predicate
                            : predicate
                            .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));
                    }
                    else
                    {
                        predicate = string.IsNullOrEmpty(paginationParams.searchKey) ? null
                             : predicate
                             .And(a => a.Timesheet.Employee.FirstName.ToLower().Contains(paginationParams.searchKey.ToLower()));

                    }
                }

            }

            return await _timesheetApprovalService.AllTimesheetAproval(predicate,paginationParams);
        }

        [HttpGet("UserTimesheetApprovalsHistory")]
        [AllowAnonymous]
        public async Task<ResponseDTO> GetUserTimesheetApprovalsHistory([FromQuery] UserTimesheetApprovalParamDto paginationParams)

        {
            return await _timesheetApprovalService.GetUserTimesheetApprovalHistory(paginationParams, paginationParams.EmployeeGuId);

        }
        #endregion
    }
}
