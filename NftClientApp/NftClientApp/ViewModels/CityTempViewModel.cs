using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NftClientApp.ViewModels
{
    public class CityTempViewModel
    {
        [Display(Name = "City name")]
        public string Name { get; set; }
    }
}
