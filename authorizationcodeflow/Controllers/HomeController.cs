using System.Threading.Tasks;
using authorizationcodeflow.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace authorizationcodeflow.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Discovery(DiscoveryModel model)
        {
            var discoveryClient = new DiscoveryClient(model.Url);
            var doc = await discoveryClient.GetAsync();
            if (doc.IsError)
            {
                return Error(doc.Error);
            }
            return View(doc);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error(string error = "")
        {
            return View("Error", error);
        }
    }
}
