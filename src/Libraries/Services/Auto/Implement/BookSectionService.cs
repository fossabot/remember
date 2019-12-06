// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-12-03 10:35:07
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class BookSectionService : BaseService<BookSection>, IBookSectionService
    {
        private readonly IBookSectionRepository _repository;
        public BookSectionService(IBookSectionRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}