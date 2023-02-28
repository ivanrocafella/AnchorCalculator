using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.AnchorCalculator.Entities.Enums;

namespace Core.AnchorCalculator.Entities
{
    public class Material : Entity
    {
        public string? Name { get; set; }
        public double Size { get; set; }
        public virtual int TypeId
        {
            get => (int)Type;
            set => Type = (Enums.Type)value;
        }
        [EnumDataType(typeof(Enums.Type))]
        public Enums.Type Type { get; set; }
        public double PricePerMetr { get; set; }


        public ICollection<Anchor>? Anchors { get; set; }

        public Material()
        {
            Anchors = new HashSet<Anchor>();
        }
    }
}
