using ELabel.Controllers;
using ELabel.Data;
using ELabel.Middleware;
using ELabel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Load Producer configuration from appsettings.json
builder.Services.Configure<Producer>(builder.Configuration.GetSection("Producer"));

// Load environment variable with ELABEL prefix
builder.Configuration.AddEnvironmentVariables(prefix: "ELABEL_");

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Adding the view/localization services
builder.Services.AddLocalization();
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<DatabaseInitializationController>();
builder.Services.AddTransient<IStartupFilter, DatabaseInitializationStartupFilter>();

// Configure supported cultures and localization options
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Official languages of the EU (and ISO 639-1 language codes)
    CultureInfo[] supportedCultures = new[]
    {
        new CultureInfo("bg"), // Bulgarian (BG)
        new CultureInfo("hr"), // Croatian (HR)
        new CultureInfo("cs"), // Czech (CS)
        new CultureInfo("da"), // Danish (DA)
        new CultureInfo("nl"), // Dutch (NL)
        new CultureInfo("en"), // English (EN)
        new CultureInfo("et"), // Estonian (ET)
        new CultureInfo("fi"), // Finnish (FI)
        new CultureInfo("fr"), // French (FR)
        new CultureInfo("de"), // German (DE)
        new CultureInfo("el"), // Greek (EL)
        new CultureInfo("hu"), // Hungarian (HU)
        new CultureInfo("ga"), // Irish (GA)
        new CultureInfo("it"), // Italian (IT)
        new CultureInfo("lv"), // Latvian (LV)
        new CultureInfo("lt"), // Lithuanian (LT)
        new CultureInfo("mt"), // Maltese (MT)
        new CultureInfo("pl"), // Polish (PL)
        new CultureInfo("pt"), // Portuguese (PT)
        new CultureInfo("ro"), // Romanian (RO)
        new CultureInfo("sk"), // Slovak (SK)
        new CultureInfo("sl"), // Slovene (SL)
        new CultureInfo("es"), // Spanish (ES)
        new CultureInfo("sv"), // Swedish (SV)
    };

    // State what the default culture for your application is. This will be used if no specific culture
    // can be determined for a given request.
    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");

    // You must explicitly state which cultures your application supports.
    // These are the cultures the app supports for formatting numbers, dates, etc.
    options.SupportedCultures = supportedCultures;

    // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
    options.SupportedUICultures = supportedCultures;

    // Using Accept-Language HTTP header from browsers
    options.ApplyCurrentCultureToResponseHeaders = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }

    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions?.Value!);

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
