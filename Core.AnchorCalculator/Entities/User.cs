using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnchorCalculator.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Anchor> Anchors { get; set; }

        public User()
        {
            Anchors = new HashSet<Anchor>();
        }
    }
}
