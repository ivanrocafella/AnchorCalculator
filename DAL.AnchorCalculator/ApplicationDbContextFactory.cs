using DAL.AnchorCalculator.Cotracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator
{
    public class ApplicationDbContextFactory : IApplicationDbContextFactory
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public ApplicationDbContext Create() => new(_options);
    }
}
