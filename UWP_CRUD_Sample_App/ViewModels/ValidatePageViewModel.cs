using CollectionsWorkUWPTemplate10.Models;
using CollectionsWorkUWPTemplate10.ProxyObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace CollectionsWorkUWPTemplate10.ViewModels
{
    public class ValidatePageViewModel : ViewModelBase
    {
        Person PocoPerson;
        public ValidatePageViewModel()
        {

            PocoPerson = new Person()
            {
                Name = "AAAAAAAA",
                LastName = "BBBBBBBBBB"
            };

            PersonProxy person = new PersonProxy(PocoPerson)
            {
                Name = PocoPerson.Name,
                LastName = PocoPerson.LastName,
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
                },
            };
            CurPerson = person;
        }

        PersonProxy _CurPerson;
        public PersonProxy CurPerson
        {
            get { return _CurPerson; }
            set { _CurPerson= value; }
        }
    }
}
