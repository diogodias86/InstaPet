using System.IO;

namespace InstaPet.DomainService.Interfaces
{
    public interface IBlobStorage
    {
        string UploadFile(string fileName, Stream fileContent, string blobContainerName, string contentType);
    }
}
