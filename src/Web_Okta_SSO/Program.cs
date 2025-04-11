using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using Web_Okta_SSO.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Authority = $"{builder.Configuration["Okta:Domain"]}/oauth2/{builder.Configuration["Okta:AuthorizationServerId"]}";///.well-known/oauth-authorization-server";
    options.ClientId = builder.Configuration["Okta:ClientId"];
    options.ClientSecret = builder.Configuration["Okta:ClientSecret"];
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    //options.CallbackPath = "/Home/Index"; //"/signin-oidc";
    //options.CallbackPath = "Home/Callback";
    options.CallbackPath = "/authorization-code/callback";
    options.Events = new OpenIdConnectEvents
{
    OnRemoteFailure = context =>
    {
        // Log ou breakpoint aqui
        context.Response.Redirect("/Home/Error?message=" + context.Failure?.Message);
        context.HandleResponse();
        return Task.CompletedTask;
    },
    OnTokenValidated = context =>
    {
        // Aqui você pode inspecionar o token/claims
        return Task.CompletedTask;
    }
};

});
//.AddOktaMvc(new OktaMvcOptions
//{
//    OktaDomain = builder.Configuration["Okta:Domain"],
//    ClientId = builder.Configuration["Okta:ClientId"],
//    ClientSecret = builder.Configuration["Okta:ClientSecret"],
//    AuthorizationServerId = "default", // ou personalizado, se tiver
//    CallbackPath = "/authorization-code/callback"
//});

var teste = new OktaMvcOptions
{
    OktaDomain = builder.Configuration["Okta:Domain"],
    ClientId = builder.Configuration["Okta:ClientId"],
    ClientSecret = builder.Configuration["Okta:ClientSecret"],
    AuthorizationServerId = "default", // ou personalizado, se tiver
    CallbackPath = builder.Configuration["Okta:RedirectUri"]
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
