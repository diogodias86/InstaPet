using InstaPet.DomainModel.Entities;
using InstaPet.DomainModel.Interfaces.Repositories;
using InstaPet.Infrastructure.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaPet.Infrastructure.DataAccess.Repositories
{
    public class ProfileRepositoryEF : IProfileRepository
    {
        private readonly InstaPetDbContext _db;

        public ProfileRepositoryEF(InstaPetDbContext db)
        {
            _db = db;
        }

        public void Activate(Guid id)
        {
            var profile = Read(id);

            profile.Active = true;
            Update(profile);
        }

        public void Create(Profile profile)
        {
            _db.Profiles.Add(profile);
            _db.SaveChanges();
        }

        public void Deactivate(Guid id)
        {
            var profile = Read(id);

            profile.Active = false;
            Update(profile);
        }

        public Profile Read(Guid id)
        {
            return _db.Profiles.Find(id);
        }

        public Profile SearchByEmail(string email)
        {
            return _db.Profiles
               .Where(p => p.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();
        }

        public void Update(Profile profile)
        {
            _db.Profiles.Update(profile);
            _db.SaveChanges();
        }
    }
}
