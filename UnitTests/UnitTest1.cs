using FakeItEasy;
using PhotoUploader.Models;
using PhotoUploader.Services;
using PhotoUploader.ViewModels;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task PickPhotoAsync_IsValidPhoto_ShouldConvertImageProperly()
        {
            //Arrange
            var fakeMediaPickerService = A.Fake<IMediaPickerService>();
            A.CallTo(() => fakeMediaPickerService.PickPhoto()).Returns(new FileResult("testPath") { FileName = "testFileName"});

            var vm = CreateViewModel(mediaPickerService: fakeMediaPickerService);

            //Act
            vm.OnNavigatedTo(null);
            await vm.PickPhotoAsync();

            //Assert
            A.CallTo(() => fakeMediaPickerService.ConvertImageToString(A<FileResult>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeMediaPickerService.ConvertImageToImageSource(A<FileResult>._)).MustHaveHappenedOnceExactly();
            Assert.That(vm.SitePhoto != null, Is.True);
        }

        [Test]
        public void RemovePhotoCommand_ShouldRemovePhotoFromList()
        {
            //Arrange
            var fakeMediaPickerService = A.Fake<IMediaPickerService>();
            A.CallTo(() => fakeMediaPickerService.PickPhoto()).Returns(new FileResult("testPath") { FileName = "testFileName" });

            var vm = CreateViewModel(mediaPickerService: fakeMediaPickerService);
            var photoFileNameToRemove = "TestFileName";
            vm.SitePhoto = new SitePhoto()
            {
                Photos = new ObservableCollection<ImageDetails>()
                {
                    new ImageDetails()
                    {
                        FileName = photoFileNameToRemove
                    },
                    new ImageDetails()
                    {
                        FileName = "TestFileName2"
                    }
                },
                Comment = "Test Comment"
            };

            //Act
            vm.OnNavigatedTo(null);
            vm.RemovePhotoCommand.Execute(new ImageDetails() { FileName = photoFileNameToRemove });

            //Assert
            Assert.IsFalse(vm.SitePhoto.Photos.Any(c => c.FileName == photoFileNameToRemove));
        }

        private UploadPageViewModel CreateViewModel(
            INavigationService navigationService = null,
            IMediaPickerService mediaPickerService = null,
            IUploadDataService uploadDataService = null,
            IDialogService dialogService = null)
        {
            return new UploadPageViewModel(
                navigationService ?? A.Fake<INavigationService>(),
                mediaPickerService ?? A.Fake<IMediaPickerService>(),
                uploadDataService ?? A.Fake<IUploadDataService>(),
                dialogService ?? A.Fake<IDialogService>());
        }
    }
}