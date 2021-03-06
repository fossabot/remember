﻿using Core.Common;
using Domain.Entities;
using Framework.Infrastructure.Concrete;
using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Web.Http;
using WebApi.Models.Common;
using WebApi.Models.LogVM;

namespace WebApi.Controllers
{
    public class LogController : BaseApiController
    {
        #region Fields
        private readonly ILogInfoService _logInfoService;
        #endregion

        #region Ctor
        public LogController(ILogInfoService logInfoService)
        {
            this._logInfoService = logInfoService;
        }
        #endregion

        #region 写入访问日志
        public ResponseData Post(AccessLogInputModel inputModel)
        {
            ResponseData responseData = null;
            try
            {
                UserAgentModel userAgent = JsonConvert.DeserializeObject<UserAgentModel>(inputModel.UserAgent);
                int accessUserId = 0;
                try
                {
                    var user = AccountManager.GetCurrentUserInfo();
                    if (user != null)
                    {
                        accessUserId = user.ID;
                    }
                }
                catch (Exception ex)
                {
                }

                this._logInfoService.Create(new LogInfo
                {
                    IdCode = inputModel.IdCode,
                    VisitorInfo = inputModel.VisitorInfo,
                    ClickCount = inputModel.ClickCount,
                    AccessIp = inputModel.Ip,
                    AccessCity = inputModel.City,
                    AccessTime = inputModel.AccessTime.ToDateTime13(),
                    JumpTime = inputModel.JumpTime.ToDateTime13(),
                    CreateTime = DateTime.Now,
                    UserAgent = inputModel?.UserAgent,
                    AccessUrl = inputModel?.AccessUrl,
                    RefererUrl = inputModel?.RefererUrl,
                    AccessUserId = accessUserId,
                    Browser = userAgent?.Browser?.Name + " " + userAgent?.Browser?.Version,
                    BrowserEngine = userAgent?.Engine?.Name,
                    Device = userAgent?.Device?.Model,
                    Cpu = userAgent?.Cpu?.Architecture,
                    OS = userAgent?.OS?.Name + " " + userAgent?.OS?.Version,
                    Duration = (int)(inputModel.JumpTime - inputModel.AccessTime) / 1000
                });

                responseData = new ResponseData
                {
                    Code = 1,
                    Message = "写入访问日志成功"
                };
            }
            catch (Exception ex)
            {
                responseData = new ResponseData
                {
                    Code = -1,
                    Message = "写入访问日志失败"
                };
            }

            return responseData;
        }
        #endregion
    }
}
