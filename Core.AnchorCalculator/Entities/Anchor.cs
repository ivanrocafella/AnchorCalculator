using Core.AnchorCalculator.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    public class Anchor : Entity
    {
        public int Length { get; set; }
        public int LengthSecond { get; set; }
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
        [NotMapped]
        public double PriceMaterial { get => BatchPriceMaterial / Quantity; }
        public double BatchPriceMaterial { get; set; }
        public double PriceProductionThread { get => BatchPriceProductionThread / Quantity; }
        public double BatchPriceProductionThread { get; set; }
        public double PriceProductionBend { get => BatchPriceProductionBend / Quantity; }
        public double BatchPriceProductionBend { get; set; }
        public double PriceProductionBandSaw { get => BatchPriceProductionBandSaw / Quantity; }
        public double BatchPriceProductionBandSaw { get; set; }
        public double LengthPathRoller { get; set; } // мм
        public double LengthBeforeEndPathRoller { get; set; } // мм
        public double TimeProductionThread { get; set; } // h
        public double TimeProductionBend { get; set; } // h
        public double TimeProductionBandSaw { get; set; } // h
        public double LengthFull { get; set; } // m
        [NotMapped]
        public bool HasCuttingThread { get; set; } // bool
        public bool WithoutBindThreadDiamMatetial { get; set; } // bool


        public int? MaterialId { get; set; }
        public Material? Material { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
