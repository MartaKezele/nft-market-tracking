using Newtonsoft.Json;

namespace NftClientApp.Models
{
    public class NftJson
    {
        [JsonProperty("nft_name")]
        public string NftName;

        [JsonProperty("nft_url")]
        public string NftUrl;

        [JsonProperty("collection")]
        public string CollectionName;

        [JsonProperty("collection_url")]
        public string CollectionUrl;

        [JsonProperty("date")]
        public string Date;

        [JsonProperty("price")]
        public string Price;
    }
}
