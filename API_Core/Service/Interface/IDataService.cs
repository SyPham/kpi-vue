﻿using Models.Data;
using Models.EF;
using Models.ViewModels.Auth;
using Models.ViewModels.Data;
using Models.ViewModels.Level;
using Models.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Helpers;
using Models.ViewModels.ActionPlan;

namespace Service.Interface
{
   public interface IDataService: IDisposable
    {
        Task<object> GetAllDataByCategory(int categoryid, string period, int? start, int? end, int? year);
        Task<object> GetAllKpiOverview(int categoryid, string period,int? start, int? end, int? year,int userID);
        Task<object> GetAllKpiOverview2(int categoryid, string period, string lang ,int? start, int? end, int? year,int userID);
        Task<object> SortKpiOverview(int categoryid, string period, int? start, int? end, int? year, int userID);
        Task<object> SortActionPlanByPeriod(string period , int point , int kpilevelID, int userID);
        Task<object> SortActionPlanByPeriodTest(string period, int point, int kpilevelID, string kpilevelCode, int userID);
        Task<DataUserViewModel> GetAllOwner(int categoryID, int kpilevelID);
        Task<ChartViewModel> ListDatas(string kpilevelcode, string period, int? year, int? start, int? end, int? catid,int userid);
        Task<ChartViewModel> ListDatas2(string kpilevelcode, string period,string lang, int? year, int? start, int? end, int? catid, int userid);
        Task<object> Remark(int dataid);
        Task<bool> UpdateRemark(int dataid, string remark);
        DataCompareViewModel Compare(string obj);
        ChartViewModel Compare(string kpilevelcode, string period);
        public List<ChartViewModel2> Compare2(string obj);
        ChartViewModel2 Compare2(string kpilevelcode, string period);
        Task<object> ListKPIUpload(int updaterid, int page, int pageSize);
        Task<object> UpLoadKPILevel(int userid, int page, int pageSize);
        string GetTargetData(string KPILevelCode, string CharacterPeriod, int period);
        List<DataExportViewModel> DataExport(int userid);
        string GetValueData(string KPILevelCode, string CharacterPeriod, int period);
        Task<bool> IsUpdater(int id);
        List<ActionDataExportViewModel> DataExportActionPlan(int kpilevelID);
        Task<ImportDataViewModel> ImportData(List<UploadDataViewModel> entity, string userUpdate, int userId);
        Task<object> KPIRelated(int levelId, int page, int pageSize);
        Task<object> UpLoadKPILevelTrack(int userid, int page, int pageSize);
        Task AddLateOnUploadAsync(List<LateOnUpLoad> lateOnUpLoads);

        PagedList<LateOnUpLoad> LateOnUpLoads(int userId, int notificationId,int? page, int? pageSize);
        Task<PagedList<ActionPlanTagViewModel>> ListTasks(string code, int? page, int? pageSize);
        Task<PagedList<NotificationDetail>> GetAllSubNotificationsByIdAsync(int notificationId, int user, int? page, int? pageSize);
    }
 
}
