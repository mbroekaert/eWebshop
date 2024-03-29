using Api.Filters;
using Application;
using Application.Billing.Services;
using Application.Common.Interfaces;
using Application.Common.PermissionHandler;
using Application.Mappers;
using Application.Worldline.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddSingleton(sp => new HttpClient()
{
    BaseAddress = new Uri("https://mathieubroekaert.eu.auth0.com/api/v2/")
});

builder.Services.AddSingleton<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IBillingService, BillingService>();

builder.Services.AddHttpClient("resource");
builder.Services.AddHttpClient("resource").AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(5));


builder.Services.AddControllers();

// Add CORS Policy - needed for ajax call on order confirmation page
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7276") 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = domain;
    options.SaveToken = true;
    options.Audience = builder.Configuration["Auth0:Audience"];
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
    options.AddPolicy("write:messages", policy => policy.Requirements.Add(new HasScopeRequirement("write:messages", domain)));
    options.AddPolicy("read:users", policy => policy.Requirements.Add(new HasScopeRequirement("read:users", domain)));
    options.AddPolicy("write:users", policy => policy.Requirements.Add(new HasScopeRequirement("write:users", domain)));
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddSession();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

//builder.Services.AddAutoMapper(typeof(CategoryProfile));
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new CategoryProfile());
    mc.AddProfile(new UserProfile());
});

builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ApiExceptionFilterAttribute());
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
    options.ReturnHttpNotAcceptable = true;
}).AddFluentValidation();

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();
app.Run();
