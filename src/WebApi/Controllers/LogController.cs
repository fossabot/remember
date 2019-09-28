﻿using Common;
using Core;
using Domain;
using Framework.Infrastructure.Concrete;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Common;
using WebApi.Models.LogVM;

namespace WebApi.Controllers
{
    public class LogController : ApiController
    {
        #region 写入访问日志
        public ResponseData Post(AccessLogInputModel inputModel)
        {
            ResponseData responseData = null;
            try
            {
                UserAgentModel userAgent = JsonConvert.DeserializeObject<UserAgentModel>(inputModel.UserAgent);
                int accessUserId = 0;
                var user = AccountManager.GetCurrentUserInfo();
                if (user != null)
                {
                    accessUserId = user.ID;
                }

                Container.Instance.Resolve<LogInfoService>().Create(new LogInfo
                {
                    AccessIp = inputModel.Ip,
                    AccessTime = inputModel.AccessTime.ToDateTime13(),
                    JumpTime = inputModel.JumpTime.ToDateTime13(),
                    CreateTime = DateTime.Now,
                    UserAgent = inputModel.UserAgent,
                    AccessUrl = inputModel.AccessUrl,
                    RefererUrl = inputModel.RefererUrl,
                    AccessUserId = accessUserId,
                    Browser = userAgent.Browser.Name + " " + userAgent.Browser.Version,
                    BrowserEngine = userAgent.Engine.Name,
                    Device = userAgent.Device.Model,
                    Cpu = userAgent.Cpu.Architecture,
                    OS = userAgent.OS.Name + " " + userAgent.OS.Version,
                    Duration = inputModel.JumpTime / 1000 - inputModel.AccessTime / 1000
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
