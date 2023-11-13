using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PhotoUploader.Models
{
    public class SitePhoto: BindableBase
    {
        private ObservableCollection<ImageDetails> _photos;
        private string _comment;
        private DateTime _date;
        private string _area;
        private string _taskCategory;
        private string _tags;
        private string _event;

        public ObservableCollection<ImageDetails> Photos
        {
            get => _photos;
            set => SetProperty(ref _photos, value);
        }

        public string Comment 
        {
            get => _comment ;
            set => SetProperty(ref _comment, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string Area
        {
            get => _area;
            set => SetProperty(ref _area, value);
        }

        public string TaskCategory
        {
            get => _taskCategory;
            set => SetProperty(ref _taskCategory, value);
        }

        public string Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public string Event
        {
            get => _event;
            set => SetProperty(ref _event, value);
        }

        public SitePhoto()
        {
            Photos = new ObservableCollection<ImageDetails>();
        }
    }
}
