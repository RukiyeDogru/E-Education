using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Services;
using Core3Base.Infra.Data.Entity;
using Core3Base.Infra.Data.Repository;

namespace Core3Base.Domain.Services.Impl.Services
{
    public class FileService : IFileService
    {
        private readonly IRepository<UploadedImage> imageRepository;
        private readonly IRepository<UploadedFile> fileRepository;

        public FileService(IRepository<UploadedImage> imageRepository, IRepository<UploadedFile> fileRepository)
        {
            this.imageRepository = imageRepository;
            this.fileRepository = fileRepository;
        }

        public ServiceResponse<UploadedFile> AddFile(UploadedFile file)
        {
            var response = new ServiceResponse<UploadedFile>();

            fileRepository.Insert(file);
            response.Result = file;
            return response;

        }

        public ServiceResponse<UploadedFile> GetFileByFilename(string filename)
        {
            var response = new ServiceResponse<UploadedFile>();

            response.Result = fileRepository.ListQueryableNoTracking
                .FirstOrDefault(x => x.FileName == filename);
            return response;
        }

        public ServiceResponse<UploadedFile> GetFileById(int id)
        {
            var response = new ServiceResponse<UploadedFile>();

            response.Result = fileRepository.GetById(id);
            return response;
        }

        public ServiceResponse<UploadedImage> AddImage(UploadedImage image)
        {
            var response = new ServiceResponse<UploadedImage>();

            imageRepository.Insert(image);
            response.Result = image;
            return response;
        }


        public ServiceResponse<UploadedImage> GetImageByFilename(string filename)
        {
            var response = new ServiceResponse<UploadedImage>();

            response.Result = imageRepository.ListQueryableNoTracking
                .FirstOrDefault(x => x.FileName == filename);
            return response;
        }

        public ServiceResponse<UploadedImage> GetImageById(int id)
        {
            var response = new ServiceResponse<UploadedImage>();

            response.Result = imageRepository.GetById(id);
            return response;
        }

    }
}
