using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Entities
{
    public class Post : EntityBase<Guid>
    {
        public Pet Creator { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime PublishDateTime { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}