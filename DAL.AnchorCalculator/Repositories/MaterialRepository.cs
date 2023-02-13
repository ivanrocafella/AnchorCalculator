using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator.Repositories
{
    internal class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
