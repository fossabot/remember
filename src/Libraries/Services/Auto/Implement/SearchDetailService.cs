// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-10-01 11:35:54
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class SearchDetailService : BaseService<SearchDetail>, ISearchDetailService
    {
        private readonly ISearchDetailRepository _repository;
        public SearchDetailService(ISearchDetailRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
