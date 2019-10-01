// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-10-01 11:35:54
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class CardBoxService : BaseService<CardBox>, ICardBoxService
    {
        private readonly ICardBoxRepository _repository;
        public CardBoxService(ICardBoxRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
