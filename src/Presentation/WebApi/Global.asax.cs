﻿using Autofac;
using Autofac.Integration.WebApi;
using AutoMapperConfig;
using Framework.Infrastructure.Concrete;
using Repositories.Core;
using Repositories.Implement;
using Repositories.Interface;
using Services.Implement;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApi.Attributes;
using WebApi.Infrastructure;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configuration.Filters.Add(new WhiteListAttribute());

            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new CustomMessageHandler());

            AutofacRegister();
            AutoMapperRegister();
        }

        #region Autofac
        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // 注意 Framework 所需数据库访问者
            //builder.RegisterType<DBAccessProvider>().As<IDBAccessProvider>();

            // 注册仓储层服务
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<Comment_DislikeRepository>().As<IComment_DislikeRepository>();
            builder.RegisterType<Comment_LikeRepository>().As<IComment_LikeRepository>();
            builder.RegisterType<BookInfoRepository>().As<IBookInfoRepository>();
            builder.RegisterType<BookInfo_CommentRepository>().As<IBookInfo_CommentRepository>();
            builder.RegisterType<BookInfo_DislikeRepository>().As<IBookInfo_DislikeRepository>();
            builder.RegisterType<BookInfo_LikeRepository>().As<IBookInfo_LikeRepository>();
            builder.RegisterType<BookInfo_ParticipantRepository>().As<IBookInfo_ParticipantRepository>();
            builder.RegisterType<FavoriteRepository>().As<IFavoriteRepository>();
            builder.RegisterType<Favorite_BookInfoRepository>().As<IFavorite_BookInfoRepository>();
            builder.RegisterType<Follower_FollowedRepository>().As<IFollower_FollowedRepository>();
            builder.RegisterType<FunctionInfoRepository>().As<IFunctionInfoRepository>();
            builder.RegisterType<User_BookInfoRepository>().As<IUser_BookInfoRepository>();
            builder.RegisterType<User_BookSectionRepository>().As<IUser_BookSectionRepository>();
            builder.RegisterType<LogInfoRepository>().As<ILogInfoRepository>();
            builder.RegisterType<ParticipantInfoRepository>().As<IParticipantInfoRepository>();
            builder.RegisterType<Role_FunctionRepository>().As<IRole_FunctionRepository>();
            builder.RegisterType<Role_MenuRepository>().As<IRole_MenuRepository>();
            builder.RegisterType<Role_UserRepository>().As<IRole_UserRepository>();
            builder.RegisterType<RoleInfoRepository>().As<IRoleInfoRepository>();
            builder.RegisterType<SearchDetailRepository>().As<ISearchDetailRepository>();
            builder.RegisterType<SearchTotalRepository>().As<ISearchTotalRepository>();
            builder.RegisterType<SettingRepository>().As<ISettingRepository>();
            builder.RegisterType<Sys_MenuRepository>().As<ISys_MenuRepository>();
            builder.RegisterType<ThemeTemplateRepository>().As<IThemeTemplateRepository>();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>();
            builder.RegisterType<BookSectionRepository>().As<IBookSectionRepository>();
            builder.RegisterType<BookSection_CommentRepository>().As<IBookSection_CommentRepository>();

            // 注册服务层服务
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<Comment_DislikeService>().As<IComment_DislikeService>();
            builder.RegisterType<Comment_LikeService>().As<IComment_LikeService>();
            builder.RegisterType<BookInfoService>().As<IBookInfoService>();
            builder.RegisterType<BookInfo_CommentService>().As<IBookInfo_CommentService>();
            builder.RegisterType<BookInfo_DislikeService>().As<IBookInfo_DislikeService>();
            builder.RegisterType<BookInfo_LikeService>().As<IBookInfo_LikeService>();
            builder.RegisterType<BookInfo_ParticipantService>().As<IBookInfo_ParticipantService>();
            builder.RegisterType<FavoriteService>().As<IFavoriteService>();
            builder.RegisterType<Favorite_BookInfoService>().As<IFavorite_BookInfoService>();
            builder.RegisterType<Follower_FollowedService>().As<IFollower_FollowedService>();
            builder.RegisterType<FunctionInfoService>().As<IFunctionInfoService>();
            builder.RegisterType<User_BookInfoService>().As<IUser_BookInfoService>();
            builder.RegisterType<User_BookSectionService>().As<IUser_BookSectionService>();
            builder.RegisterType<LogInfoService>().As<ILogInfoService>();
            builder.RegisterType<ParticipantInfoService>().As<IParticipantInfoService>();
            builder.RegisterType<Role_FunctionService>().As<IRole_FunctionService>();
            builder.RegisterType<Role_MenuService>().As<IRole_MenuService>();
            builder.RegisterType<Role_UserService>().As<IRole_UserService>();
            builder.RegisterType<RoleInfoService>().As<IRoleInfoService>();
            builder.RegisterType<SearchDetailService>().As<ISearchDetailService>();
            builder.RegisterType<SearchTotalService>().As<ISearchTotalService>();
            builder.RegisterType<SettingService>().As<ISettingService>();
            builder.RegisterType<Sys_MenuService>().As<ISys_MenuService>();
            builder.RegisterType<ThemeTemplateService>().As<IThemeTemplateService>();
            builder.RegisterType<UserInfoService>().As<IUserInfoService>();
            builder.RegisterType<BookSectionService>().As<IBookSectionService>();
            builder.RegisterType<BookSection_CommentService>().As<IBookSection_CommentService>();

            // TODO: 注册基于接口约束的实体，不知道为什么，改为部分类后就失败了，以前还测试成功
            // 注册基于接口约束的实体
            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
            //    .Where(
            //        assembly =>
            //            assembly.GetTypes().FirstOrDefault(type => type.GetInterfaces().Contains(typeof(IDependency))) !=
            //            null
            //    );
            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency();

            // add the Entity Framework context to make sure only one context per request
            builder.RegisterType<RemDbContext>().InstancePerRequest();
            builder.Register(c => c.Resolve<RemDbContext>()).As<DbContext>().InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // 保存容器
            Core.ContainerManager.SetContainer(container);
        }
        #endregion

        #region AutoMapper
        /// <summary>
        /// AutoMapper的配置初始化
        /// </summary>
        private void AutoMapperRegister()
        {
            new AutoMapperStartupTask().Execute();
        }
        #endregion



    }
}
