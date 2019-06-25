using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.DomainModel.Entities
{
    public class Profile : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }

        public Profile()
        {
            Pets = new List<Pet>();
        }
    }
}
