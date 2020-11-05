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
    public class FileHelper
    {
        private readonly IFileService fileService;
        private readonly string FileServerUrl;

        public FileHelper(IFileService fileService, string fileServerUrl)
        {
            this.fileService = fileService;
            FileServerUrl = fileServerUrl;
        }


        public ServiceResponse<int> Upload(IFormFile file,
            string referanceFunction, int referanceId)
        {
            var response = new ServiceResponse<int>();

            HttpClient client = new HttpClient();
            var uploadUrl = FileServerUrl + "/file/upload";

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

        public string GetFileUrlByFilename(string filename)
        {
            return FileServerUrl + "/file/public/" + filename;
        }

        public string GetFileUrlById(int id)
        {
            var fileResponse = fileService.GetFileById(id);
            if (fileResponse.IsSucceeded && fileResponse.Result != null)
            {
                return this.GetFileUrlByFilename(fileResponse.Result.FileName);
            }
            return "";
        }


    }
}
