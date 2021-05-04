﻿using Models.EF;
using Models.ViewModels.Category;
using Models.ViewModels.KPI;
using Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICommonService<T>
    {

        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(int Id);
        Task<T> GetById(int Id);
        Task<List<T>> GetAllById(int Id);
        Task<List<T>> GetAll();
        Task<PagedList<T>> GetAllPaging(string keyword,string lang,int page, int pageSize);

        Task<object> GetAllPaging2(string keyword, string lang, int page, int pageSize);
        Task<object> GetAllPaging3(string keyword, string lang, int page, int pageSize);

        Task<bool> AddAsync(CategoryVM entity);

       
    }
}
