using NftApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddSingleton(getNfts());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

List<Nft> getNfts()
{
    return new()
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
    };
}
