using Windows.UI.Xaml;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Template10.Services.NavigationService;
using CollectionsWorkUWPTemplate10.Views;
using CollectionsWorkUWPTemplate10.ViewModels;
using CollectionsWorkUWPTemplate10.Services.RepositoryServices;
using CollectionsWorkUWPTemplate10.Models;
using GalaSoft.MvvmLight.Ioc;
using CollectionsWorkUWPTemplate10.Services.DialogSevices;

namespace CollectionsWorkUWPTemplate10
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : BootStrapper
    {
        //SimpleInjector.Container container;
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region app settings

            // some settings must be set in app.constructor
            var settings = SettingsService.Instance;
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;

            #endregion

            //#region IO container
            // 1. Create a new Simple Injector container
            //container = new SimpleInjector.Container();

            //// 2. Configure the container (register)
            //////container.Register<IOrderRepository, SqlOrderRepository>();
            //////container.Register<ILogger, FileLogger>(Lifestyle.Singleton);
            //container.Register<IRepositoryService<Person>, PersonsRepositoryServiceFake>();
            //container.Register<AddEditPageViewModel>();

            //// 3. Verify your configuration
            //try
            //{
            //    container.Verify();
            //}
            //catch (Exception ex)
            //{
            //    string e = ex.Message;
            //}

            ////#endregion
        }
        public override UIElement CreateRootElement(IActivatedEventArgs e)
        {
            var service = NavigationServiceFactory(BackButton.Attach, ExistingContent.Exclude);
            return new ModalDialog
            {
                DisableBackButtonWhenModal = true,
                Content = new Views.Shell(service),
                ModalContent = new Views.Busy(),
            };
        }
        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // Регистрируем зависимости  в контейнере
            SimpleIoc.Default.Register<MasterDetailPageViewModel>();
            SimpleIoc.Default.Register<AddEditPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
            SimpleIoc.Default.Register<FavoritesPageViewModel>();
            SimpleIoc.Default.Register<IRepositoryService<Person>, PersonsRepositoryServiceFake>();
            SimpleIoc.Default.Register<PersonContentDialogViewModel>();
            SimpleIoc.Default.Register<IDialogSevices, PersonDialogService>();
            SimpleIoc.Default.Register<ValidatePageViewModel>();

            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(Views.MasterDetailPage));
        }
        public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        {
            if (page is MasterDetailPage)
                return SimpleIoc.Default.GetInstance<MasterDetailPageViewModel>();
            else if (page is AddEditPage)
                return SimpleIoc.Default.GetInstanceWithoutCaching<AddEditPageViewModel>();
            else if (page is SettingsPage)
                return SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
            else if (page is FavoritesPage)
                return SimpleIoc.Default.GetInstance<FavoritesPageViewModel>();
            else if (page is ValidatePage)
                return SimpleIoc.Default.GetInstanceWithoutCaching<ValidatePageViewModel>();
            else
                return base.ResolveForPage(page, navigationService);
        }
    }
}
