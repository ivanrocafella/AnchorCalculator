using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    public class Anchor : Entity
    {
        public int Length { get; set; }
        public float Diameter { get; set; }
        public double Weight{ get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Get or set anchhor's billet 
        /// </summary>
        public int BendLength { get; set; }
        public int BendRadius { get; set; }
        public int ThreadLength { get; set; }
        public int ThreadDiameter { get; set; }
        public double ThreadStep { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public string? SvgElement { get; set; }
        public double BatchWeight { get; set; }
        public double BilletLength { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
