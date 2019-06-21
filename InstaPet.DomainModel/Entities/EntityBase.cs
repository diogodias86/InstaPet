using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.DomainModel.Entities
{
    public abstract class EntityBase<EntityId>
    {
        public EntityId Id { get; set; }
    }
}
