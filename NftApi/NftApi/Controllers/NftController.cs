using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NftApi.Models;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml;
using System;
using System.Xml.Linq;
using Commons.Xml.Relaxng;

namespace NftApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private readonly List<Nft> Nfts;

        public NftController(List<Nft> nfts)
        {
            Nfts = nfts;
        }

        [HttpGet]
        public List<Nft> Get()
        {
            return Nfts;
        }

        [HttpPost("ValidateXsd")]
        [Consumes("application/xml")] 
        public string ValidateXsd([FromBody] XElement xml)
        {
            try
            {
                XmlTextReader schemaReader = new("nft.xsd");
                XmlSchema? schema = XmlSchema.Read(schemaReader, null);

                if (schema == null)
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return "Internal server error";
                }

                XmlSchemaSet xmlSchemaSet = new();
                xmlSchemaSet.Add(schema);

                XDocument doc = new(xml);     
                System.Xml.Schema.Extensions.Validate(doc, xmlSchemaSet, null);

                Nft nft = Common.Extensions.FromXElement<Nft>(xml);
                Nfts.Add(nft);

                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
                return "Xsd validation succeeded";
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "Xsd validation failed with message: " + ex.Message;
            }
        }

        [HttpPost("ValidateRng")]
        [Consumes("application/xml")]
        public string ValidateRng([FromBody] XElement xml)
        {
            XmlDocument document = Common.Extensions.ToXmlDocument(xml);

            // Create XmlReaders of document and nft.rng file
            XmlNodeReader nftXmlReader = new(document);
            XmlTextReader nftRngReader = new("nft.rng");

            // Validate
            using RelaxngValidatingReader reader = new(nftXmlReader, nftRngReader);
            try
            {
                while (!reader.EOF)
                {
                    reader.Read();
                }

                Nft nft = Common.Extensions.FromXElement<Nft>(xml);
                Nfts.Add(nft);

                HttpContext.Response.StatusCode = StatusCodes.Status201Created;
                return "Rng validation succeeded";
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return "Rng validation failed with message: " + ex.Message;
            }
        }
    }
}
