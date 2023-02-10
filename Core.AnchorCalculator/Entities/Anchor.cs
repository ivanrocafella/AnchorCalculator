using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    internal class Anchor : Entity
    {
        public int LengthAnchor { get; set; }
        public int DiameterAnchor { get; }
        public double WeightAnchor { get; }
        public double PriceAnchor { get; }
        public int LengthBend { get; set; }
        public int RadiusBend { get; }
        public int LengthThread { get; set; }
        public int DiameterThread { get; set; }
        public double StepThread { get; }
        public double PriceTotalAnchor { get; }
        public int Quantity { get; }

    }
}
