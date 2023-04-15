using Microsoft.AspNetCore.Mvc;
using RareHomeTest.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace RareHomeTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public async Task<ActionResult> Index()
        {
            // API Key
            string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

            // Create a HttpClient object
            using (HttpClient client = new HttpClient())
            {
                // Send GET Request to api url and retrieve JSON response
                string json = await client.GetStringAsync(url);

                // Deserialize the JSON response into a workable C# object
                var data = JsonConvert.DeserializeObject<EmployeeModel[]>(json);

                // Calcualte total time worked and sort employees by it

                //var employees = data.GroupBy(x => x.EmployeeName)     // Takes everything into account
                //var employees = data.Where(x => x.DeletedOn == null).GroupBy(x => x.EmployeeName)     //Only counts the entries that haven't been deleted

                var employees = data.Where(x => x.DeletedOn == null && x.EmployeeName != null)      // Filter out the entries that have a non-null DeletedOn value or a null EmployeeName
                                .GroupBy(x => x.EmployeeName)
                                .Select(g => new Employee
                                {
                                    Name = g.Key,
                                    TotalTimeWorked = g.Sum(x => (x.EndTimeUtc - x.StarTimeUtc).TotalHours)
                                })
                                   .OrderByDescending(e => e.TotalTimeWorked)
                                   .ToList();
                return View(employees);
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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