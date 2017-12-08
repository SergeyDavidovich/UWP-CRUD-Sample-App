using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.ViewModels;

namespace CollectionsWorkUWPTemplate10.Common
{
    public class BaseViewModelLocator
    {
        public BaseViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MasterDetailPageViewModel>();
            SimpleIoc.Default.Register<AddEditPageViewModel>();
        }

        //public MasterDetailPageViewModel MasterDetailPage
        //{
        //    get { return ServiceLocator.Current.GetInstance<MasterDetailPageViewModel>(); }
        //}

        public AddEditPageViewModel AddEditPage
        {
            get { return ServiceLocator.Current.GetInstance<AddEditPageViewModel>(); }
        }
    }
}
