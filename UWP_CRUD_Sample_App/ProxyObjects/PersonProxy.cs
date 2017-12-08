using CollectionsWorkUWPTemplate10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Validation;
using Template10.Interfaces.Validation;
using System.ComponentModel;

namespace CollectionsWorkUWPTemplate10.ProxyObjects
{
    public class PersonProxy : ValidatableModelBase
    {
        #region Constructor

        public PersonProxy(Person person)
        {
            Name = person.Name;
            LastName = person.LastName;
            Notes = person.Notes;
            Email = person.Email;
            PathToImage = person.PathToImage;
            IsFavorite = person.IsFavorite;
        }
        #endregion

        #region Initial properties

        public string Name
        {
            get { return Read<string>(); }
            set { Write(value); }
        }

        public string LastName
        {
            get { return Read<string>(); }
            set { Write(value); }
        }

        public string Email
        {
            get { return Read<string>(); }
            set { Write(value); }
        }

        private string _Notes;
        public string Notes
        {
            get { return Read<string>(); }
            set { Write(value); }
        }

        private Uri _PathToimage;
        public Uri PathToImage
        {
            get { return _PathToimage; }
            set { _PathToimage = value; }
        }

        private bool _IsFavorite;
        public bool IsFavorite
        {
            get { return _IsFavorite; }
            set { _IsFavorite = value; }
        }

        #endregion

        #region Calculated properties

        public string FullName
        {
            get { return $"{Name} {LastName}"; }
        }

        public Uri EmailUri
        {
            get { return new Uri($"mailto:{this.Email}"); }
        }

        #endregion

    }
}
