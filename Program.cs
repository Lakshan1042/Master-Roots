using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ------------------ MongoDB ------------------
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var mongoConnection = builder.Configuration.GetConnectionString("MongoDb")
        ?? throw new InvalidOperationException("MongoDB connection string not found.");
    return new MongoClient(mongoConnection);
});

// Register MongoDB database accessor
builder.Services.AddScoped<IMongoDatabase>(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration.GetValue<string>("MongoDbDatabase")
        ?? "StudentDb"; // fallback name
    return client.GetDatabase(databaseName);
});

// ------------------ Identity (in-memory, optional) ------------------
// If you still want login/register but don’t want SQL Server
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders(); // no EF storage

// ------------------ Other Services ------------------
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailSender, EmailSender>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(Int32.Parse(port));
});


var app = builder.Build();

// ------------------ Middleware ------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // needed if you keep Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
