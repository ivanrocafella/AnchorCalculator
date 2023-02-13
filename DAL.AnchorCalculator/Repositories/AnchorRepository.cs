using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator.Repositories
{
    internal class AnchorRepository : Repository<Anchor>, IAnchorRepository
    {
        public AnchorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
    