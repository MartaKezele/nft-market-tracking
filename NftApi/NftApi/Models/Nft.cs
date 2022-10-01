using System.Runtime.Serialization;

namespace NftApi.Models
{
    [DataContract(Namespace = "")]
    public class Nft
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public string Url { get; set; }

        [DataMember(Order = 2, IsRequired = true)]
        public DateTime DateTimeSold { get; set; }

        [DataMember(Order = 3, IsRequired = true)]
        public double PriceUsd { get; set; }

        [DataMember(Order = 4, IsRequired = true)]
        public Collection Collection { get; set; }

        public Nft(string name, string url, DateTime dateTimeSold, double priceUsd, Collection collection)
        {
            Name = name;
            Url = url;
            DateTimeSold = dateTimeSold;
            PriceUsd = priceUsd;
            Collection = collection;
        }

        public Nft()
        {
        }
    }
}
