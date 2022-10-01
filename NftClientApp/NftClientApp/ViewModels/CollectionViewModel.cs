using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NftClientApp.ViewModels
{
    public class CollectionViewModel
    {
        [Display(Name = "Collection name")]
        public string Name { get; set; }

        [Display(Name = "Collection url")]
        public string Url { get; set; }
    }
}
