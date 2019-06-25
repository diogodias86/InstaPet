using InstaPet.DomainModel.Entities;
using InstaPet.DomainModel.Interfaces.Repositories;
using InstaPet.DomainService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace InstaPet.DomainService
{
    public class PetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IBlobStorage _blobStorage;

        public PetService(IPetRepository petRepository, IBlobStorage blobStorage)
        {
            _petRepository = petRepository;
            _blobStorage = blobStorage;
        }

        public IEnumerable<Pet> GetAllFromProfile(Guid profileId)
        {
            return _petRepository.GetAllFromProfile(profileId);
        }

        public string UploadPhoto(string fileName, Stream fileContent, string contentType)
        {
            return _blobStorage.UploadFile(fileName, fileContent, "pets", contentType);
        }

        public void Create(Pet pet)
        {
            _petRepository.Create(pet);
        }

        public Pet Read(Guid id)
        {
            return _petRepository.Read(id);
        }

        public void Update(Pet pet)
        {
            _petRepository.Update(pet);
        }

        public void Delete(Guid id)
        {
            _petRepository.Delete(id);
        }
    }
}
