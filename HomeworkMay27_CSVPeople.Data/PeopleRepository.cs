using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkMay27_CSVPeople.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPeople(List<Person> people)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }
        public void DeleteAll()
        {
            using var context = new PeopleDataContext(_connectionString);
            context.Database.ExecuteSql($"truncate table People");
        }
    }
}
