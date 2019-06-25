using InstaPet.DomainModel.Entities;
using System;
using System.Collections.Generic;

namespace InstaPet.DomainModel.Interfaces.Repositories
{
    public interface IPetRepository : IRepository<Pet, Guid>
    {
        IEnumerable<Pet> GetAllFromProfile(Guid profileId);
    }
}
