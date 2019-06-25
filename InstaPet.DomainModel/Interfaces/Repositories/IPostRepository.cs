using InstaPet.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        IEnumerable<Post> GetAllFromOwner(Guid ownerId);
    }
}
