using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Imobi.Services.Interfaces
{
    public interface IMessageService
    {
        Task ShowAsync(Exception ex);

        Task ShowAsync(string message);

        Task ShowAsync(string title, string message);

        Task<bool> ShowConfirmationAsync(string message, string accept, string cancel, string title = Constants.Constants.AppName);

        Task<bool> ShowConfirmationAsync(string message, string title = Constants.Constants.AppName);

        Task<string> ShowOptionsAsync(string message, params string[] options);

        Task<string> ShowOptionsAsync(string message, IEnumerable<string> options, string cancel = "Cancelar");
    }
}