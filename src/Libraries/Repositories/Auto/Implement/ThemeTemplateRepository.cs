// Code generated by a template
// Project: Remember
// LastUpadteTime: 2019-10-01 11:03:23
using Domain.Entities;
using Repositories.Core;
using Repositories.Interface;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Implement
{
    public partial class ThemeTemplateRepository : BaseRepository<ThemeTemplate>, IThemeTemplateRepository
    {
        private readonly RemDbContext _context;

        public ThemeTemplateRepository(RemDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
