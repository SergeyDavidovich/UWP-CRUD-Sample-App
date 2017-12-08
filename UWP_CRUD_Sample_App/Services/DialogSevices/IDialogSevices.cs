using CollectionsWorkUWPTemplate10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CollectionsWorkUWPTemplate10.Services.DialogSevices
{
    public interface IDialogSevices
    {
        Task<ContentDialogResult> ShowAsync(object entity);
    }
}
