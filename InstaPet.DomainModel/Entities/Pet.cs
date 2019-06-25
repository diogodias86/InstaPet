using InstaPet.DomainModel.ValueObjects;
using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Entities
{
    public class Pet : EntityBase<Guid>
    {
        public Profile Owner { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public Breed Breed { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Pet()
        {
            Owner = new Profile();
            Posts = new List<Post>();
        }
    }
}