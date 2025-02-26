using daryon_house_ui.Server.Applications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<WaterUsedService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBff();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
        options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7119";

        options.ClientId = "daryonhousewebapp";
        options.ClientSecret = "daryonhousewebappsecret";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("api");
        options.Scope.Add("role");
        options.Scope.Add("offline_access");

        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });
//.AddOpenIdConnect("oidc", options =>
//{
//    options.Authority = "https://demo.duendesoftware.com";

//    options.ClientId = "interactive.confidential";
//    options.ClientSecret = "secret";
//    options.ResponseType = "code";
//    options.ResponseMode = "query";

//    options.Scope.Clear();
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("api");
//    options.Scope.Add("offline_access");

//    options.MapInboundClaims = false;
//    options.GetClaimsFromUserInfoEndpoint = true;
//    options.SaveTokens = true;
//});


builder.Services.AddScoped<ApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();


app.MapRazorPages();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapFallbackToFile("index.html");

app.Run();