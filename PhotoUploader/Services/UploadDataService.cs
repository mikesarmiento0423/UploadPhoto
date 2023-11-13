using Newtonsoft.Json;
using PhotoUploader.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoUploader.Services
{
    public class UploadDataService : IUploadDataService
    {
        private const string ControllerName = "https://reqres.in/api/users";

        protected HttpClient HttpClient { get; }

        public UploadDataService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public async Task<SitePhotoDto> Post(SitePhoto sitePhoto)
        {
            var result = new SitePhotoDto();
            var requestDto = new SitePhotoDto(sitePhoto);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, ControllerName);
            httpRequest.Content = FormatContent(requestDto);

            using (var response = await HttpClient.SendAsync(httpRequest).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.DeserializeResponse<SitePhotoDto>();
                }
            };

            return result;
        }

        private string GetContentString(object contentObj)
        {
            if (contentObj == null)
            {
                return string.Empty;
            }

            if (contentObj is string)
            {
                return contentObj.ToString();
            }

            return JsonConvert.SerializeObject(contentObj);
        }

        private HttpContent FormatContent(object contentObj)
        {
            string contentType = contentObj is string
                ? "text/plain"
                : "application/json";

            var content = GetContentString(contentObj);

            return new StringContent(content, Encoding.UTF8, contentType);
        }
    }
}
