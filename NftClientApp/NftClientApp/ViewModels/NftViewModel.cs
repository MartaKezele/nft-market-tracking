using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NftClientApp.ViewModels
{
    public class NftViewModel
    {
        [Display(Name = "Nft name")]
        public string Name { get; set; }

        [Display(Name = "Nft url")]
        public string Url { get; set; }

        [Display(Name = "Date time sold")]
        public DateTime DateTimeSold { get; set; }

        [Display(Name = "Price (USD)")]
        public double PriceUsd { get; set; }

        [Display(Name = "Collection")]
        public CollectionViewModel Collection { get; set; }
    }
}
