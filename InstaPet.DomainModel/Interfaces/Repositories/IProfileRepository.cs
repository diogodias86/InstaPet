using InstaPet.DomainModel.Entities;
using System;

namespace InstaPet.DomainModel.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        void Create(Profile profile);
        Profile Read(Guid id);        
        void Update(Profile profile);
        void Deactivate(Guid id);
        Profile SearchByEmail(string email);
        void Activate(Guid id);
    }
}
