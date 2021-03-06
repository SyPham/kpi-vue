// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProModes
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActionPlanDetails> ActionPlanDetails { get; set; }
        public virtual DbSet<ActionPlans> ActionPlans { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryKpilevels> CategoryKpilevels { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Data> Data { get; set; }
        public virtual DbSet<ErrorMessages> ErrorMessages { get; set; }
        public virtual DbSet<Favourites> Favourites { get; set; }
        public virtual DbSet<Kpilevels> Kpilevels { get; set; }
        public virtual DbSet<Kpis> Kpis { get; set; }
        public virtual DbSet<Levels> Levels { get; set; }
        public virtual DbSet<Managers> Managers { get; set; }
        public virtual DbSet<MenuLangs> MenuLangs { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<NotificationDetails> NotificationDetails { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Occategories> Occategories { get; set; }
        public virtual DbSet<Owners> Owners { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
        public virtual DbSet<Revises> Revises { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SeenComments> SeenComments { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Sponsors> Sponsors { get; set; }
        public virtual DbSet<StateSendMails> StateSendMails { get; set; }
        public virtual DbSet<SubNotifications> SubNotifications { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Uploaders> Uploaders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ActionPlanDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionPlanId).HasColumnName("ActionPlanID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("USerID");
            });

            modelBuilder.Entity<ActionPlans>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActualFinishDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01T00:00:00.000')");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DataId).HasColumnName("DataID");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelCodeAndPeriod).HasColumnName("KPILevelCodeAndPeriod");

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.UpdateSheduleDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900-01-01T00:00:00.000')");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<CategoryKpilevels>(entity =>
            {
                entity.ToTable("CategoryKPILevels");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentedDate).HasColumnType("datetime");

                entity.Property(e => e.DataId).HasColumnName("DataID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Data>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");
            });

            modelBuilder.Entity<ErrorMessages>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Favourites>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Kpilevels>(entity =>
            {
                entity.ToTable("KPILevels");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Kpiid).HasColumnName("KPIID");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.Monthly).HasColumnType("datetime");

                entity.Property(e => e.Pic).HasColumnName("PIC");

                entity.Property(e => e.Quarterly).HasColumnType("datetime");

                entity.Property(e => e.TeamId)
                    .HasColumnName("TeamID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeCheck).HasColumnType("datetime");

                entity.Property(e => e.Yearly).HasColumnType("datetime");
            });

            modelBuilder.Entity<Kpis>(entity =>
            {
                entity.ToTable("KPIs");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Levels>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<MenuLangs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.LangId).HasColumnName("LangID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuLangs)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_dbo.MenuLangs_dbo.Menus_MenuID");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");
            });

            modelBuilder.Entity<NotificationDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.Url).HasColumnName("URL");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionplanId).HasColumnName("ActionplanID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.Kpiname).HasColumnName("KPIName");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Occategories>(entity =>
            {
                entity.ToTable("OCCategories");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Ocid).HasColumnName("OCID");
            });

            modelBuilder.Entity<Owners>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Urldefault).HasColumnName("URLDefault");
            });

            modelBuilder.Entity<Resources>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Revises>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KpilevelCodePeriod).HasColumnName("KPILevelCodePeriod");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<SeenComments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Sponsors>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<StateSendMails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ToDay).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubNotifications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Kpiname).HasColumnName("KPIName");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.Url).HasColumnName("URL");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.SubNotifications)
                    .HasForeignKey(d => d.NotificationId)
                    .HasConstraintName("FK_dbo.SubNotifications_dbo.Notifications_NotificationID");
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionPlanId).HasColumnName("ActionPlanID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Uploaders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.KpilevelCode).HasColumnName("KPILevelCode");

                entity.Property(e => e.KpilevelId).HasColumnName("KPILevelID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LevelId).HasColumnName("LevelID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}