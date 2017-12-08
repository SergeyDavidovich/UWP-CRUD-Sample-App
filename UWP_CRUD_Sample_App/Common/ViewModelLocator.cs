using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.ViewModels;
using CollectionsWorkUWPTemplate10.Services.RepositoryServices;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;


namespace CollectionsWorkUWPTemplate10.Common
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(()=>SimpleIoc.Default);
        }
        private MasterDetailPageViewModel _MasterDetailPageViewModel;
        public MasterDetailPageViewModel MasterDetailPage => _MasterDetailPageViewModel ??
            (_MasterDetailPageViewModel = new MasterDetailPageViewModel(SimpleIoc.Default.GetInstance<PersonsRepositoryServiceFake>()));

        private AddEditPageViewModel _AddEditPageViewModel;
        public AddEditPageViewModel AddEditPageViewModel
        {
            get
            {
                return _AddEditPageViewModel ??
            (_AddEditPageViewModel = new AddEditPageViewModel(SimpleIoc.Default.GetInstance<PersonsRepositoryServiceFake>()));
            }
        }

    }
}
