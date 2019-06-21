using System;

namespace InstaPet.DomainModel.Entities
{
    public class Comment : EntityBase<Guid>
    {
        public Pet Creator { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime PublishDateTime { get; set; }
        public Post Post { get; set; }
    }
}