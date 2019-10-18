namespace Repositories.Core
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entities;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class RemDbContext : DbContext
    {
        public RemDbContext()
            : base("name=RemDbContext")
        {
            // 让Entity Framework启动不再效验 __MigrationHistory 表
            // 发现每次效验/查询，都要去创建 __MigrationHistory 表，而 此表 的 ContextKey字段varchar(300) 超过限制导致
            // 解决：Specified key was too long; max key length is 767 bytes
            Database.SetInitializer<RemDbContext>(null);

            this.Configuration.AutoDetectChangesEnabled = true;//对多对多，一对多进行curd操作时需要为true

            //this.Configuration.LazyLoadingEnabled = false;

            // 记录 EF 生成的 SQL
            Database.Log = (str) =>
            {
                System.Diagnostics.Debug.WriteLine(str);
            };
        }

        #region Tables

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<CardBox> CardBox { get; set; }
        public virtual DbSet<CardInfo> CardInfo { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Comment_Dislike> Comment_Dislike { get; set; }
        public virtual DbSet<Comment_Like> Comment_Like { get; set; }
        public virtual DbSet<CourseBox> CourseBox { get; set; }
        public virtual DbSet<CourseBox_Comment> CourseBox_Comment { get; set; }
        public virtual DbSet<CourseBox_Dislike> CourseBox_Dislike { get; set; }
        public virtual DbSet<CourseBox_Like> CourseBox_Like { get; set; }
        public virtual DbSet<CourseBox_Participant> CourseBox_Participant { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Favorite_CourseBox> Favorite_CourseBox { get; set; }
        public virtual DbSet<Follower_Followed> Follower_Followed { get; set; }
        public virtual DbSet<FunctionInfo> FunctionInfo { get; set; }
        public virtual DbSet<Learner_CourseBox> Learner_CourseBox { get; set; }
        public virtual DbSet<Learner_VideoInfo> Learner_VideoInfo { get; set; }
        public virtual DbSet<LogInfo> LogInfo { get; set; }
        public virtual DbSet<ParticipantInfo> ParticipantInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<SearchDetail> SearchDetail { get; set; }
        public virtual DbSet<SearchTotal> SearchTotal { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Sys_Menu> Sys_Menu { get; set; }
        public virtual DbSet<ThemeTemplate> ThemeTemplates { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<VideoInfo> VideoInfo { get; set; }
        public virtual DbSet<VideoInfo_Comment> VideoInfo_Comment { get; set; }
        public virtual DbSet<Favorite_CardBox> Favorite_CardBox { get; set; }
        public virtual DbSet<Role_Function> Role_Function { get; set; }
        public virtual DbSet<Role_Menu> Role_Menu { get; set; }
        public virtual DbSet<Role_User> Role_User { get; set; }

        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 表名不会自动转换为复数
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //设置多对多的关系 .Map()配置用于存储关系的外键列和表。
            /*
             Employees  HasMany此实体类型配置一对多关系。对应Orders实体               
            WithMany   将关系配置为 many:many，且在关系的另一端有导航属性。
             * MapLeftKey 配置左外键的列名。左外键指向在 HasMany 调用中指定的导航属性的父实体。
             * MapRightKey 配置右外键的列名。右外键指向在 WithMany 调用中指定的导航属性的父实体。
             */
            // https://www.cnblogs.com/wer-ltm/p/4944745.html
            //this.HasMany(x => x.Orders).
            //    WithMany(x => x.InvolvedEmployees).
            //    Map(m => m.ToTable("EmployeeOrder").
            //        MapLeftKey("EmployeeId").
            //        MapRightKey("OrderId"));


            // 两组一对多，形成多对多，并在第三张关系表中附加字段
            modelBuilder.Entity<RoleInfo>()
                .HasMany(m => m.Role_Users)
                .WithRequired(m => m.RoleInfo)
                .HasForeignKey(m => m.RoleInfoId);
            modelBuilder.Entity<UserInfo>()
                .HasMany(m => m.Role_Users)
                .WithRequired(m => m.UserInfo)
                .HasForeignKey(m => m.UserInfoId);

            modelBuilder.Entity<RoleInfo>()
                .HasMany(m => m.Role_Functions)
                .WithRequired(m => m.RoleInfo)
                .HasForeignKey(m => m.RoleInfoId);
            modelBuilder.Entity<FunctionInfo>()
                .HasMany(m => m.Role_Functions)
                .WithRequired(m => m.FunctionInfo)
                .HasForeignKey(m => m.FunctionInfoId);

            modelBuilder.Entity<RoleInfo>()
                .HasMany(m => m.Role_Menus)
                .WithRequired(m => m.RoleInfo)
                .HasForeignKey(m => m.RoleInfoId);
            modelBuilder.Entity<Sys_Menu>()
                .HasMany(m => m.Role_Menus)
                .WithRequired(m => m.Sys_Menu)
                .HasForeignKey(m => m.Sys_MenuId);


            modelBuilder.Entity<Sys_Menu>()
                .HasMany(m => m.Children)
                .WithOptional(m => m.Parent)
                .HasForeignKey(m => m.ParentId);

            modelBuilder.Entity<Comment>()
                .HasMany(m => m.Children)
                .WithOptional(m => m.Parent)
                .HasForeignKey(m => m.ParentId);



            // 其它普通设置
           

        }
        #endregion

    }
}
