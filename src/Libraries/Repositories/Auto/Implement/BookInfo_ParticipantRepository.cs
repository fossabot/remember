// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-12-03 10:32:23
using Domain.Entities;
using Repositories.Core;
using Repositories.Interface;

namespace Repositories.Implement
{
    public partial class BookInfo_ParticipantRepository : BaseRepository<BookInfo_Participant>, IBookInfo_ParticipantRepository
    {
        private readonly RemDbContext _context;

        public BookInfo_ParticipantRepository(RemDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
