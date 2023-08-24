using System;
using System.Collections.Generic;
using System.Net;
using ChamaAe.Servico.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel().ConfigureKestrel(options =>
{
    options.ListenAnyIP(8182);
});

// Add services to the container.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("5.183.8.134"));
    options.KnownProxies.Add(IPAddress.Parse("93.188.164.166"));
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Services Chama Ae",
        Version = "v1",
        Description = "Api Rest criada para consumo de serviços da Chama Ae Solutions.",
        Contact = new OpenApiContact
        {
            Name = "Sistema de Informação - Toledo Centro Universitário",
            Url = new Uri("https://toledoprudente.edu.br/")
        }
    });
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddConfiguration(builder.Configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policyBuilder =>
    {
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseForwardedHeaders();

app.UseCors("default");

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
    if (!app.Environment.IsDevelopment())
    {
        c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/api/chamae" } };
        });
    }
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("swagger/v1/swagger.json", "Chama Ae Services");
    c.DocumentTitle = "ChamaAe Services";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();