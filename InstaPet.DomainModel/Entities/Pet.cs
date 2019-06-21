using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Entities
{
    public class Pet : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Pet()
        {
            Posts = new List<Post>();
        }
    }
}