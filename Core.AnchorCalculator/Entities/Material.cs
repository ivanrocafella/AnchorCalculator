using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AnchorCalculator.Entities.Enums;

namespace Core.AnchorCalculator.Entities
{
    public class Material : Entity
    {
        public string? Name { get; set; }
        [NotMapped]
        public string? FullName { get { return $"{Name} ⌀{Size} {Type}"; } }
        public double Size { get; set; }
        public double TimeThreadRolling { get; set; } // unity of measure = н
        public double TimeThreadCutting { get; set; } // unity of measure = н
        public double Plashka { get; set; } // unity of measure = 1 плашка
        public double Cutter { get; set; } // unity of measure = 1 резец
        public double TimeBandSaw { get; set; } // unity of measure = н
        public double LengthBladeBandSaw { get; set; } // unity of measure = м
        public virtual int TypeId
        {
            get => (int)Type;
            set => Type = (Enums.Type)value;
        }
        [EnumDataType(typeof(Enums.Type))]
        public Enums.Type Type { get; set; }
        public double PricePerMetr { get; set; }
        public DateTime DateUpdate { get; set; }


        public ICollection<Anchor>? Anchors { get; set; }

        public Material()
        {
            Anchors = new HashSet<Anchor>();
        }
    }
}
