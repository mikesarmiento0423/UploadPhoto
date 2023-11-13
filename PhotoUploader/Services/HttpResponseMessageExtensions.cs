using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhotoUploader.Services
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeResponse<T>(this HttpResponseMessage response, JsonSerializerSettings serializerSettings = null) where T : class
        {
            var sw = new Stopwatch();
            sw.Start();
            long contentLength;
            T responseDto;

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var jsonSerializer = new JsonSerializer();
                if (serializerSettings != null)
                {
                    jsonSerializer = JsonSerializer.Create(serializerSettings);
                }

                try
                {
                    responseDto = jsonSerializer.Deserialize<T>(jsonReader);
                }
                catch (Exception exception)
                {
                    var raw = reader.ReadToEnd();

                    if (string.IsNullOrWhiteSpace(raw))
                    {
                        throw new HttpRequestException("JsonDeserializeExceptionMessage", exception);
                    }
                    if (exception is JsonException jsonException)
                    {
                        throw new HttpRequestException("JsonDeserializeExceptionMessage", exception);
                    }
                    else
                    {
                        throw new HttpRequestException("GeneralExceptionMessage", exception);
                    }
                }

                contentLength = reader.BaseStream.Length;
            }

            if (responseDto == null)
            {
                throw new HttpRequestException("");
            }

            sw.Stop();
            return responseDto;
        }
    }
}
