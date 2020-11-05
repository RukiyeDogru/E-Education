using System;
using System.Collections.Generic;
using System.Text;
using Core3Base.Domain.Model.Base;
using Core3Base.Infra.Data.Entity;

namespace Core3Base.Domain.Services.Services
{
    public interface IFileService
    {
        ServiceResponse<UploadedFile> AddFile(UploadedFile file);
        ServiceResponse<UploadedFile> GetFileById(int id);
        ServiceResponse<UploadedFile> GetFileByFilename(string filename);


        ServiceResponse<UploadedImage> AddImage(UploadedImage image);
        ServiceResponse<UploadedImage> GetImageById(int id);
        ServiceResponse<UploadedImage> GetImageByFilename(string filename);

    }
}
