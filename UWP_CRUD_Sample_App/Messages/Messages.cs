using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace CollectionsWorkUWPTemplate10.Messages
{
    public enum CRUD { Create, Read, Update, Delete }

    public class PersonsChangedMessage : MessageBase
    {
        public string ExeptionMessage { get; set; }

        public CRUD OperationType { get; set; }
    }
    public class PersonFavoriteChangedMessage : MessageBase
    {
        public bool IsNowFavorite { get; set; }
        public string AffectedPersonId { get; set; }
    }
}
