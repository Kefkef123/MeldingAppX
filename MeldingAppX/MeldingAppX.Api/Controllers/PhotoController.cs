using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using MeldingAppX.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MeldingAppX.Api.Controllers
{
    public class PhotoController : ApiController
    {
        public PhotoController()
        {
            _db = new MeldingAppContext();
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            _blobClient = _storageAccount.CreateCloudBlobClient();

            _container = _blobClient.GetContainerReference("photos");
            _container.CreateIfNotExists();
            _container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

        }

        public async Task<IEnumerable<Photo>> Get()
        {
            var photoEntities = _db.Photos;
            var photos = new List<Photo>();

            foreach (var photoEntity in photoEntities)
            {
                var image = GetImage(photoEntity.Key);

                var photo = new Photo
                {
                    id = photoEntity.Id,
                    ContentLength = photoEntity.ContentLength,
                    ContentType = photoEntity.ContentType,
                    Name = photoEntity.Name,
                    Url = image.Uri.ToString()
                };

                photos.Add(photo);
            }

            return photos;
        }

        private async Task<Photo> Get(int id)
        {
            var photoEntity = await _db.Photos.FindAsync(id);

            var image = GetImage(photoEntity.Key);

            var photo = new Photo
            {
                id = photoEntity.Id,
                ContentLength = photoEntity.ContentLength,
                ContentType = photoEntity.ContentType,
                Name = photoEntity.Name,
                Url = image.Uri.ToString()
            };

            return photo;
        }

        public async Task<IHttpActionResult> Post([FromBody] Photo photo)
        {
            var image = Convert.FromBase64String(photo.EncodedFile);

            var fileExtension = Path.GetExtension(photo.FileName);

            var key = String.Format("Photo-{0}{1}", Guid.NewGuid(), fileExtension);

            using (var stream = new MemoryStream(image))
            {
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(key);
                blockBlob.Properties.ContentType = photo.ContentType;
                await blockBlob.UploadFromStreamAsync(stream);
            }

            var photoEntity = new PhotoEntity
            {
                ContentLength = photo.ContentLength,
                ContentType = photo.ContentType,
                Key = key,
                Name = photo.Name
            };

            if (photo.Name == null)
            {
                photoEntity.Name = photo.FileName;
                photoEntity.Name = photoEntity.Name.Replace(fileExtension, "");
            }

            _db.Photos.Add(photoEntity);
            await _db.SaveChangesAsync();

            return Ok();
        }

        private CloudBlockBlob GetImage(string key)
        {
            var image = _container.GetBlockBlobReference(key);
            return image;
        }

        private MeldingAppContext _db;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _container;


    }
}
