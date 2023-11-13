using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace PhotoUploader.Models
{
    public class ImageDetails : BindableBase
    {
        private string _fileName;
        private ImageSource _imageSource;
        private string _imageBase64String;

        public string Id { get; set; }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public string ImageBase64String
        {
            get => _imageBase64String;
            set => SetProperty(ref _imageBase64String, value);
        }
    }
}
