using PhotoUploader.Models;
using PhotoUploader.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace PhotoUploader.ViewModels
{
    public class UploadPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IMediaPickerService _mediaPickerService;
        private readonly IUploadDataService _uploadDataService;
        private readonly IDialogService _dialogService;

        private SitePhoto _sitePhoto;
        private bool _isLoading;
        private ICommand _pickPhotoCommand;
        private ICommand _removePhotoCommand;
        private ICommand _uploadPhotoCommand;

        public ICommand PickPhotoCommand => _pickPhotoCommand ?? (_pickPhotoCommand = new Command(async () => await PickPhotoAsync()));

        public ICommand RemovePhotoCommand => _removePhotoCommand ?? (_removePhotoCommand = new Command((parameter) => RemovePhoto((ImageDetails)parameter)));

        public ICommand UploadPhotoCommand => _uploadPhotoCommand ?? (_uploadPhotoCommand = new Command(async () => await UploadPhotoAsync()));

        public SitePhoto SitePhoto
        {
            get => _sitePhoto;
            set => SetProperty(ref _sitePhoto, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public UploadPageViewModel(INavigationService navigationService, IMediaPickerService mediaPickerService, IUploadDataService uploadDataService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _mediaPickerService = mediaPickerService;
            _uploadDataService = uploadDataService;
            _dialogService = dialogService;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            SitePhoto = new SitePhoto();
        }

        public async Task PickPhotoAsync()
        {
            try
            {
                IsLoading = true;

                var fileResult = await _mediaPickerService.PickPhoto();

                if (fileResult != null)
                {
                    var imageDetails = new ImageDetails()
                    {
                        FileName = fileResult.FileName,
                        ImageBase64String = _mediaPickerService.ConvertImageToString(fileResult),
                        ImageSource = await _mediaPickerService.ConvertImageToImageSource(fileResult)
                    };

                    if (!SitePhoto.Photos.Any(c => c.FileName.Contains(imageDetails.FileName)))
                        SitePhoto.Photos.Add(imageDetails);
                };
            }
            catch 
            {
            }
            finally 
            { 
                IsLoading = false;
            }
        }

        public void RemovePhoto(ImageDetails image)
        {
            SitePhoto.Photos.Remove(image);
        }

        public async Task UploadPhotoAsync()
        {
            try
            {
                IsLoading = true;
                var result = await _uploadDataService.Post(SitePhoto);
                if (result != null)
                {
                    await _dialogService.ShowDialogMessageAsync("Upload Photo", "Success!", "OK");
                    SitePhoto = new SitePhoto();
                }
            }
            catch
            {
            }
            finally 
            { 
                IsLoading = false;
            }
        }
    }
}
