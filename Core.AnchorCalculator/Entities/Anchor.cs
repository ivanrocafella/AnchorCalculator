using Core.AnchorCalculator.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    public class Anchor : Entity
    {
        public int Length { get; set; }
        public float Diameter { get; set; }
        public double Weight { get; set; }
        public double Sebes { get; set; }        
        public double BatchSebes { get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Get or set anchhor's billet 
        /// </summary>
        public int BendLength { get; set; }
        public int BendRadius { get; set; }
        public int ThreadLength { get; set; }
        public int ThreadLengthSecond { get; set; }
        public int ThreadDiameter { get; set; }
        public float ThreadStep { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public string? SvgElement { get; set; }
        public double BatchWeight { get; set; }
        public double BilletLength { get; set; }
        public string MaterialJson { get; set; } // json
        public string UserJson { get; set; } // json
        public virtual int KindId
        {
            get => (int)Kind;
            set => Kind = (Kind)value;
        }
        [EnumDataType(typeof(Kind))]
        public Kind Kind { get; set; }
        public virtual int ProductionId
        {
            get => (int)Production;
            set => Production = (Production)value;
        }
        public Production Production { get; set; }
        public double PricePerMetr { get; set; }
        public double PriceMaterial { get; set; }
        public double BatchPriceMaterial { get; set; }
        public double LengthPathRoller { get; set; } // мм
        public double LengthBeforeEndPathRoller { get; set; } // мм
        public double TimeProductionUnity { get; set; }
        public double TimeProductionBatch { get; set; }


        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
