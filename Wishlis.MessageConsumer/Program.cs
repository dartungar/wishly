// See https://aka.ms/new-console-template for more information

using Amazon.SQS;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wishlis.Application.Mappers;
using Wishlis.Application.Services;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.LiteDB;
using Wishlis.MessageConsumer;

const string AWSconfigSection = "AWS";
var builder = Host.CreateApplicationBuilder(args);
var config = Configuration.Create();

// services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IWishlistItemService, WishlistItemService>();

// infrastructure
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection(nameof(LiteDbOptions)));
builder.Services.AddScoped<ILiteDbContext, LiteDbContext>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);
    
    x.UsingAmazonSqs((context, cfg) =>
    {
        var region = config.GetSection("AWS")["region"];
        cfg.Host(region, _ => {});

        cfg.ReceiveEndpoint("wishlis-queue", endpoint => endpoint.ConfigureConsumer<CreatePersonConsumer>(context));

    });
});


using var host = builder.Build();
await host.RunAsync();