using CollectionsWorkUWPTemplate10.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CollectionsWorkUWPTemplate10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValidatePage : Page
    {
        ValidatePageViewModel _viewModel;

        public ValidatePage()
        {
            this.InitializeComponent();
        }
        public ValidatePageViewModel ViewModel
        {
            get { return _viewModel ?? (_viewModel = (ValidatePageViewModel)DataContext); }
        }
    }
}
