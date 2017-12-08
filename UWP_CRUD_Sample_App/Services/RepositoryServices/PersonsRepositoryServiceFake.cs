using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionsWorkUWPTemplate10.Models;
using GalaSoft.MvvmLight.Messaging;
using CollectionsWorkUWPTemplate10.Messages;

namespace CollectionsWorkUWPTemplate10.Services.RepositoryServices
{
    class PersonsRepositoryServiceFake : IRepositoryService<Person>
    {
        private List<Person> _persons;

        public async Task<List<Person>> GetAllAsync()
        {
            return _persons = _persons ?? await ReadPersonsAsync();
        }

        public async Task<Person> GetByIdAsync(string id)
        {
            return await Task.Run(() =>
            (from p in _persons where p.Id.Equals(id) select p).First());
        }

        public async Task<List<Person>> GetAllFavoritesASync()
        {
            return await Task.Run(() =>
                    (from person in _persons where person.IsFavorite.Equals(true) select person).ToList<Person>());

        }

        public async Task AddAsync(Person person)
        {
            if (_persons == null) _persons = await ReadPersonsAsync();

            _persons.Add(person);

            await WritePersonsAsync();

            var message = new PersonsChangedMessage() { OperationType = CRUD.Create, ExeptionMessage = string.Empty };
            Messenger.Default.Send<PersonsChangedMessage>(message);
        }

        public async Task DeleteAsync(string id)
        {
            _persons.Remove(_persons.Find(x => x.Id == id));

            await WritePersonsAsync();

            var message = new PersonsChangedMessage() { OperationType = CRUD.Delete, ExeptionMessage = string.Empty };
            Messenger.Default.Send<PersonsChangedMessage>(message);
        }

        public async Task UpdateAsync()
        {
            await Task.Run(null);

            var message = new PersonsChangedMessage() { OperationType = CRUD.Update, ExeptionMessage = string.Empty };
            Messenger.Default.Send<PersonsChangedMessage>(message);
        }

        public async Task UpdateAsync(Person person)
        {
            //var _person = _persons.Where((Person p) => p.Id == person.Id).FirstOrDefault();

            var _person = _persons.FirstOrDefault(p => p.Id == person.Id);

            _persons.Remove(_person);
            _persons.Add(person);

            await WritePersonsAsync();

            var message = new PersonsChangedMessage() { OperationType = CRUD.Update, ExeptionMessage = string.Empty };
            Messenger.Default.Send<PersonsChangedMessage>(message);

            //await Task.CompletedTask;
        }

        #region Read/Write
        private async Task<List<Person>> ReadPersonsAsync()
        {
            return await Task.Run(() => _persons = new List<Person>
            {
                new Person{Id=Guid.NewGuid().ToString(), Name="Веста",LastName="Буркот",Email="vesta.burkot@mail.com", IsFavorite=true,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage = new Uri("http://100portraits.ru/wp-content/uploads/2017/08/Vesta_100Portraits-04.jpg")},
                new Person{Id=Guid.NewGuid().ToString(), Name="Андрей",LastName="Альтов",Email="andrey.altov@mail.com", IsFavorite=false,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage =new Uri ("http://100portraits.ru/wp-content/uploads/2017/05/Tatyana_Makarova-01.jpg")},
                new Person{Id=Guid.NewGuid().ToString(), Name="Ксения",LastName="Берелет",Email="ksenia.berelet@mail.com", IsFavorite=true,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage = new Uri ("http://100portraits.ru/wp-content/uploads/2017/08/Kseniya_Berelet_09w.jpg")},
                new Person{Id=Guid.NewGuid().ToString(), Name="Маргарита",LastName="Бабкина",Email="margarite.babkina@mail.com", IsFavorite=false,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage =new Uri("http://100portraits.ru/wp-content/uploads/2017/08/Margarita_100Portraits-16.jpg")},
                new Person{Id=Guid.NewGuid().ToString(), Name="Владимир", LastName="Тюрин", Email="vladimir.turin@mail.com", IsFavorite=true,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage =new Uri("http://100portraits.ru/wp-content/uploads/2017/08/Vladimir_100Portraits-06.jpg") },
                new Person{Id=Guid.NewGuid().ToString(), Name="Дарья",LastName="Митичашвили",Email="daria.michiashvili2@mail.com", IsFavorite=true,
                    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                    PathToImage =new Uri("http://100portraits.ru/wp-content/uploads/2017/08/Darya_Mitichashvili_06w.jpg")}
                //new Person{Name="Name 3",LastName="LastName 3",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra dictum nibh vel ornare. Donec sem urna, rhoncus sed cursus ac, aliquet at nisl. Pellentesque cursus et lacus vel porta. Morbi iaculis efficitur volutpat. Curabitur sit amet cursus nisl, ac suscipit mauris. Nulla a tellus a odio tincidunt maximus. Maecenas non eros lacus. Donec aliquam libero nec ex ullamcorper, in lobortis nibh dapibus. Mauris vehicula, tellus quis congue tincidunt, neque massa auctor ante, ut laoreet felis nisi id tellus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nunc sed dapibus quam, in viverra arcu. Vivamus ut metus non magna viverra porttitor a ultrices dui.",
                //    PathToImage =null},
                //new Person{Name="Name 4",LastName="LastName 4",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null},
                //new Person{Name="Name 5",LastName="LastName 5",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null}, new Person{Name="Name 1",LastName="LastName 1",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null},
                //new Person{Name="Name 2",LastName="LastName 2",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null},
                //new Person{Name="Name 3",LastName="LastName 3",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null},
                //new Person{Name="Name 4",LastName="LastName 4",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null},
                //new Person{Name="Name 5",LastName="LastName 5",
                //    Notes ="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor",
                //    PathToImage =null}
            });
        }

        private async Task WritePersonsAsync()
        {
            await Task.CompletedTask;
        }

        #endregion
    }
}
