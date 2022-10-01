using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NftClientApp.ViewModels
{
    public class FilterNftsViewModel
    {
        public List<NftViewModel> Nfts { get; set; }

        [Display(Name = "Collection name")]
        public string Name { get; set; }
    }
}
