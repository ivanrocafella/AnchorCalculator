using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    public class Anchor : Entity
    {
        public int LengthAnchor { get; set; }
        public int DiameterAnchor { get; set; }
        public double WeightAnchor { get; set; }
        public double PriceAnchor { get; set; }
        public int LengthBend { get; set; }
        public int RadiusBend { get; set; }
        public int LengthThread { get; set; }
        public int DiameterThread { get; set; }
        public double StepThread { get; set; }
        public double PriceTotalAnchor { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

    }
}
