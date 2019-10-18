using Imobi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imobi.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(Exception ex)
        {
            if (ex == null) return;

            var error = ex.Message;
            if (!string.IsNullOrWhiteSpace(error))
            {
                await ShowAsync(error);
                return;
            }

            var detailError = ex.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailError))
            {
                await ShowAsync(detailError);
                return;
            }

            var detailErrorFurther = ex.InnerException?.InnerException?.Message;
            if (!string.IsNullOrWhiteSpace(detailErrorFurther)) await ShowAsync(detailErrorFurther);
        }

        public async Task ShowAsync(string message)
        {
            await Application.Current.MainPage.DisplayAlert(Constants.Constants.AppName, message, "OK");
        }

        public async Task ShowAsync(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> ShowConfirmationAsync(string message, string accept, string cancel, string title)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<bool> ShowConfirmationAsync(string message, string title)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "OK", "Cancelar");
        }

        public async Task<string> ShowOptionsAsync(string message, params string[] options)
        {
            return await Application.Current.MainPage.DisplayActionSheet(message, "Cancelar", null, options);
        }

        public async Task<string> ShowOptionsAsync(string message, IEnumerable<string> options, string cancel)
        {
            return await Application.Current.MainPage.DisplayActionSheet(message, cancel, null, options.ToArray());
        }
    }
}