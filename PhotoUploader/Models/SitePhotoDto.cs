using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Xamarin.Essentials.Permissions;
using System.Xml.Linq;
using System.IO.Compression;
using System.IO;

namespace PhotoUploader.Models
{
    [JsonObject]
    public class SitePhotoDto
    {
        public SitePhotoDto()
        {

        }

        public SitePhotoDto(SitePhoto sitePhoto)
        {
            var listImage = new List<string>();
            foreach(var item in sitePhoto.Photos)
            {
                listImage.Add(item.ImageBase64String);
            }
            Photos = listImage;
            Comment = sitePhoto.Comment;
            Date = sitePhoto.Date;
            Area = sitePhoto.Area;
            TaskCategory = sitePhoto.TaskCategory;
            Tags = sitePhoto.Tags;
            Event = sitePhoto.Event;
        }

        [JsonProperty("photos")]
        public List<string> Photos { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("taskCategory")]
        public string TaskCategory { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
