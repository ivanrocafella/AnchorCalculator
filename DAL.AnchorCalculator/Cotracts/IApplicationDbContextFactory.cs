using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator.Cotracts
{
    public interface IApplicationDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
