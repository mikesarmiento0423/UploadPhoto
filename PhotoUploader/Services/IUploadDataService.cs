using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PhotoUploader.Models;

namespace PhotoUploader.Services
{
    public interface IUploadDataService
    {
        Task<SitePhotoDto> Post(SitePhoto sitePhoto);
    }
}
