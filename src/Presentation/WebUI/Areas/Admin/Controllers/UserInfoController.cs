﻿using Core;
using Domain;
using Domain.Entities;
using Framework.Common;
using Framework.Infrastructure.Concrete;
using Framework.Models;
using Framework.Mvc;
using Jdenticon;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Admin.Models;
using WebUI.Areas.Admin.Models.Common;
using WebUI.Areas.Admin.Models.UserInfoVM;
using WebUI.Extensions;

namespace WebUI.Areas.Admin.Controllers
{
    public class UserInfoController : Controller
    {
        #region Fields
        private readonly IUserInfoService _userInfoService;
        private readonly IRoleInfoService _roleInfoService;
        private readonly IRole_UserService _role_UserService;
        #endregion

        #region Ctor
        public UserInfoController(IUserInfoService userInfoService, IRoleInfoService roleInfoService, IRole_UserService role_UserService)
        {
            this._userInfoService = userInfoService;
            this._roleInfoService = roleInfoService;
            this._role_UserService = role_UserService;
        }
        #endregion

        #region 列表
        public ViewResult Index(int pageIndex = 1, int pageSize = 6)
        {
            Query(pageIndex, pageSize, out IList<UserInfo> list, out int totalCount);

            ListViewModel<UserInfo> viewModel = new ListViewModel<UserInfo>(list, pageIndex: pageIndex, pageSize: pageSize, totalCount: totalCount);
            TempData["RedirectUrl"] = Request.RawUrl;

            return View(viewModel);
        }

        private void Query(int pageIndex, int pageSize, out IList<UserInfo> list, out int totalCount)
        {
            // 输入的查询关键词
            string query = Request["q"]?.Trim() ?? "";
            // 查询类型
            QueryType queryType = new QueryType();
            queryType.Val = Request["type"]?.Trim() ?? "username";
            switch (queryType.Val.ToLower())
            {
                case "username":
                    queryType.Text = "用户名";
                    list = this._userInfoService.Filter<int>(pageIndex, pageSize, out totalCount, m => m.UserName.Contains(query) && !m.IsDeleted, m => m.ID, false).ToList();
                    break;
                case "id":
                    queryType.Text = "ID";
                    int userId = int.Parse(query);
                    list = this._userInfoService.Filter<int>(pageIndex, pageSize, out totalCount, m => m.ID == userId && !m.IsDeleted, m => m.ID, false).ToList();
                    break;
                default:
                    queryType.Text = "用户名";
                    list = this._userInfoService.Filter<int>(pageIndex, pageSize, out totalCount, m => m.UserName.Contains(query) && !m.IsDeleted, m => m.ID, false).ToList();
                    break;
            }
            ViewBag.Query = query;
            ViewBag.QueryType = queryType;
        }
        #endregion

        #region 删除
        public JsonResult Delete(int id)
        {
            try
            {
                var dbModel = this._userInfoService.Find(m => m.ID == id && !m.IsDeleted);
                dbModel.IsDeleted = true;
                dbModel.DeletedAt = DateTime.Now;
                this._userInfoService.Update(dbModel);

                return Json(new { code = 1, message = $"删除 {dbModel.UserName} 成功" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "删除失败" });
            }
        }
        #endregion

        #region 查看
        public ViewResult Details(int id)
        {
            UserInfo viewModel = this._userInfoService.Find(m => m.ID == id && !m.IsDeleted);

            return View(viewModel);
        }
        #endregion

