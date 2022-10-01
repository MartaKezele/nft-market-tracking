using Microsoft.AspNetCore.Mvc;
using NftClientApp.ViewModels;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace NftClientApp.Controllers
{
    public class WeatherController : Controller
    {
        [HttpGet]
        [ActionName("CityTemp")]
        public IActionResult CityTemp_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("CityTemp")]
        public IActionResult CityTemp_Post(CityTempViewModel viewModel)
        {
            var endpoint = Common.Constants.CITY_TEMP_ENDPOINT + $"?city={viewModel.Name}";
            var request = Common.Extensions.MakeRequest(endpoint, "GET", null);
            HttpWebResponse? response;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
                ViewBag.Success = true;
            }
            catch (WebException e)
            {
                response = e.Response as HttpWebResponse;
                ViewBag.Success = false;
            }

            if (response != null)
            {
                using Stream stream = response.GetResponseStream();
                StreamReader reader = new(stream, Encoding.UTF8);
                ViewBag.Response = reader.ReadToEnd();
            }
            else
            {
                ViewBag.Response = "Server not available";
            }
            return View();
        }
    }
}
