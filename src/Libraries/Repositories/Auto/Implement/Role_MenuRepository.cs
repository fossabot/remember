// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-12-03 10:32:23
using Domain.Entities;
using Repositories.Core;
using Repositories.Interface;

namespace Repositories.Implement
{
    public partial class Role_MenuRepository : BaseRepository<Role_Menu>, IRole_MenuRepository
    {
        private readonly RemDbContext _context;

        public Role_MenuRepository(RemDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
