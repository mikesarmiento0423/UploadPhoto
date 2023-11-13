using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoUploader.Services
{
    public interface IMediaPickerService
    {
        Task<FileResult> PickPhoto();

        Task<ImageSource> ConvertImageToImageSource(FileResult fileResult);

        string ConvertImageToString(FileResult fileResult);
    }
}
