using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsWorkUWPTemplate10.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public string Email { get; set; }
        public Uri PathToImage { get; set; }
        public bool IsFavorite { get; set; }
    }
}
