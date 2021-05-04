﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Microsoft.AspNetCore.Http;
using Models.ViewModels.Data;
using Service.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using API.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkplaceController : ControllerBase
    {
        private readonly IActionPlanService _actionPlanService;
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        private readonly ILevelService _levelService;
        private readonly IMailHelper _mailHelper;
        private readonly IHubContext<HenryHub> _hubContext;
        public WorkplaceController(IActionPlanService actionPlanService,
                                   IDataService dataService,
                                   IConfiguration configuration,
                                   ILevelService levelService,
                                   IHubContext<HenryHub> hubContext,
                                   IMailHelper mailHelper)
        {
            _actionPlanService = actionPlanService;
            _dataService = dataService;
            _configuration = configuration;
            _levelService = levelService;
            _hubContext = hubContext;
            _mailHelper = mailHelper;
        }

        // GET: Workplace
        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> ListKPIUpload(int page = ConstantCommon.PAGE,  int pageSize = ConstantCommon.PAGE_SIZE)
        {
            string token = Request.Headers["Authorization"];
            var Id = Extensions.GetDecodeTokenByProperty(token, "nameid").ToInt();

            if (!await _dataService.IsUpdater(Id))
                return Ok(new
                {
                    status = true,
                    isUpdater = false
                });

            return Ok(await _dataService.ListKPIUpload(Id, page, pageSize));
        }
        [HttpGet("{role}")]
        [HttpGet("{role}/{page}/{pageSize}/{status}")]
        public async Task<IActionResult> LoadActionPlan(string role, bool status , int page = ConstantCommon.PAGE, int pageSize = ConstantCommon.PAGE_SIZE)
        {
            string token = Request.Headers["Authorization"];
            var userId = Extensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            return Ok(await _actionPlanService.LoadActionPlan(role, status, page, pageSize, userId));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {

            await _hubContext.Clients.All.SendAsync("ReceiveMessage","user","message");
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Import([FromForm] IFormFile file2)
        {
            var URL = _configuration.GetSection("AppSettings:URL").Value.ToSafetyString();
            var url = URL + "/workplace";
            string token = Request.Headers["Authorization"];
            var userId = Extensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            var aliasUser = Extensions.GetDecodeTokenByProperty(token, ClaimTypeEnum.Alias.ToString()).ToSafetyString();

            IFormFile file = Request.Form.Files["UploadedFile"];
            var datasList = new List<UploadDataViewModel>();
            //var datasList2 = new List<UploadDataVM2>();

            if ((file != null) && (file.Length > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.Length];
                //var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.Length));

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {
                        var value = string.Empty;
                        var target_value = string.Empty;
                        if (workSheet.Cells[rowIterator, 3].Value != null)
                        {
                            value = workSheet.Cells[rowIterator, 3].Value.ToSafetyString();
                        } else
                        {
                            value = "0";
                        }
                        if (workSheet.Cells[rowIterator, 4].Value != null)
                        {
                            target_value = workSheet.Cells[rowIterator, 4].Value.ToSafetyString();
                        }
                        else
                        {
                            target_value = "0";
                        }

                        if (workSheet.Cells[rowIterator, 1].Value != null)
                        {
                            datasList.Add(new UploadDataViewModel()
                            {
                                KPILevelCode = workSheet.Cells[rowIterator, 1].Value.ToSafetyString().ToUpper() ?? null,
                                Value = value,
                                TargetValue = target_value,
                                PeriodValue = workSheet.Cells[rowIterator, 5].Value.ToInt(),
                                Year = workSheet.Cells[rowIterator, 6].Value.ToInt(),
                                CreateTime = DateTime.Now,
                            });
                        }
                    }
                }

                var model = await _dataService.ImportData(datasList, aliasUser, userId);
                //signalr

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "user", "message");

                if (model.ListDatasSuccess.Count > 0 )
                {
                    string content2 = System.IO.File.ReadAllText("./wwwroot/Templates/UploadSuccessfully.html");
                    content2 = content2.Replace("{{{content}}}", "<b style='color:green'>Upload Data Successfully!</b><br/> Dear Updater, <br/> You just uploaded the KPIs as below list: " + "<br/>" +
                        "<b style='color:green'>上傳資料成功!</b><br/> 親愛的資料更新者, <br/> 您已更新以下的KPI資料: ");
                    var html2 = string.Empty;
                    foreach (var item in model.ListDatasSuccess.DistinctBy(x => x.KPIName))
                    {
                        var area = _levelService.GetNode(item.KPILevelCode);
                        html2 += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                             </tr>"
                        .Replace("{{area}}", area)
                        .Replace("{{code}}", item.KPILevelCode)
                        .Replace("{{kpiname}}", item.KPIName);
                    }
                    content2 = content2.Replace("{{{html-template}}}", html2).Replace("{{{href}}}", url);
                    Thread thread = new Thread(async () =>
                    {
                        await _mailHelper.SendEmailRangeAsync(model.ListSendMail, "[KPI System] Upload Data successfully(上傳資料成功)", content2);

                    });
                    thread.Start();
                }
                
                if (model.ListDatasOverTarget != null && model.ListDatasOverTarget.Count > 0)
                {
                    string content = System.IO.File.ReadAllText("./wwwroot/Templates/overTarget.html");
                    content = content.Replace("{{{content}}}", @"<b style='color:red'>Over Target!</b><br/>Dear Owner, <br/>Please add your comment and action plan because you did not achieve KPI target as over list:" + "<br/>" +
                        @"<b style='color:red'>未達到目標!</b><br/>親愛的資料執行者, <br/>請加上您的意見以及行動方案因為以下的KPI項目未達到目標:");
                    var html = string.Empty;

                    foreach (var item in model.ListDatasOverTarget)
                    {
                        var area = _levelService.GetNode(item.KPILevelCode);
                        if (item.Week > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{week}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{week}}", "Week " + item.Week.ToSafetyString());
                        }
                        if (item.Month > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>                            
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{month}}</td>
                             </tr>"
                           .Replace("{{area}}", area)
                           .Replace("{{code}}", item.KPILevelCode)
                           .Replace("{{kpiname}}", item.KPIName)
                           .Replace("{{month}}", "Month " + item.Month.ToSafetyString());
                        }
                        if (item.Half > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{quarter}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{half}}", "HalfYear " + item.Half.ToSafetyString());
                        }
                        if (item.Quarter > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{quarter}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{quarter}}", "Quarter " + item.Quarter.ToSafetyString());
                        }

                        if (item.Year > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{year}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{year}}", "Year " + item.Year.ToSafetyString());
                        }
                    }

                    content = content.Replace("{{{html-template}}}", html).Replace("{{{href}}}", url);
                    Thread thread = new Thread(async () =>
                    {
                        await _mailHelper.SendEmailRangeAsync(model.ListSendMail, "[KPI System] Over Target (未達到目標)", content);
                    });
                    thread.Start();

                    //signalR
                    return Ok(model.Status);
                }

                if (model.ListDatasBelowTarget.Count > 0)
                {
                    string content = System.IO.File.ReadAllText("./wwwroot/Templates/BelowTarget.html");
                    content = content.Replace("{{{content}}}", @"<b style='color:red'>Below Target!</b><br/>Dear Owner, <br/>Please add your comment and action plan because you did not achieve KPI target as below list:" + "<br/>" +
                        @"<b style='color:red'>未達到目標!</b><br/>親愛的資料執行者, <br/>請加上您的意見以及行動方案因為以下的KPI項目未達到目標:");
                    var html = string.Empty;

                    foreach (var item in model.ListDatasBelowTarget)
                    {
                        var area = _levelService.GetNode(item.KPILevelCode);
                        if (item.Week > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{week}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{week}}", "Week " + item.Week.ToSafetyString());
                        }
                        if (item.Month > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>                            
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{month}}</td>
                             </tr>"
                           .Replace("{{area}}", area)
                           .Replace("{{code}}", item.KPILevelCode)
                           .Replace("{{kpiname}}", item.KPIName)
                           .Replace("{{month}}", "Month " + item.Month.ToSafetyString());
                        }
                        if (item.Half > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{quarter}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{half}}", "HalfYear " + item.Half.ToSafetyString());
                        }
                        if (item.Quarter > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{quarter}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{quarter}}", "Quarter " + item.Quarter.ToSafetyString());
                        }

                        if (item.Year > 0)
                        {
                            html += @"<tr>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{area}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{code}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{kpiname}}</td>
                            <td valign='top' style='padding:5px; font-family: Arial,sans-serif; font-size: 16px; line-height:20px;'>{{year}}</td>
                             </tr>"
                            .Replace("{{area}}", area)
                            .Replace("{{code}}", item.KPILevelCode)
                            .Replace("{{kpiname}}", item.KPIName)
                            .Replace("{{year}}", "Year " + item.Year.ToSafetyString());
                        }
                    }

                    content = content.Replace("{{{html-template}}}", html).Replace("{{{href}}}", url);
                    Thread thread = new Thread(async () =>
                    {
                        await _mailHelper.SendEmailRangeAsync(model.ListSendMail, "[KPI System] Below Target (未達到目標)", content);
                    });
                    thread.Start();
                    
                    //signalR
                    return Ok(model.Status);
                }
               
                return Ok(model.Status);
            }
            return Ok(false);
        }
        [HttpGet("{userid}")]
        public ActionResult ExcelExport1(int userid)
        {
            var model = _dataService.DataExport(userid);
            var currentYear = DateTime.Now.Year;
            var currentWeek = DateTime.Now.GetIso8601WeekOfYear();
            var currentMonth = DateTime.Now.Month;
            var currentQuarter = DateTime.Now.GetQuarter();

            var now = DateTime.Now;
            var end = now.GetEndOfQuarter();
            var tt = end.Subtract(now).Days;
            //var targetValue = "";

            DataTable Dt = new DataTable();
            Dt.Columns.Add("KPILevel Code", typeof(string));
            Dt.Columns.Add("KPI Name", typeof(string));
            Dt.Columns.Add("Actual Value", typeof(string));
            Dt.Columns.Add("Target Value", typeof(object));
            Dt.Columns.Add("Period Value", typeof(string));
            Dt.Columns.Add("Year", typeof(int));
            Dt.Columns.Add("OC", typeof(string));
            Dt.Columns.Add("Update Time", typeof(object));
            Dt.Columns.Add("Start Date", typeof(string));
            Dt.Columns.Add("End Date", typeof(string));
            foreach (var item in model)
            {
                var oc = _levelService.GetNode(item.KPILevelCode);
                ///Logic export tuần
                //Nếu tuần MAX trong bảng DATA mà bằng 0 thì export từ tuần đầu cho đến tuần hiện tại
                if (item.PeriodValueW == 0 && item.StateW == true)
                {

                    for (int i = 1; i <= currentWeek; i++)
                    {
                        var startDayOfWeek = CodeUtility.ToGetMondayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                        var endDayOfWeek = CodeUtility.ToGetSaturdayOfWeek(currentYear, i).ToString("MM/dd/yyyy");

                        var updateTimeW = item.UploadTimeW.ConvertNumberDayOfWeekToString() + ", Week " + i;
                        Dt.Rows.Add(item.KPILevelCode + "W", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, updateTimeW, startDayOfWeek, endDayOfWeek);
                    }
                }
                // nếu tuần hiện tại trừ tuần MAX >= 1 thì export từ tuần kế tiếp tuần MAX cho đến tuần hiện tại
                else if (item.PeriodValueW > 0 && item.StateW == true)
                {
                    if (currentWeek - item.PeriodValueW >= 1)
                    {

                        for (int i = item.PeriodValueW + 1; i <= currentWeek; i++)
                        {
                            var startDayOfWeek = CodeUtility.ToGetMondayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                            var endDayOfWeek = CodeUtility.ToGetSaturdayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                            var updateTimeW = item.UploadTimeW.ConvertNumberDayOfWeekToString() + ", Week " + i;
                            Dt.Rows.Add(item.KPILevelCode + "W", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, updateTimeW, startDayOfWeek, endDayOfWeek);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= currentWeek; i++)
                        {
                            var startDayOfWeek = CodeUtility.ToGetMondayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                            var endDayOfWeek = CodeUtility.ToGetSaturdayOfWeek(currentYear, i).ToString("MM/dd/yyyy");

                            var updateTimeW = item.UploadTimeW.ConvertNumberDayOfWeekToString() + ", Week " + i;
                            var value = _dataService.GetValueData(item.KPILevelCode, "W", i);
                            Dt.Rows.Add(item.KPILevelCode + "W", item.KPIName, value, item.TargetValueW, i, currentYear, oc, updateTimeW, startDayOfWeek, endDayOfWeek);
                        }
                    }
                }


                ///Logic export tháng
                //Nếu tháng MAX trong bảng DATA mà bằng 0 thì export từ tháng đầu cho đến tháng hiện tại

                if (item.PeriodValueM == 0 && item.StateM == true)
                {
                    for (int i = 1; i <= currentMonth; i++)
                    {
                        Dt.Rows.Add(item.KPILevelCode + "M", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeM.ToSafetyString().Split(' ')[0].ToSafetyString());
                    }
                }
                // nếu tháng hiện tại trừ tháng MAX >= 1 thì export từ tháng kế tiếp tháng MAX cho đến tháng hiện tại
                if (item.PeriodValueM > 0 && item.StateM == true)
                {
                    if (currentMonth - item.PeriodValueM > 1)
                    {

                        for (int i = item.PeriodValueM + 1; i <= currentMonth; i++)
                        {

                            Dt.Rows.Add(item.KPILevelCode + "M", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeM.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= currentMonth; i++)
                        {
                            var value = _dataService.GetValueData(item.KPILevelCode, "M", i);
                            Dt.Rows.Add(item.KPILevelCode + "M", item.KPIName, value, item.TargetValueW, i, currentYear, oc, item.UploadTimeM.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }
                    }

                }
                ///Logic export quý
                //Nếu quý MAX trong bảng DATA mà bằng 0 thì export từ tháng đầu cho đến quý hiện tại
                if (item.PeriodValueQ == 0 && item.StateQ == true)
                {
                    for (int i = 1; i < currentQuarter; i++)
                    {
                        Dt.Rows.Add(item.KPILevelCode + "Q", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeQ.ToSafetyString().Split(' ')[0].ToSafetyString());
                    }
                    if (tt <= 30)
                    {
                        Dt.Rows.Add(item.KPILevelCode + "Q", item.KPIName, item.Value, item.TargetValueW, currentQuarter, currentYear, oc, item.UploadTimeQ.ToSafetyString().Split(' ')[0].ToSafetyString());
                    }
                }
                if (item.PeriodValueQ > 0 && item.StateQ == true)
                {

                    //, Nếu quý hiện tại trừ quý MAX trong bảng DATA lớn hơn 1 thì
                    //export từ quý 1 cho đến quý hiện tại
                    if (currentQuarter - item.PeriodValueQ >= 1)
                    {

                        for (int i = item.PeriodValueQ; i <= currentQuarter; i++)
                        {
                            Dt.Rows.Add(item.KPILevelCode + "Q", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeQ.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }

                    }
                    else
                    {
                        for (int i = 1; i <= currentQuarter; i++)
                        {
                            var value = _dataService.GetValueData(item.KPILevelCode, "Q", i);
                            Dt.Rows.Add(item.KPILevelCode + "Q", item.KPIName, value, item.TargetValueW, i, currentYear, oc, item.UploadTimeQ.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }
                    }
                }

                ///Logic export năm
                //Nếu năm MAX trong bảng DATA == 0 thì export năm hiện tại
                if (item.PeriodValueY == 0 && item.StateY == true)
                {
                    for (int i = currentYear - 10; i <= currentYear; i++)
                    {
                        Dt.Rows.Add(item.KPILevelCode + "Y", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeY.ToSafetyString().Split(' ')[0].ToSafetyString());
                    }

                }
                if (item.PeriodValueY > 0 && item.StateY == true)
                {
                    // nếu năm hiện tại - năm max trong bảng DATA > 1 thì export năm kế tiếp đến năm hiện tại
                    if (currentYear - item.PeriodValueY >= 1)
                    {
                        for (int i = item.PeriodValueY + 1; i <= currentYear; i++)
                        {
                            Dt.Rows.Add(item.KPILevelCode + "Y", item.KPIName, item.Value, item.TargetValueW, i, currentYear, oc, item.UploadTimeY.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }
                    }
                    else
                    {
                        for (int i = currentYear - 10; i <= currentYear; i++)
                        {
                            var value = _dataService.GetValueData(item.KPILevelCode, "Y", i);
                            Dt.Rows.Add(item.KPILevelCode + "Y", item.KPIName, value, item.TargetValueW, i, currentYear, oc, item.UploadTimeY.ToSafetyString().Split(' ')[0].ToSafetyString());
                        }
                    }

                }
            }
            var memoryStream = new MemoryStream();
            using var excelPackage = new ExcelPackage(memoryStream);
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.None);
            worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
            worksheet.DefaultRowHeight = 18;

            worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            worksheet.DefaultColWidth = 20;
            worksheet.Column(2).AutoFit();
            return File(excelPackage.GetAsByteArray(), "application/octet-stream", "DataUpload.xlsx");

        }

        [AllowAnonymous]
        [HttpGet("{userid}")]
        public ActionResult ExcelExport(int userid)
        {
            //string token = Request.Headers["Authorization"].ToSafetyString();
            //var userid = Extensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            var model = _dataService.DataExport(userid);
            var currentYear = DateTime.Now.Year;
            var currentWeek = DateTime.Now.GetIso8601WeekOfYear();
            var currentMonth = DateTime.Now.Month;
            var currentHalf = DateTime.Now.GetQuarter();
            var currentQuarter = DateTime.Now.GetQuarter();

            //var now = DateTime.Now;
            //var end = now.GetEndOfQuarter();
            //var tt = end.Subtract(now).Days;
            //var targetValue = "";

            DataTable Dt = new DataTable();
            Dt.Columns.Add("KPILevel Code", typeof(string));
            Dt.Columns.Add("KPI Name", typeof(string));
            Dt.Columns.Add("Actual Value", typeof(string));
            Dt.Columns.Add("Target Value", typeof(object));
            Dt.Columns.Add("Period Value", typeof(string));
            Dt.Columns.Add("Year", typeof(int));
            Dt.Columns.Add("OC", typeof(string));
            Dt.Columns.Add("Update Time", typeof(object));
            Dt.Columns.Add("Start Date", typeof(string));
            Dt.Columns.Add("End Date", typeof(string));
            foreach (var item in model)
            {
                var oc = _levelService.GetNode(item.KPILevelCode);

                // Logic export tuần
                if (item.StateW == true)
                {
                    for (int i = 1; i <= currentWeek; i++)
                    {
                        var startDayOfWeek = CodeUtility.ToGetMondayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                        var endDayOfWeek = CodeUtility.ToGetSaturdayOfWeek(currentYear, i).ToString("MM/dd/yyyy");
                        var updateTimeW = item.UploadTimeW.ConvertNumberDayOfWeekToString() + ", Week " + i;
                        var target = _dataService.GetTargetData(item.KPILevelCode, "W", i);
                        var value = _dataService.GetValueData(item.KPILevelCode, "W", i);
                        Dt.Rows.Add(item.KPILevelCode + "W", item.KPIName, value, target, i, currentYear, oc, updateTimeW, startDayOfWeek, endDayOfWeek);
                    }
                }

                ///Logic export tháng

                if (item.StateM == true)
                {
                    var updateTimeM = item.UploadTimeM.ToStringDateTime("MM/dd/yyyy");
                    for (int i = 1; i <= currentMonth; i++)
                    {
                        var startDayOfMonth = CodeUtility.ToGetStartDateOfMonth(currentYear, i).ToString("MM/dd/yyyy");
                        var endDayOfMonth = CodeUtility.ToGetEndDateOfMonth(currentYear, i).ToString("MM/dd/yyyy");
                        var value = _dataService.GetValueData(item.KPILevelCode, "M", i);
                        var target = _dataService.GetTargetData(item.KPILevelCode, "M", i);

                        Dt.Rows.Add(item.KPILevelCode + "M", item.KPIName, value, target, i, currentYear, oc, updateTimeM, startDayOfMonth, endDayOfMonth);
                    }
                }

                ///Logic export nửa năm

                if (item.StateH == true)
                {
                    var updateTimeH = item.UploadTimeH.ToStringDateTime("MM/dd/yyyy");
                    for (int i = 1; i <= currentHalf; i++)
                    {
                        var seq = CodeUtility.ToGetStartAndEndDateOfHalfYear(currentYear, i);
                        var value = _dataService.GetValueData(item.KPILevelCode, "H", i);
                        var target = _dataService.GetTargetData(item.KPILevelCode, "H", i);
                        Dt.Rows.Add(item.KPILevelCode + "H", item.KPIName, value, target, i, currentYear, oc, updateTimeH, seq.Item1.ToString("MM/dd/yyyy"), seq.Item2.ToString("MM/dd/yyyy"));
                    }
                }

                ///Logic export quý
                if (item.StateQ == true)
                {
                    var updateTimeQ = item.UploadTimeQ.ToStringDateTime("MM/dd/yyyy");
                    for (int i = 1; i <= currentQuarter; i++)
                    {
                        var seq = CodeUtility.ToGetStartAndEndDateOfQuarter(currentYear, i);
                        var value = _dataService.GetValueData(item.KPILevelCode, "Q", i);
                        var target = _dataService.GetTargetData(item.KPILevelCode, "Q", i);
                        Dt.Rows.Add(item.KPILevelCode + "Q", item.KPIName, value, target, i, currentYear, oc, updateTimeQ, seq.Item1.ToString("MM/dd/yyyy"), seq.Item2.ToString("MM/dd/yyyy"));
                    }
                }

                ///Logic export năm
                if (item.StateY == true)
                {
                    var updateTimeY = item.UploadTimeY.ToStringDateTime("MM/dd/yyyy");
                    var sey = CodeUtility.ToGetStartAndEndDateOfYear(currentYear);
                    for (int i = currentYear - 9; i <= currentYear; i++)
                    {
                        var value = _dataService.GetValueData(item.KPILevelCode, "Y", currentYear);
                        var target = _dataService.GetTargetData(item.KPILevelCode, "Y", i);

                        Dt.Rows.Add(item.KPILevelCode + "Y", item.KPIName, value, target, i, currentYear, oc, updateTimeY, sey.Item1.ToString("MM/dd/yyyy"), sey.Item2.ToString("MM/dd/yyyy"));

                    }
                }
            }

            var memoryStream = new MemoryStream();
            using var excelPackage = new ExcelPackage(memoryStream);
            var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            worksheet.Cells["A1"].LoadFromDataTable(Dt, true, TableStyles.None);
            worksheet.Cells["A1:AN1"].Style.Font.Bold = true;
            worksheet.DefaultRowHeight = 18;

            worksheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            worksheet.Column(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Column(7).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.DefaultColWidth = 20;
            worksheet.Column(2).AutoFit();
            if (excelPackage == null)
            {
                return NotFound();
            }

            byte[] data = excelPackage.GetAsByteArray() as byte[];

            return File(data, "application/octet-stream", "DataUpload.xlsx");

        }

        [HttpGet("{userid}")]
        [HttpGet("{userid}/{page}/{pageSize}")]
        public async Task<IActionResult> UpLoadKPILevel(int userid, int page = ConstantCommon.PAGE, int pageSize = ConstantCommon.PAGE_SIZE)
        {
            return Ok(await _dataService.UpLoadKPILevel(userid, page, pageSize));
        }
        [HttpGet("{userid}")]
        [HttpGet("{userid}/{page}/{pageSize}")]
        public async Task<IActionResult> UpLoadKPILevelTrack(int userid, int page = ConstantCommon.PAGE, int pageSize = ConstantCommon.PAGE_SIZE)
        {
            return Ok(await _dataService.UpLoadKPILevelTrack(userid, page, pageSize));
        }
        [HttpGet("{levelid}/{page}/{pageSize}")]
        public async Task<IActionResult> KPIRelated(int levelid, int page = ConstantCommon.PAGE, int pageSize = ConstantCommon.PAGE_SIZE)
        {
            return Ok(await _dataService.KPIRelated(levelid, page, pageSize));
        }
        [HttpGet]
        public async Task<IActionResult> GetListTreeForWorkplace()
        {
            string token = Request.Headers["Authorization"];
            var userId = Extensions.GetDecodeTokenByProperty(token, "nameid").ToInt();
            if (userId > 0)
                return Ok(await _levelService.GetListTreeForWorkplace(userId));
            return BadRequest();
        }

    }
}