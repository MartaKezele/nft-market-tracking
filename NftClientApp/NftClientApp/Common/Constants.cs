namespace NftClientApp.Common
{
    public static class Constants
    {
        public const string NFT_API_ENDPOINT_BASE = "http://localhost:7002/Api";
        public const string JAXB_SERVICE_ENDPOINT = "http://localhost:8080/JaxbService";

        public const string VALIDATE_XSD_ENDPOINT = NFT_API_ENDPOINT_BASE + "/Nft/ValidateXsd";
        public const string VALIDATE_RNG_ENDPOINT = NFT_API_ENDPOINT_BASE + "/Nft/ValidateRng";
        public const string GET_NFTS_ENDPOINT = NFT_API_ENDPOINT_BASE + "/Nft";

        public const string VALIDATE_JAXB_ENDPOINT = JAXB_SERVICE_ENDPOINT + "/NftsValidation";
        public const string CITY_TEMP_ENDPOINT = JAXB_SERVICE_ENDPOINT + "/CityTemp";
    }
}
