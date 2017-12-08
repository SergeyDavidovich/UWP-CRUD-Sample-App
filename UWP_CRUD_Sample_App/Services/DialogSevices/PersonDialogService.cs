using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.Views.ContentDialogs;
using CollectionsWorkUWPTemplate10.Models;
using Windows.UI.Xaml.Controls;

namespace CollectionsWorkUWPTemplate10.Services.DialogSevices
{
    public class PersonDialogService : IDialogSevices
    {
        public ContentDialogResult ContentDialogResult { get; private set; }

        public async Task<ContentDialogResult> ShowAsync(object entity)
        {
            var contentDialog = new PersonContentDialog();

            contentDialog.ViewModel.CurrentPerson = entity as Person;
            return ContentDialogResult = await contentDialog.ShowAsync();
        }
    }
}
