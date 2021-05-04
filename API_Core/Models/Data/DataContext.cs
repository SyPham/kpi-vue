﻿using Microsoft.EntityFrameworkCore;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Data
{
    public class DataContext : DbContext
    {
        public DbSet<KPI> KPIs { get; set; }
        public DbSet<KPILevel> KPILevels { get; set; }
        public DbSet<CategoryLevel> CategoryLevels { get; set; }
        public DbSet<KPIKind> KPI_Kinds { get; set; }
        public DbSet<KPIKindLang> KPI_Kind_Langs { get; set; }
        public DbSet<CateKind> Cate_Kinds { get; set; }
        public DbSet<ScorePerfomance> ScorePerfomances { get; set; }
        public DbSet<KPILang> KPILangs { get; set; }
        public DbSet<Models.EF.Data> Datas { get; set; }
        //public virtual ICollection<KPILang> KPILangs { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Level> Levels { get; set; }
        public DbSet<HistoryActionPlan> HistoryActionPlans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLang> CategoryLangs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<SeenComment> SeenComments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<ActionPlan> ActionPlans { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ActionPlanDetail> ActionPlanDetails { get; set; }
        public DbSet<NotificationDetail> NotificationDetails { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<Language> Languages { get; set; }
        public DbSet<ErrorMessage> ErrorMessages { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Uploader> Uploaders { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<CategoryKPILevel> CategoryKPILevels { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<OCCategory> OCCategories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<StateSendMail> StateSendMails { get; set; }
        public DbSet<LateOnUpLoad> LateOnUpLoads { get; set; }

        public DbSet<Setting> Settings { get; set; }
        public DbSet<MenuLang> MenuLangs { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<WorkingPlan> WorkingPlans { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MenuRole>()
              .HasKey(c => new { c.RoleID, c.MenuID }); 

        }
       
    }
}
