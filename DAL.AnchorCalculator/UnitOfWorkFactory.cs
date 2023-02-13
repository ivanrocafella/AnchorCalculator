using Core.AnchorCalculator;
using DAL.AnchorCalculator.Cotracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IApplicationDbContextFactory _applicationDbContextFactory;

        public UnitOfWorkFactory(IApplicationDbContextFactory applicationDbContextFactory)
        {
            _applicationDbContextFactory = applicationDbContextFactory;
        }

        public IUnitOfWork CreateUnitOfWork() => new UnitOfWork(_applicationDbContextFactory.Create());
    }
}
