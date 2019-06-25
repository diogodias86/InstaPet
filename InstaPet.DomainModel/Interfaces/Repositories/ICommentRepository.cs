using InstaPet.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment, Guid>
    {
        IEnumerable<Comment> GetAllFromPost(Guid postId);
    }
}
