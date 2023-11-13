using PhotoUploader.Services;
using PhotoUploader.ViewModels;
using PhotoUploader.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoUploader
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"NavigationPage/{nameof(UploadPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<UploadPage, UploadPageViewModel>();
            containerRegistry.Register<IMediaPickerService, MediaPickerService>();
            containerRegistry.Register<IUploadDataService, UploadDataService>();
            containerRegistry.Register<IDialogService, DialogService>();
        }
    }
}
