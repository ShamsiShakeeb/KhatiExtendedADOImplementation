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

        public async Task<IActionResult> Index()
        {
            var insert = await _personContext.SqlWriteAsync("Insert into Student(Name) values (@Name)", 
                new Dictionary<string, object>() { { "@Name", "Washiq Anwar Shamsi" }, });

            List<Student> students = new List<Student>() 
            { 
                new Student() { Name = "The Rock"},
                new Student() { Name = "Brock Lesner"}
            };

            await _personContext.SqlBulkUploadAsync(students,"Student");

            var readAll = await _personContext.SqlReadAsync<List<Student>>("select * from Student");

            var readFilter = await _personContext.SqlReadAsync<List<Student>>("select * from Student where Name = @Name",
                new Dictionary<string, object>() { { "@Name", "Brock Lesner" }, });

            var firstOrDefault = await _personContext.SqlReadScalerModelAsync<Student>("select Top 1 * from Student");

            var readScalerValue = await _personContext.SqlReadScalerValueAsync<int>("select count(*) from Student"); 

            return Ok(new 
            {
                readAll=readAll.Data,
                readFilter = readFilter.Data,
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