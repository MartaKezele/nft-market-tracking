using System;
using System.Runtime.Serialization;

namespace SoapService.Models
{
    [DataContract]
    public class Nft
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
        public string Url { get; set; }

        [DataMember(Order = 2)]
        public DateTime DateTimeSold { get; set; }

        [DataMember(Order = 3)]
        public double PriceUsd { get; set; }

        [DataMember(Order = 4)]
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