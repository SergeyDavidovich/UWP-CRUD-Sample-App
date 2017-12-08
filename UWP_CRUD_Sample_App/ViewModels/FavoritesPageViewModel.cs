using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using CollectionsWorkUWPTemplate10.Models;
using CollectionsWorkUWPTemplate10.Services.RepositoryServices;
using GalaSoft.MvvmLight.Messaging;
using CollectionsWorkUWPTemplate10.Messages;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.Popups;
using CollectionsWorkUWPTemplate10.Services.DialogSevices;

namespace CollectionsWorkUWPTemplate10.ViewModels
{
    public class FavoritesPageViewModel : ViewModelBase
    {
        #region Fields
        Person _SelectedPerson;
        ObservableCollection<Person> _persons;
        IRepositoryService<Person> _personsRepositary;
        IDialogSevices _dialog;

        #endregion
        #region Constructor
        public FavoritesPageViewModel(IRepositoryService<Person> personsRepository, IDialogSevices dialog)
        {
            _personsRepositary = personsRepository;
            _dialog = dialog;
        }
        #endregion

        #region Page Navigation events
        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            var list = await _personsRepositary.GetAllFavoritesASync();
            this.Persons = new ObservableCollection<Person>(list);
        }
        #endregion

        #region Bindable properties
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { Set(ref _persons, value); }
        }
        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set { Set(ref _SelectedPerson, value); }
        }

        #endregion

        public async void ShowDialog()
        {
            ContentDialogResult result = await _dialog.ShowAsync(SelectedPerson);

            if (result == ContentDialogResult.Primary)
            {
                if (SelectedPerson.IsFavorite == false)
                    Persons.Remove(SelectedPerson);
            }
        }
    }
}
