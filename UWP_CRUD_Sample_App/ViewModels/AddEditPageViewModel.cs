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
using CollectionsWorkUWPTemplate10.ProxyObjects;

namespace CollectionsWorkUWPTemplate10.ViewModels
{
    public class AddEditPageViewModel : ViewModelBase
    {
        #region Fields

        IRepositoryService<Person> PersonsRepositary;
        Person currentPerson;
        PersonProxy proxyPerson;
        States currentState;
        enum States { Edit, Add }

        #endregion

        #region Constructor
        public AddEditPageViewModel(IRepositoryService<Person> personsRepository)
        {
            PersonsRepositary = personsRepository;
        }
        #endregion

        #region Page Navigation events

        public async override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            currentPerson = parameter == null ? new Person() { Id = Guid.NewGuid().ToString() } : parameter as Person;

            var temp = new PersonProxy(currentPerson)
            {
                Name = currentPerson.Name,
                LastName = currentPerson.LastName,
                Validator = i =>
                {
                    var u = i as PersonProxy;
                    if (string.IsNullOrEmpty(u.Name))
                    {
                        u.Properties[nameof(u.Name)].Errors.Add("First name is required");
                    }
                    else if (u.Name.Length < 3)
                    {
                        u.Properties[nameof(u.Name)].Errors.Add("First name is less then 3 symbols");
                    }
                    if (string.IsNullOrEmpty(u.LastName))
                    {
                        u.Properties[nameof(u.LastName)].Errors.Add("Last name is required");
                    }
                    if (string.IsNullOrEmpty(u.Email))
                        u.Properties[nameof(u.Email)].Errors.Add("Email is required.");
                    else if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(u.Email))
                        u.Properties[nameof(u.Email)].Errors.Add("A valid Email is required.");
                },
            };
            TempPerson = temp;
            TempPerson.Validate();

            if (parameter == null)
            {
                currentState = States.Add;
                Title = "Addiing new person";
            }
            else if (parameter != null)
            {
                currentState = States.Edit;
                Title = $"Editting {TempPerson.FullName}";
            }
            await Task.CompletedTask;
        }
        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            return base.OnNavigatingFromAsync(args);
        }

        #endregion

        #region Bindable properties
        public string Title { get; set; }

        public PersonProxy TempPerson
        {
            get { return proxyPerson; }
            set { Set(ref proxyPerson, value); }
        }
        #endregion

        #region Navigation tasks

        public async void GotoBackUnSaved() =>
            await NavigationService.NavigateAsync(typeof(Views.MasterDetailPage));

        public async void GotoBackSaved()
        {
            currentPerson.Name = TempPerson.Name;
            currentPerson.LastName = TempPerson.LastName;
            currentPerson.Notes = TempPerson.Notes;
            currentPerson.Email = TempPerson.Email;
            currentPerson.PathToImage = TempPerson.PathToImage;

            if (currentState == States.Add)
            {
                await PersonsRepositary.AddAsync(currentPerson);
            }
            if (currentState == States.Edit)
            {
                await PersonsRepositary.UpdateAsync(currentPerson);
            }
            await NavigationService.NavigateAsync(typeof(Views.MasterDetailPage));
        }

        #endregion
    }
}
