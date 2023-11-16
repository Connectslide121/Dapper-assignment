using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration setup
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Retrieve the connection string from appsettings.json
string connectionString = configuration.GetConnectionString("DefaultConnection");

// Retrieve the password from environment variables
string password = configuration["Database__Password"];

// Replace the password in the connection string
connectionString = connectionString.Replace("your-password", password);

// Add the configured MySqlConnection to the services
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();