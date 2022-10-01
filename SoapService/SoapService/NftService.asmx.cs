using SoapService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml;

namespace SoapService
{
    /// <summary>
    /// Summary description for NftService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    //[System.Web.Script.Services.ScriptService]
    public class NftService : System.Web.Services.WebService
    {
        private readonly List<Nft> nfts = new List<Nft>()
            {
                new Nft(
                    "CryptoPunk #4156",
                    "https://www.nft-stats.com/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/4156",
                    DateTime.Now,
                    15000,
                    new Collection(
                        "CryptoPunks",
                        "https://www.nft-stats.com/collection/cryptopunks")),
                new Nft(
                    "Right-click and Save As guy",
                    "https://www.nft-stats.com/asset/0x41a322b28d0ff354040e2cbc676f0320d8c8850d/1154",
                    DateTime.Now,
                    7000,
                    new Collection(
                        "SuperRare",
                        "https://www.nft-stats.com/collection/superrare")),
                new Nft(
                    "Ross Ulbricht Genesis Collection",
                    "https://www.nft-stats.com/asset/0xb932a70a57673d89f4acffbe830e8ed7f75fb9e0/30841",
                    DateTime.Now,
                    5000000,
                    new Collection(
                        "SuperRare",
                        "https://www.nft-stats.com/collection/superrare")),
                new Nft(
                    "CryptoPunk #2964",
                    "https://www.nft-stats.com/asset/0xb47e3cd837ddf8e4c57f05d70ab865de6e193bbb/2964",
                    DateTime.Now,
                    9000,
                    new Collection(
                        "CryptoPunks",
                        "https://www.nft-stats.com/collection/cryptopunks")),
                new Nft(
                    "Bored Ape Yacht Club",
                    "https://www.nft-stats.com/asset/0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d/9452",
                    DateTime.Now,
                    7650,
                    new Collection(
                        "Bored Ape Yacht Club",
                        "https://www.nft-stats.com/collection/boredapeyachtclub"))
        };
        private readonly string filePath = "C:\\Users\\Marta\\Desktop\\New folder\\nfts.xml";

        [WebMethod]
        public List<Nft> FilterNfts(string collectionName)
        {
            // Create XmlDocument with nfts
            XmlDocument document = new XmlDocument();
            XPathNavigator navigator = document.CreateNavigator();
            using (XmlWriter writer = navigator.AppendChild())
            {
                XmlSerializer serializer = new XmlSerializer(nfts.GetType());
                serializer.Serialize(writer, nfts);
            }

            // Save to file
            document.Save(filePath);

            // Filter with XPath  
            XmlNodeList filteredNodes = document.SelectNodes($"./ArrayOfNft/Nft[Collection/Name=\"{collectionName}\"]");

            List<Nft> filteredNfts = filteredNodes.Cast<XmlNode>()
                                .Select(node => Common.Extensions.ConvertNode<Nft>(node))
                               .ToList();

            return filteredNfts;
        }

        [WebMethod]
        public List<Nft> GetAllNfts()
        {
            XmlDocument document = new XmlDocument();
            XPathNavigator navigator = document.CreateNavigator();
            using (XmlWriter writer = navigator.AppendChild())
            {
                XmlSerializer serializer = new XmlSerializer(nfts.GetType());
                serializer.Serialize(writer, nfts);
            }

            return document.ChildNodes[0].ChildNodes
                .Cast<XmlNode>()
                .Select(node => Common.Extensions.ConvertNode<Nft>(node))
                .ToList();
        }
    }
}
