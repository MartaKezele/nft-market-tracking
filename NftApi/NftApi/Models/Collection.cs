using System.Runtime.Serialization;

namespace NftApi.Models
{
    [DataContract(Namespace = "")]
    public class Collection
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public string Url { get; set; }

        public Collection(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public Collection()
        {
        }
    }
}
