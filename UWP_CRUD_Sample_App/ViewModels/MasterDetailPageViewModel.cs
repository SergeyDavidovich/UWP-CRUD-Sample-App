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
using Microsoft.Toolkit.Uwp.UI.Controls;

namespace CollectionsWorkUWPTemplate10.ViewModels
{
    public class MasterDetailPageViewModel : ViewModelBase
    {
        #region Fields

        ObservableCollection<Person> _persons;
        IRepositoryService<Person> _personsRepositary;
        Person _selectedPerson;
        DelegateCommand _deletePersonCommand;
        DelegateCommand _editPersonCommand;
        DelegateCommand _addPersonCommand;

        #endregion

        #region Constructor
        public MasterDetailPageViewModel(IRepositoryService<Person> personsRepository)
        {
            _personsRepositary = personsRepository;
            _deletePersonCommand = new DelegateCommand(DeletePersonExecute, CanDeletePerson);
            _editPersonCommand = new DelegateCommand(EditPersonExecute, CanEditPerson);
            _MakeFaforiteCommand = new DelegateCommand(MakeFavoriteExecute, CanMakeFavorite);
        }
        #endregion

        #region Page Navigation events
        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            var list = await _personsRepositary.GetAllAsync();
            this.Persons = new ObservableCollection<Person>(list);

            Messenger.Default.Register<PersonsChangedMessage>(this, (message) => HandlePersonsChangedMessage(message));

            await Task.CompletedTask;
        }
        public async override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            Messenger.Default.Unregister<PersonsChangedMessage>(this);
            await Task.CompletedTask;
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
            get { return _selectedPerson; }
            set
            {
                Set(ref _selectedPerson, value);
                DeletePersonCommand.RaiseCanExecuteChanged();
                EditPersonCommand.RaiseCanExecuteChanged();
                MakeFaforiteCommand.RaiseCanExecuteChanged();

                if (SelectedPerson != null)
                    FavoriteSymbol = (SelectedPerson.IsFavorite == true) ? Symbol.Favorite : Symbol.UnFavorite;
            }
        }

        Symbol _FavoriteSymbol = Symbol.Favorite;
        public Symbol FavoriteSymbol
        {
            get { return _FavoriteSymbol; }
            set { Set(ref _FavoriteSymbol, value); }
        }
        #endregion

        #region Navigation tasks

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

        #endregion

        #region Commands

        #region MakeFaforiteCommand
        DelegateCommand _MakeFaforiteCommand;

        public DelegateCommand MakeFaforiteCommand
        {
            get { return _MakeFaforiteCommand ?? new DelegateCommand(MakeFavoriteExecute, CanMakeFavorite); }
        }
        private void MakeFavoriteExecute()
        {
            if (SelectedPerson != null)
                FavoriteSymbol = (SelectedPerson.IsFavorite == true) ? Symbol.UnFavorite : Symbol.Favorite;
            _personsRepositary.UpdateAsync(SelectedPerson);

           
        }
        private bool CanMakeFavorite()
        {
            return this.SelectedPerson == null ? false : true;
        }
        #endregion

        #region DeletePersonCommand
        public DelegateCommand DeletePersonCommand
        {
            get { return _deletePersonCommand ?? new DelegateCommand(DeletePersonExecute, CanDeletePerson); }
        }

        private async void DeletePersonExecute()
        {
            ContentDialog OkCancelDialog = new ContentDialog()
            {
                Title = $"Deleting {SelectedPerson.Name} {SelectedPerson.LastName}!",
                Content = "If agree press OK, otherwise Cancel",
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            ContentDialogResult result = await OkCancelDialog.ShowAsync();
            if (result != ContentDialogResult.Primary) return;

            await _personsRepositary.DeleteAsync(SelectedPerson.Id);
        }
        private bool CanDeletePerson() => this.SelectedPerson == null ? false : true;
        #endregion

        #region EditPersonCommand
        public DelegateCommand EditPersonCommand
        {
            get { return _editPersonCommand ?? new DelegateCommand(EditPersonExecute, CanEditPerson); }
        }
        private void EditPersonExecute()
        {
            NavigationService.Navigate(typeof(Views.AddEditPage), SelectedPerson);
        }
        private bool CanEditPerson() => SelectedPerson == null ? false : true;
        #endregion

        #region AddPersonCommand
        public DelegateCommand AddPersonCommmand
        {
            get { return _addPersonCommand ?? new DelegateCommand(AddPersonExecute); }
        }
        private void AddPersonExecute()
        {
            NavigationService.Navigate(typeof(Views.AddEditPage));
        }
        #endregion

        #endregion

        #region MeesagesHandlers
        private async void HandlePersonsChangedMessage(PersonsChangedMessage message)
        {
            switch (message.OperationType)
            {
                case CRUD.Delete:
                    this.Persons.Remove(this.SelectedPerson);
                    break;
                case CRUD.Update:

                    SelectedPerson.IsFavorite = (SelectedPerson.IsFavorite == true) ? false : true;
                    break;
                default:
                    await new MessageDialog(message.ExeptionMessage).ShowAsync();
                    break;
            }
        }
        #endregion
    }
}
