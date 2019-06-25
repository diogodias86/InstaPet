using InstaPet.DomainModel.Entities;
using InstaPet.DomainModel.Interfaces.Repositories;
using InstaPet.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaPet.Infrastructure.DataAccess.Repositories
{
    public class PetRepositoryEF : IPetRepository
    {
        private readonly InstaPetDbContext _db;

        public PetRepositoryEF(InstaPetDbContext db)
        {
            _db = db;
        }

        public void Create(Pet entity)
        {
            _db.Pets.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _db.Remove(Read(id));
            _db.SaveChanges();
        }

        public IEnumerable<Pet> GetAllFromProfile(Guid profileId)
        {
            return _db.Pets.Where(p => p.Owner.Id.Equals(profileId));
        }

        public Pet Read(Guid id)
        {
            return _db.Pets.Find(id);
        }

        public IEnumerable<Pet> ReadAll()
        {
            return _db.Pets;
        }

        public void Update(Pet entity)
        {
            _db.Pets.Update(entity);
            _db.SaveChanges();
        }
    }
}
