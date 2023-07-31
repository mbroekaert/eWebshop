using Application;
using Application.Auth0Users.Services;
using Application.Cart.Services;
using Application.Common.Interfaces;
using Application.Customers.Services;
using CustomerWebsite.Middlewares;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Website.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddSession();
builder.Services.AddSingleton(sp => new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7276/Api/")
});

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IAuth0UserService, Auth0UserService>();
builder.Services.AddSingleton<IBillingAddressService, BillingAddressService>();
builder.Services.AddSingleton<IShippingAddressService, ShippingAddressService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddMemoryCache();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

})
.AddCookie()
.AddOpenIdConnect("Auth0", options =>
{
    // Define authority
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    // Authorization Code
    options.ResponseType = OpenIdConnectResponseType.Code;
    // Add scopes
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("read:messages");
    options.Scope.Add("write:messages");
    options.Scope.Add("read:users");
    options.Scope.Add("write:users");
    options.Scope.Add("create:users");
    // Define callback
    options.CallbackPath = new PathString("/callback");
    // Issuer
    options.ClaimsIssuer = "Auth0";
    options.SaveTokens = true;
    // Add events for logout
    options.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProviderForSignOut = (context) =>
        {
            var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Auth0:ClientId"]}";
            var postLogoutUri = context.Properties.RedirectUri;
            if (!string.IsNullOrEmpty(postLogoutUri))
            {
                if (postLogoutUri.StartsWith("/"))
                {
                    var request = context.Request;
                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                }
                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            }
            context.Response.Redirect(logoutUri);
            context.HandleResponse();
            return Task.CompletedTask;
        },
        OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.SetParameter("audience", builder.Configuration["Auth0:Audience"]);
            return Task.FromResult(0);
        },
        OnMessageReceived = context =>
        {
            if (context.ProtocolMessage.Error == "access_denied")
            {
                context.HandleResponse();
                context.Response.Redirect("/Home/AccessDenied");
            }
            return Task.FromResult(0);
        }
    };
});



builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<AuthMiddleware>();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
