using KhatiExtendedADONugetImplementation.ADOContext;
using KhatiExtendedADONugetImplementation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KhatiExtendedADONugetImplementation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonContext _personContext;
        public HomeController(ILogger<HomeController> logger, IPersonContext personContext)
        {
            _logger = logger;
            _personContext = personContext;
        }

        public IActionResult Index()
        {
            var insert = _personContext.SqlWrite(string.Format("Insert into Student(Name) values('{0}')","John Cena"));

            List<Student> students = new List<Student>() 
            { 
                new Student() { Name = "The Rock"},
                new Student() { Name = "Brock Lesner"}
            };

            _personContext.SqlBulkUpload(students,"Student");

            var readAll = _personContext.SqlRead<List<Student>>("select * from Student");

            var firstOrDefault = _personContext.SqlReadScalerModel<Student>("select Top 1 * from Student");

            var readScalerValue = _personContext.SqlReadScalerValue<int>("select count(*) from Student"); 

            return Ok(new 
            {
                readAll=readAll.Data,
                firstOrDefault=firstOrDefault.Data,
                readScalerValue=readScalerValue.Data
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}