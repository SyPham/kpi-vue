﻿using Models.Data;
using Models.EF;
using Models.ViewModels.ActionPlan;
using Models.ViewModels.Comment;
using Models.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IActionPlanService : ICommonService<ActionPlan>, IDisposable
    {
        Task<Tuple<List<string[]>, bool, string>> Approve(int id, int aproveBy, string KPILevelCode, int CategoryID);
        Task<object> LoadActionPlan(string role,bool status, int page, int pageSize,int userid);
        Task<object> GetAll(int DataID, int CommentID, int userid);
        Task<CommentForReturnViewModel> Add(ActionPlanParams obj);
        Task<CommentForReturnViewModel> AddActionPlanFormData(ActionPlanParams obj);
        Task<Tuple<List<string[]>, bool, string>> Done(int id, int userid, string KPILevelCode, int CategoryID);
        Task<bool> UpdateActionPlan(ActionPlanForUpdateParams actionPlan);
        Task<object> UpdateSheduleDate(string name, string value, string pk, int userid);
        Task<Tuple<List<string[]>, bool, string>> UpdateSheduleDate2(string name, string value, string pk, int userid);
        Tuple<List<object[]>, List<UserViewModel>> CheckDeadline();
        Tuple<List<object[]>, List<UserViewModel>> CheckLateOnUpdateData();
        Tuple<List<object[]>, List<UserViewModel>> CheckLateOnUpdateData(int userid);
        Task<object> GetAll(int DataID, int CommentID, int userid, string keyword, int page, int pageSize);
        Task<object> GetAllActionPlan(int kpilevelID , int userID);
        Task<object> GetFileUpload(int actionPlanID);
        Task<bool> UploadFile(List<UploadFile> entity);
    }
}
