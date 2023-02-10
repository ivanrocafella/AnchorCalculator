using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    internal class Material : Entity
    {
        public int Size { get; set; }
        public string? Type { get; set; }
        public string? Name { get; }

        public Material(int size, string? type)
            => Name = $"Круг {size} {type}";
    }
}
