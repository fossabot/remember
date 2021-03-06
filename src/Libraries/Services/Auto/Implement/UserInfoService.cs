// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-12-03 10:35:07
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        private readonly IUserInfoRepository _repository;
        public UserInfoService(IUserInfoRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
