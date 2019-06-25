using InstaPet.DomainModel.Entities;
using InstaPet.DomainModel.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaPet.DomainService
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public void Create(Profile profile)
        {
            _profileRepository.Create(profile);
        }

        public Profile Read(Guid id)
        {
            return _profileRepository.Read(id);
        }

        public void Update(Profile profile)
        {
            _profileRepository.Update(profile);
        }

        public void Deactivate(Guid id)
        {
            _profileRepository.Deactivate(id);
        }

        public Profile SearchByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("E-mail é obrigatório.");

            return _profileRepository.SearchByEmail(email);
        }

        public void Activate(Guid id)
        {
            _profileRepository.Activate(id);
        }
    }
}
