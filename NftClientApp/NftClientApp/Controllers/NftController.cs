using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NftClientApp.Models;
using NftClientApp.ViewModels;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Collection = NftClientApp.Models.Collection;

namespace NftClientApp.Controllers
{
    public class NftController : Controller
    {
        [HttpGet]
        [ActionName("ValidateXsd")]
        public IActionResult ValidateXsd_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ValidateXsd")]
        public IActionResult ValidateXsd_Post(NftViewModel nftViewModel)
        {
            var nft = CreateNft(nftViewModel);
            var data = ConvertNftToByteArray(nft);

            var request = Common.Extensions.MakeRequest(Common.Constants.VALIDATE_XSD_ENDPOINT, "POST", data);
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
                var stream = response.GetResponseStream();
                DataContractSerializer serializer = new(typeof(string));
                string? responseString = serializer.ReadObject(stream) as string;
                ViewBag.Response = responseString;
            }
            else
            {
                ViewBag.Response = "Server not available";
            }

            return View();
        }

        [HttpGet]
        [ActionName("ValidateRng")]
        public IActionResult ValidateRng_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ValidateRng")]
        public IActionResult ValidateRng_Post(NftViewModel nftViewModel)
        {
            var nft = CreateNft(nftViewModel);
            var data = ConvertNftToByteArray(nft);

            var request = Common.Extensions.MakeRequest(Common.Constants.VALIDATE_RNG_ENDPOINT, "POST", data);
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
                var stream = response.GetResponseStream();
                DataContractSerializer serializer = new(typeof(string));
                string? responseString = serializer.ReadObject(stream) as string;
                ViewBag.Response = responseString;
            }
            else
            {
                ViewBag.Response = "Server not available";
            }

            return View();
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List_Get()
        {
            HttpWebRequest request = Common.Extensions.MakeRequest(Common.Constants.GET_NFTS_ENDPOINT, "GET", null);
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

            List<NftViewModel> model = new();
            if (response != null)
            {
                var data = response.GetResponseStream();
                DataContractSerializer serializer = new(typeof(List<Nft>));
                List<Nft>? nfts = serializer.ReadObject(data) as List<Nft>;
                nfts?.ForEach(nft =>
                {
                    model.Add(CreateNftViewModel(nft));
                });
                ViewBag.Success = true;
            } else
            {
                ViewBag.Success = false;
                ViewBag.Response = "Server not available";
            }

            return View(model);
        }

        [HttpGet]
        [ActionName("FilterNfts")]
        public IActionResult FilterNfts_Get()
        {
            FilterNftsViewModel model = new();
            try
            {
                ServiceReference1.NftServiceSoapClient service = new(ServiceReference1.NftServiceSoapClient.EndpointConfiguration.NftServiceSoap);
                ServiceReference1.GetAllNftsResponse response = service.GetAllNftsAsync().Result;
                ServiceReference1.Nft[] nfts = response.Body.GetAllNftsResult;
                List<NftViewModel> nftsModel = new();
                foreach (var nft in nfts)
                {
                    nftsModel.Add(new NftViewModel()
                    {
                        Name = nft.Name,
                        DateTimeSold = nft.DateTimeSold,
                        PriceUsd = nft.PriceUsd,
                        Url = nft.Url,
                        Collection = new()
                        {
                            Name = nft.Collection.Name,
                            Url = nft.Collection.Url,
                        }
                    });
                }
                model.Nfts = nftsModel;
            }
            catch (AggregateException)
            {
                ViewBag.Success = false;
                ViewBag.Response = "Server not available";
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("FilterNfts")]
        public IActionResult FilterNfts_Post(FilterNftsViewModel viewModel)
        {
            FilterNftsViewModel model = new();
            try
            {
                ServiceReference1.NftServiceSoapClient service = new(ServiceReference1.NftServiceSoapClient.EndpointConfiguration.NftServiceSoap);
                ServiceReference1.FilterNftsResponse response = service.FilterNftsAsync(viewModel.Name).Result;
                ServiceReference1.Nft[] nfts = response.Body.FilterNftsResult;
                List<NftViewModel> nftsModel = new();
                foreach (var nft in nfts)
                {
                    nftsModel.Add(new NftViewModel()
                    {
                        Name = nft.Name,
                        DateTimeSold = nft.DateTimeSold,
                        PriceUsd = nft.PriceUsd,
                        Url = nft.Url,
                        Collection = new()
                        {
                            Name = nft.Collection.Name,
                            Url = nft.Collection.Url,
                        }
                    });
                }

                model.Nfts = nftsModel;
                model.Name = viewModel.Name;
            }
            catch (AggregateException)
            {
                ViewBag.Success = false;
                ViewBag.Response = "Server not available";
            }

            return View(model);
        }

        [HttpGet]
        [ActionName("ValidateJaxb")]
        public IActionResult ValidateJaxb_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ValidateJaxb")]
        public IActionResult ValidateJaxb_Post()
        {
            var request = Common.Extensions.MakeRequest(Common.Constants.VALIDATE_JAXB_ENDPOINT, "GET", null);
            HttpWebResponse? response;

            try
            {
                response = request.GetResponse() as HttpWebResponse;
                if(response != null)
                {
                    using Stream stream = response.GetResponseStream();
                    StreamReader reader = new(stream, Encoding.UTF8);
                    var responseString = reader.ReadToEnd();
                    ViewBag.Success = (responseString == "Validation successful\r\n");
                    ViewBag.Response = responseString;
                }
            } 
            catch
            {
                ViewBag.Response = "Server not available";
            }

            return View();
        }

        [HttpGet]
        [ActionName("TopNftsThisWeek")]
        public async Task<IActionResult> TopNftsThisWeek_GetAsync()
        {
            List<NftJson> nfts = new();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://top-nft-sales.p.rapidapi.com/sales/7d"),
                Headers =
                {
                    { "X-RapidAPI-Key", "replace_with_your_key" },
                    { "X-RapidAPI-Host", "replace_with_your_host" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                nfts = JsonConvert.DeserializeObject<List<NftJson>>(body);
            }
            return View(nfts);
        }

        #region Helper Methods

        private static byte[] ConvertNftToByteArray(Nft nft)
        {
            DataContractSerializer nftSerializer = new(typeof(Nft));
            MemoryStream stream = new();
            XmlWriter writer = XmlWriter.Create(stream);
            nftSerializer.WriteObject(writer, nft);
            writer.Close();

            return Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(stream.ToArray()));
        }

        private static Nft CreateNft(NftViewModel nftViewModel)
        {
            return new()
            {
                Name = nftViewModel.Name ?? "",
                Url = nftViewModel.Url ?? "",
                DateTimeSold = nftViewModel.DateTimeSold,
                PriceUsd = nftViewModel.PriceUsd,
                Collection = new Collection
                {
                    Name = nftViewModel.Collection.Name ?? "",
                    Url = nftViewModel.Collection.Url ?? ""
                }
            };
        }

        private static NftViewModel CreateNftViewModel(Nft nft)
        {
            return new()
            {
                Name = nft.Name,
                DateTimeSold = nft.DateTimeSold,
                PriceUsd = nft.PriceUsd,
                Url = nft.Url,
                Collection = new()
                {
                    Name = nft.Collection.Name,
                    Url = nft.Collection.Url,
                }
            };
        }
        #endregion Helper Methods
    }
}
