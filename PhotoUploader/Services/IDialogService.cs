using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoUploader.Services
{
    public interface IDialogService
    {
        Task ShowDialogMessageAsync(string title, string message, string buttonText);
    }
}
