using System.Runtime.Serialization;

namespace SoapService.Models
{
    [DataContract]
    public class Collection
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 1)]
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