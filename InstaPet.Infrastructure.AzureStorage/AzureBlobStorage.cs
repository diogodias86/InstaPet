using InstaPet.DomainService.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InstaPet.Infrastructure.BlobStorage
{
    public class AzureBlobService : IBlobStorage
    {
        private readonly CloudStorageAccount _cloudStorageAccount;

        public AzureBlobService()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(Properties.Resources.
                ResourceManager.GetString("StorageAccountConnectionString"));
        }

        public string UploadFile(string fileName, Stream fileContent, string blobContainerName, string contentType)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(blobContainerName);

            blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            blobContainer.CreateIfNotExistsAsync();

            var blob = blobContainer.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = contentType;

            blob.UploadFromStreamAsync(fileContent);

            return blob.Uri.AbsoluteUri;
        }
    }
}
