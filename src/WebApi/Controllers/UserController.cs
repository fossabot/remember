﻿using Common;
using Core;
using Domain;
using Framework.Common;
using Framework.Config;
using Framework.Infrastructure.Concrete;
using Framework.Models;
using NHibernate.Criterion;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Attributes;
using WebApi.DomainExt;
using WebApi.Infrastructure;
using WebApi.Models;
using WebApi.Models.Common;
using WebApi.Models.UserInfoVM;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        #region Get: 获取当前登录账号信息
        [NeedAuth]
        public ResponseData Get()
        {
            ResponseData responseData = null;
            UserInfoViewModel viewModel = null;

            UserInfo userInfo = AccountManager.GetCurrentUserInfo();
            if (userInfo != null)
            {
                SettingService settingService = Container.Instance.Resolve<SettingService>();
                string webApiSite = settingService.Query(new List<ICriterion>
                {
                    Expression.Eq("SetKey", "WebUISite")
                }).FirstOrDefault()?.SetValue;
                string avatarUrl = userInfo.Avatar.Replace(":WebUISite:", webApiSite);

                int followNum = Container.Instance.Resolve<Follower_FollowedService>().Count(Expression.Eq("Follower.ID", userInfo.ID));
                int fansNum = Container.Instance.Resolve<Follower_FollowedService>().Count(Expression.Eq("Followed.ID", userInfo.ID));
                int articleNum = Container.Instance.Resolve<ArticleService>().Count(Expression.Eq("Author.ID", userInfo.ID));

                viewModel = new UserInfoViewModel()
                {
                    ID = userInfo.ID,
                    UserName = userInfo.UserName,
                    Desc = userInfo.Description,
                    FansNum = fansNum,
                    FollowNum = followNum,
                    Avatar = avatarUrl,
                    ArticleNum = articleNum
                };
            }
            responseData = new ResponseData
            {
                Code = 1,
                Message = "成功获取当前登录账号信息",
                Data = viewModel
            };

            return responseData;
        }
        #endregion

        #region 更新自己的密码(只能更改当前登录账号的密码)
        [HttpPost]
        [NeedAuth]
        [Route("UpdatePwd")]
        public ResponseData UpdatePwd([FromBody]string oldPassword, [FromBody]string newPassword)
        {
            ResponseData responseData = null;

            UserInfoService userInfoService = Container.Instance.Resolve<UserInfoService>();
            // 当前登录用户
            UserInfo userInfo = userInfoService.Query(new List<ICriterion>
            {
                Expression.Eq("UserName", User.Identity.Name)
            }).FirstOrDefault();
            if (EncryptHelper.MD5Encrypt32(oldPassword) == userInfo.Password)
            {
                userInfo.Password = EncryptHelper.MD5Encrypt32(newPassword);
                userInfoService.Edit(userInfo);

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "密码更新成功"
                };
            }
            else
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "旧密码不正确"
                };
            }

            return responseData;
        }
        #endregion

        #region 注册
        [HttpPost]
        [Route("Register")]
        public ResponseData Register([FromBody]RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool isExist = Container.Instance.Resolve<UserInfoService>().Exist(viewModel.UserName?.Trim());
                if (!isExist)
                {
                    Container.Instance.Resolve<UserInfoService>().Create(new UserInfo()
                    {
                        UserName = viewModel.UserName,
                        Password = EncryptHelper.MD5Encrypt32(viewModel.Password),
                        Name = viewModel.Name.Trim(),
                        Email = viewModel.Email?.Trim()
                    });

                    return new ResponseData()
                    {
                        Code = 1,
                        Message = "注册成功"
                    };
                }
                else
                {
                    return new ResponseData()
                    {
                        Code = -1,
                        Message = "该用户名已被注册"
                    };
                }
            }

            return new ResponseData()
            {
                Code = -2,
                Message = "输入项有误"
            };
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns>返回一个 JWToken 和一个 JWToken 的过期时间（Unix时间戳）</returns>
        [HttpPost]
        [Route("Login")]
        public ResponseData Login([FromBody]LoginViewModel viewModel)
        {
            string loginAccount = viewModel.LoginAccount.Trim();
            viewModel.Password = EncryptHelper.MD5Encrypt32(viewModel.Password);
            if (ModelState.IsValid)
            {
                UserInfo userInfo = null;
                if (IsEmail(loginAccount))
                {
                    // 邮箱
                    userInfo = Container.Instance.Resolve<UserInfoService>().Query(new List<ICriterion>
                    {
                        Expression.And(
                            Expression.Eq("Email", loginAccount),
                            Expression.Eq("Password", viewModel.Password)
                        )
                    }).FirstOrDefault();
                }
                else if (IsPhone(loginAccount))
                {
                    // 手机号
                    userInfo = Container.Instance.Resolve<UserInfoService>().Query(new List<ICriterion>
                    {
                        Expression.And(
                            Expression.Eq("Phone", loginAccount),
                            Expression.Eq("Password", viewModel.Password)
                        )
                    }).FirstOrDefault();
                }
                else
                {
                    // 用户名
                    userInfo = Container.Instance.Resolve<UserInfoService>().Query(new List<ICriterion>
                    {
                        Expression.And(
                            Expression.Eq("UserName", loginAccount),
                            Expression.Eq("Password", viewModel.Password)
                        )
                    }).FirstOrDefault();
                }
                // 账号密码是否正确
                if (userInfo != null)
                {
                    long expire = DateTime.Now.AddDays(7).ToTimeStamp10();

                    return new ResponseData
                    {
                        Code = 1,
                        Message = "登录成功",
                        Data = new LoginResultViewModel
                        {
                            Token = JwtHelper.Encode(new JWTokenViewModel
                            {
                                ID = userInfo.ID,
                                UserName = userInfo.UserName,
                                Expire = expire,
                                Create = DateTime.Now.ToTimeStamp10()
                            }),
                            Expire = expire
                        }
                    };
                }
                else
                {
                    return new ResponseData
                    {
                        Code = -2,
                        Message = "账号或密码错误"
                    };
                }
            }
            else
            {
                return new ResponseData
                {
                    Code = -1,
                    Message = "输入项有误"
                };
            }
        }
        #endregion

        #region 上传头像-更改自己的头像
        [NeedAuth]
        [HttpPost]
        [Route("UploadAvatar")]
        public ResponseData UploadAvatar()
        {
            ResponseData responseData = null;
            try
            {
                string basePath = "~/Upload/avatars/" + User.Identity.Name + "/";

                // 如果路径含有~，即需要服务器映射为绝对路径，则进行映射
                basePath = (basePath.IndexOf("~") > -1) ? System.Web.HttpContext.Current.Server.MapPath(basePath) : basePath;
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                // 如果目录不存在，则创建目录
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                string fileName = System.Web.HttpContext.Current.Request["name"];
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = file.FileName;
                }
                // 文件保存
                //string saveFileName = Guid.NewGuid().ToString() + "." + file.FileName.Split('.')[1];
                string saveFileName = "avatar" + "." + file.FileName.Split('.')[1];
                string fullPath = basePath + saveFileName;
                file.SaveAs(fullPath);

                // 获取 WebApi 网址 eg: http://localhost:7784/
                SettingService settingService = Container.Instance.Resolve<SettingService>();
                string webSite = settingService.Query(new List<ICriterion>
                {
                    Expression.Eq("SetKey", "WebApiSite")
                }).FirstOrDefault().SetValue;
                if (!string.IsNullOrEmpty(webSite))
                {
                    // 去除末尾 '/'
                    // http://localhost:7784
                    webSite = webSite[webSite.Length - 1] == '/' ? webSite.Substring(0, webSite.Length - 1) : webSite;
                }
                else
                {
                    webSite = "";
                }

                // 更改数据库中用户头像
                UserInfo userInfo = AccountManager.GetCurrentUserInfo();
                userInfo.Avatar = ":WebApiSite:" + "/Upload/avatars/" + User.Identity.Name + "/" + saveFileName;
                Container.Instance.Resolve<UserInfoService>().Edit(userInfo);

                // 全部更新成功后，删除以往头像文件
                DirectoryInfo avatarDic = new DirectoryInfo(basePath);
                var needDeleteFiles = avatarDic.GetFiles().Where(m => m.Name != saveFileName).ToList();
                foreach (var item in needDeleteFiles)
                {
                    item.Delete();
                }

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "上传头像成功",
                    Data = new
                    {
                        Url = webSite + "/Upload/avatars/" + User.Identity.Name + "/" + saveFileName
                    }
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "上传头像失败"
                };
            }

            return responseData;
        }
        #endregion

        #region Put:更新个人基本信息-只能更新当前登录用户
        /// <summary>
        /// 只允许更新 Name, Description
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NeedAuth]
        public ResponseData Put([FromBody]UserInfoViewModel model)
        {
            ResponseData responseData = null;
            try
            {
                UserInfo userInfo = AccountManager.GetCurrentUserInfo();
                if (!string.IsNullOrEmpty(model.Name))
                {
                    userInfo.Name = model.Name;
                }
                if (!string.IsNullOrEmpty(model.Desc))
                {
                    userInfo.Description = model.Desc;
                }
                Container.Instance.Resolve<UserInfoService>().Edit(userInfo);

                SettingService settingService = Container.Instance.Resolve<SettingService>();
                string webApiSite = settingService.Query(new List<ICriterion>
                {
                    Expression.Eq("SetKey", "WebApiSite")
                }).FirstOrDefault()?.SetValue;
                string avatarUrl = userInfo.Avatar.Replace(":WebApiSite:", webApiSite);

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "更新个人基本信息成功",
                    Data = new UserInfoViewModel
                    {
                        ID = userInfo.ID,
                        UserName = userInfo.UserName,
                        Name = userInfo.Name,
                        Desc = userInfo.Description,
                        Avatar = avatarUrl
                    }
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "更新个人基本信息失败"
                };
            }

            return responseData;
        }
        #endregion

        #region 我的关注
        [NeedAuth]
        [Route("MyFollow")]
        [HttpGet]
        public ResponseData MyFollow()
        {
            ResponseData responseData = null;
            try
            {
                MyFollowViewModel viewModel = new MyFollowViewModel();

                // 我关注的所有人
                IList<Follower_Followed> iFollow = Container.Instance.Resolve<Follower_FollowedService>().Query(new List<ICriterion> {
                    Expression.Eq("Follower.ID", ((UserIdentity)User.Identity).ID)
               }).ToList().OrderByDescending(m => m.CreateTime.ToTimeStamp13()).ToList();
                // 关注我的所有人
                IList<Follower_Followed> iFollowed = Container.Instance.Resolve<Follower_FollowedService>().Query(new List<ICriterion> {
                    Expression.Eq("Followed.ID", ((UserIdentity)User.Identity).ID)
               }).ToList().OrderByDescending(m => m.CreateTime.ToTimeStamp13()).ToList();

                // 互粉 = 关注我的所有人  中 挑选出 我关注的人
                // 我和这些人互粉 （UserInfo.ID）
                IList<int> iAndHeFollow = iFollowed.Select(m => m.Follower.ID).Where((id) =>
                {
                    bool exist = iFollow.Select(m => m.Followed.ID).Contains(id);

                    return exist;
                }).ToList();



                viewModel.Groups = new List<MyFollowViewModel.MyFollowGroup>();
                MyFollowViewModel.MyFollowGroup group = new MyFollowViewModel.MyFollowGroup();
                group.ID = 1;
                group.GroupName = "默认分组";
                group.Users = new List<MyFollowViewModel.MyFollowInfo>();
                foreach (var item in iFollow)
                {
                    group.Users.Add(new MyFollowViewModel.MyFollowInfo
                    {
                        // 若我关注的人中，被关注者 也关注了我，则为互粉，否则 我 单方面关注他
                        Relation = iAndHeFollow.Contains(item.Followed.ID) ? 3 : 1,
                        CreateTime = item.CreateTime.ToTimeStamp13(),
                        User = new MyFollowViewModel.User
                        {
                            ID = item.Followed.ID,
                            Avatar = item.Followed.Avatar.ToHttpAbsoluteUrl(),
                            Desc = item.Followed.Description,
                            UserName = item.Followed.UserName,
                        }
                    });
                }
                viewModel.Groups.Add(group);

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "获取我的关注成功",
                    Data = viewModel
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "获取我的关注失败"
                };
            }

            return responseData;
        }
        #endregion

        #region 我的粉丝
        [NeedAuth]
        [Route("MyFans")]
        [HttpGet]
        public ResponseData MyFans()
        {
            ResponseData responseData = null;
            try
            {
                MyFansViewModel viewModel = new MyFansViewModel();

                // 我关注的所有人
                IList<Follower_Followed> iFollow = Container.Instance.Resolve<Follower_FollowedService>().Query(new List<ICriterion> {
                    Expression.Eq("Follower.ID", ((UserIdentity)User.Identity).ID)
               }).ToList().OrderByDescending(m => m.CreateTime.ToTimeStamp13()).ToList();
                // 关注我的所有人
                IList<Follower_Followed> iFollowed = Container.Instance.Resolve<Follower_FollowedService>().Query(new List<ICriterion> {
                    Expression.Eq("Followed.ID", ((UserIdentity)User.Identity).ID)
               }).ToList().OrderByDescending(m => m.CreateTime.ToTimeStamp13()).ToList();

                // 互粉 = 关注我的所有人  中 挑选出 我关注的人
                // 我和这些人互粉 （UserInfo.ID）
                IList<int> iAndHeFollow = iFollowed.Select(m => m.Follower.ID).Where((id) =>
                {
                    bool exist = iFollow.Select(m => m.Followed.ID).Contains(id);

                    return exist;
                }).ToList();

                viewModel.Groups = new List<MyFansViewModel.MyFansGroup>();
                MyFansViewModel.MyFansGroup group = new MyFansViewModel.MyFansGroup();
                group.ID = 1;
                group.GroupName = "默认分组";
                group.Users = new List<MyFansViewModel.MyFansInfo>();
                foreach (var item in iFollowed)
                {
                    group.Users.Add(new MyFansViewModel.MyFansInfo
                    {
                        Relation = iAndHeFollow.Contains(item.Follower.ID) ? 3 : 2,
                        CreateTime = item.CreateTime.ToTimeStamp13(),
                        User = new MyFansViewModel.User
                        {
                            ID = item.Follower.ID,
                            Avatar = item.Follower.Avatar.ToHttpAbsoluteUrl(),
                            Desc = item.Follower.Description,
                            UserName = item.Follower.UserName
                        }
                    });
                }
                viewModel.Groups.Add(group);

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "获取我的粉丝成功",
                    Data = viewModel
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "获取我的粉丝失败"
                };
            }

            return responseData;
        }
        #endregion

        #region Helper

        private bool IsEmail(string str)
        {
            bool isEmail = false;
            isEmail = System.Text.RegularExpressions.Regex.IsMatch(str, @"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");

            return isEmail;
        }

        private bool IsPhone(string str)
        {
            bool isPhone = false;
            isPhone = System.Text.RegularExpressions.Regex.IsMatch(str, @"^[1]+[3,5]+\d{9}");

            return isPhone;
        }

        #endregion

    }
}
