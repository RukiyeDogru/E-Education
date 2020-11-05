using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Core3Base.Domain.Model.Base;
using Core3Base.Domain.Services.Services;
using Microsoft.AspNetCore.Http;

namespace Core3Base.Domain.Services.Impl.Helper
{
    public class ImageHelper
    {
        private readonly IFileService fileService;
        private readonly string FileServerUrl;

        public ImageHelper(IFileService fileService, string fileServerUrl)
        {
            this.fileService = fileService;
            FileServerUrl = fileServerUrl;
        }

        public ServiceResponse<int> Upload(IFormFile file,
            string referanceFunction, int referanceId)
        {
            var response = new ServiceResponse<int>();

            HttpClient client = new HttpClient();
            var uploadUrl = FileServerUrl + "/image/upload";

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                ByteArrayContent bytes = new ByteArrayContent(memoryStream.ToArray());
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                multiContent.Add(bytes, "File", file.FileName);
                multiContent.Add(new StringContent(referanceId.ToString()), "ReferanceId");
                multiContent.Add(new StringContent(referanceFunction.ToString()), "ReferanceFunction");
                multiContent.Add(new StringContent("false"), "IsNeedAuth"); //TODO
                var result = client.PostAsync(uploadUrl, multiContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    response.Result = int.Parse(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    response.SetError("Dosya Yüklenirken Hata Oluştu"); //TODO
                }
            }

            return response;

        }

        public string GetImageUrlByFilename(string filename)
        {
            return FileServerUrl + "/image/public/" + filename;
        }

        public string GetImageUrlById(int id)
        {
            var fileResponse = fileService.GetImageById(id);
            if (fileResponse.IsSucceeded && fileResponse.Result != null)
            {
                return this.GetImageUrlByFilename(fileResponse.Result.FileName);
            }
            return "";
        }



    }
}
