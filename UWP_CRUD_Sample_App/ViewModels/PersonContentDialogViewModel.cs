using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using CollectionsWorkUWPTemplate10.Models;
using Windows.UI.Xaml.Navigation;
using CollectionsWorkUWPTemplate10.Messages;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Controls;

namespace CollectionsWorkUWPTemplate10.ViewModels
{
    public class PersonContentDialogViewModel : ViewModelBase
    {
        public PersonContentDialogViewModel()
            {
            }
        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        #region Bindable properties

        Person _CurrentPerson;
        public Person CurrentPerson
        {
            get { return _CurrentPerson; }
            set { Set(ref _CurrentPerson, value); }
        }

        public string GetFullName
        {
            get { return $"{_CurrentPerson.Name} {_CurrentPerson.LastName}"; }
        }

        Symbol _FavoriteSymbol = Symbol.Favorite;
        public Symbol FavoriteSymbol
        {
            get { return _FavoriteSymbol; }
            set { Set(ref _FavoriteSymbol, value); }
        }
        #endregion

        #region MakeFaforiteCommand
        DelegateCommand _MakeFaforiteCommand;

        public DelegateCommand MakeFaforiteCommand
        {
            get { return _MakeFaforiteCommand ?? new DelegateCommand(MakeFavoriteExecute); }
        }
        private void MakeFavoriteExecute()
        {
            if (CurrentPerson != null)
                FavoriteSymbol = (CurrentPerson.IsFavorite == true) ? Symbol.UnFavorite : Symbol.Favorite;
        }
        #endregion

        #region SaveCommand
        DelegateCommand _SaveCommand;
        public DelegateCommand SaveCommand
        {
            get { return _SaveCommand ?? new DelegateCommand(Save, CanSave); }
        }

        private bool CanSave()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            throw new NotImplementedException();
        }
        #endregion
        public void Send() ///todo: есть ли необходимость в этом сообщении?
        {
            var message = new PersonFavoriteChangedMessage()
            {
                AffectedPersonId = CurrentPerson.Id,
                IsNowFavorite=CurrentPerson.IsFavorite
            };
            Messenger.Default.Send<PersonFavoriteChangedMessage>(message);
        }
    }
}
