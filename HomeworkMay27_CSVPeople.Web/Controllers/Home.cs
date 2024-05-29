using CsvHelper.Configuration;
using CsvHelper;
using Faker;
using HomeworkMay27_CSVPeople.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using HomeworkMay27_CSVPeople.Web.Models;

namespace HomeworkMay27_CSVPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Home : ControllerBase
    {
        private readonly string _connectionString;
        public Home(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet("Generate")]
        public IActionResult Generate(int amount)
        {
            var client = new HttpClient();
            var list = Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(18, 95),
                Address = Address.StreetAddress() + " " + Address.StreetName() + " " + Address.StreetSuffix(),
                Email = Internet.Email()
            }).ToList();
            var listCsv = GenerateCsv(list);
            return File(Encoding.UTF8.GetBytes(listCsv), "application/octet-stream", "people.csv");
        }
        [HttpPost("Upload")]
        public void Upload(UploadModel m)
        {
            var listBytes = Convert.FromBase64String(m.Base64.Substring(m.Base64.IndexOf(',') + 1));
            var people = GetFromCsv(listBytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }
        [HttpGet("GetPeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }
        [HttpPost("Delete")]
        public void Delete()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }
       private static string GenerateCsv(List<Person> people)
        {
            var writer = new StringWriter();
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(people);

            return writer.ToString();
        }
        private static List<Person> GetFromCsv(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Person>().ToList();
        }
    }
}