        #region 编辑
        [HttpGet]
        public ViewResult Edit(int id)
        {
            //UserInfo dbModel = Container.Instance.Resolve<UserInfoService>().GetEntity(id);
            UserInfo dbModel = this._userInfoService.Find(m => m.ID == id && !m.IsDeleted);
            UserInfoViewModel viewModel = (UserInfoViewModel)dbModel;

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Edit(UserInfoViewModel inputModel)
        {
            try
            {
                // 数据格式效验
                if (ModelState.IsValid)
                {
                    int currentUserId = AccountManager.GetCurrentAccount().UserId;

                    #region 数据有效效验

                    #region 绑定邮箱
                    // 查找 已经有此用户名的用户
                    string inputUserName = inputModel.InputUserName?.Trim();
                    if (this._userInfoService.Contains(m => m.UserName == inputUserName && m.ID != inputModel.ID && !m.IsDeleted))
                    {
                        return Json(new { code = -3, message = "用户名已存在，请使用其他用户名" });
                    }
                    // 查找 已经绑定此邮箱的 (非本正编辑) 的用户
                    if (!string.IsNullOrEmpty(inputModel.InputEmail))
                    {
                        if (IsExistEmail(inputModel.InputEmail, inputModel.ID))
                        {
                            return Json(new { code = -3, message = "邮箱已经被其他用户绑定，请绑定其他邮箱" });
                        }
                    }
                    #endregion

                    #endregion

                    // 输入模型->数据库模型
                    UserInfo dbModel = this._userInfoService.Find(m => m.ID == inputModel.ID && !m.IsDeleted);
                    if (!string.IsNullOrEmpty(inputModel.InputPassword))
                    {
                        dbModel.Password = EncryptHelper.MD5Encrypt32(inputModel.InputPassword);
                    }
                    dbModel.UserName = inputModel.InputUserName?.Trim();
                    //dbModel.Avatar = inputModel.InputAvatar?.Trim();
                    dbModel.Email = inputModel.InputEmail?.Trim();
                    dbModel.Description = inputModel.InputDescription?.Trim();

                    #region 角色选项
                    if (inputModel.RoleOptions != null)
                    {
                        IList<int> roleIdList = new List<int>();
                        foreach (OptionModel option in inputModel.RoleOptions)
                        {
                            roleIdList.Add(option.ID);
                        }
                        this._role_UserService.UserAssignRoles(inputModel.ID, roleIdList, currentUserId);
                    }
                    else
                    {
                        // 删除此用户的所有角色
                        this._role_UserService.UserAssignRoles(inputModel.ID, new List<int>(), currentUserId);
                    }
                    #endregion
                    //Container.Instance.Resolve<UserInfoService>().Edit(dbModel);
                    this._userInfoService.Update(dbModel);


                    return Json(new { code = 1, message = "保存成功" });
                }
                else
                {
                    string errorMessage = ModelState.GetErrorMessage();
                    return Json(new { code = -1, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = -2, message = "保存失败" });
            }
        }
        #endregion

        #region 添加
        [HttpGet]
        public ViewResult Create()
        {
            UserInfoViewModel viewModel = new UserInfoViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Create(UserInfoViewModel inputModel)
        {
            try
            {
                // 数据格式效验
                if (ModelState.IsValid)
                {
                    int currentUserId = AccountManager.GetCurrentAccount().UserId;

                    #region 数据有效效验
                    if (string.IsNullOrEmpty(inputModel.InputPassword?.Trim()))
                    {
                        return Json(new { code = -2, message = "请填写初始密码" });
                    }
                    // 查找 已经有此用户名的用户
                    string inputUserName = inputModel.InputUserName?.Trim();
                    if (this._userInfoService.Contains(m => m.UserName == inputUserName && !m.IsDeleted))
                    {
                        return Json(new { code = -3, message = "用户名已存在，请使用其他用户名" });
                    }
                    // 查找 已经绑定此邮箱的 (非本正编辑) 的用户
                    if (!string.IsNullOrEmpty(inputModel.InputEmail))
                    {
                        //bool isExist = Container.Instance.Resolve<UserInfoService>().Count(Expression.Eq("Email", inputModel.InputEmail?.Trim())) > 0;
                        string inputEmail = inputModel.InputEmail?.Trim();
                        bool isExist = this._userInfoService.Contains(m => m.Email == inputEmail && !m.IsDeleted);
                        if (isExist)
                        {
                            return Json(new { code = -3, message = "邮箱已经被其他用户绑定，请绑定其它邮箱" });
                        }
                    }
                    #endregion

                    // 输入模型 - > 数据库模型
                    UserInfo dbModel = new UserInfo();
                    dbModel.Password = EncryptHelper.MD5Encrypt32(inputModel.InputPassword);
                    dbModel.UserName = inputModel.InputUserName?.Trim();
                    //dbModel.Avatar = inputModel.InputAvatar?.Trim();
                    dbModel.Email = inputModel.InputEmail?.Trim();
                    dbModel.Description = inputModel.InputDescription?.Trim();
                    dbModel.CreateTime = DateTime.Now;

                    // 自动生成头像
                    Identicon
                    .FromValue(EncryptHelper.MD5Encrypt32(dbModel.UserName), size: 100)
                    .SaveAsPng(Server.MapPath("/upload/images/avatars/" + dbModel.UserName + ".png"));
                    dbModel.Avatar = ":WebUISite:/upload/images/avatars/" + dbModel.UserName + ".png";
                    this._userInfoService.Create(dbModel);
                    // 注意：添加 用户（如果要支持在添加的时候选择角色），则一定要先创建用户，因为 Role_Users 需要用户外键

                    #region 角色选项
                    if (inputModel.RoleOptions != null)
                    {
                        IList<int> roleIdList = new List<int>();
                        foreach (OptionModel option in inputModel.RoleOptions)
                        {
                            roleIdList.Add(option.ID);
                        }
                        this._role_UserService.UserAssignRoles(dbModel.ID, roleIdList, currentUserId);
                    }
                    else
                    {
                        // 删除此用户的所有角色
                        this._role_UserService.UserAssignRoles(dbModel.ID, new List<int>(), currentUserId);
                    }
                    #endregion

                    return Json(new { code = 1, message = "添加成功" });
                }
                else
                {
                    string errorMessage = ModelState.GetErrorMessage();
                    return Json(new { code = -1, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = -2, message = "添加失败" });
            }
        }
        #endregion

        #region 用户画像
        [HttpGet]
        public ViewResult FaceStat(int id)
        {
            //IList<Learner_CourseBox> learner_CourseBoxes = Container.Instance.Resolve<Learner_CourseBoxService>().GetPaged(new List<ICriterion>
            //{
            //    Expression.Eq("Learner.ID", id )
            //}, new List<Order> { new Order("JoinTime", false) }, 0, 10, out int totalCount);
            IList<Learner_CourseBox> learner_CourseBoxes = ContainerManager.Resolve<ILearner_CourseBoxService>().Filter(1, 10, out int totalCount_CourseBox, m => m.LearnerId == id && !m.IsDeleted, m => m.JoinTime, false).ToList();

            //IList<Learner_VideoInfo> learner_VideoInfos = Container.Instance.Resolve<Learner_VideoInfoService>().GetPaged(new List<ICriterion>
            //{
            //    Expression.Eq("Learner.ID", id )
            //}, new List<Order> { new Order("LastPlayTime", false) }, 0, 10, out int totalCount2);
            IList<Learner_VideoInfo> learner_VideoInfos = ContainerManager.Resolve<ILearner_VideoInfoService>().Filter(1, 10, out int totalCount_VideoInfo, m => m.LearnerId == id && !m.IsDeleted, m => m.LastPlayTime, false).ToList();

            IList<Learner_CourseBoxViewModel> learner_CourseBoxViewModels = new List<Learner_CourseBoxViewModel>();
            foreach (var item in learner_CourseBoxes)
            {
                Learner_CourseBoxViewModel viewModelItem = new Learner_CourseBoxViewModel
                {
                    ID = item.ID,
                    JoinTime = item.JoinTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                    CourseBox = new Learner_CourseBoxViewModel.CourseBoxModel
                    {
                        Name = item.CourseBox.Name
                    },
                    LastPlayVideoInfo = new Learner_CourseBoxViewModel.VideoInfoModel
                    {
                        Page = item.LastPlayVideoInfo.Page,
                        Title = item.LastPlayVideoInfo.Title
                    },
                    Score = 0
                };
                var relate_Learner_VideoInfos = learner_VideoInfos.Where(m => m.VideoInfo.CourseBoxId == item.CourseBoxId);
                long totalLen = relate_Learner_VideoInfos.Select(m => m.VideoInfo).Select(m => m.Duration).Sum();
                long playLen = relate_Learner_VideoInfos.Select(m => m.ProgressAt).Sum();

                if (totalLen > 0)
                {
                    viewModelItem.Score = (int)(playLen / totalLen) * 100;
                }

                learner_CourseBoxViewModels.Add(viewModelItem);
            }


            ViewBag.Learner_CourseBoxes = learner_CourseBoxViewModels;
            ViewBag.Learner_VideoInfos = learner_VideoInfos;

            return View();
        }
        #endregion

        #region Helpers

        #region 检查邮箱是否已(被其它用户)绑定
        public bool IsExistEmail(string email, int exceptUserId = 0)
        {
            bool isExist = false;
            if (exceptUserId == 0)
            {
                //criteria = new List<ICriterion>
                //{
                //    Expression.Eq("Email", email)
                //};
                isExist = this._userInfoService.Contains(m => m.Email == email && !m.IsDeleted);
            }
            else
            {
                //criteria = new List<ICriterion>
                //{
                //     Expression.And(
                //        Expression.Eq("Email", email),
                //        Expression.Not(Expression.Eq("ID", exceptUserId))
                //     )
                //};
                isExist = this._userInfoService.Contains(m =>
                    m.Email == email
                    && m.ID != exceptUserId
                    && !m.IsDeleted
                );
            }

            return isExist;
        }
        #endregion

        #endregion

    }

}