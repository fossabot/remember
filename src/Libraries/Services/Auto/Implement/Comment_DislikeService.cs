// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-12-03 10:35:07
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class Comment_DislikeService : BaseService<Comment_Dislike>, IComment_DislikeService
    {
        private readonly IComment_DislikeRepository _repository;
        public Comment_DislikeService(IComment_DislikeRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
