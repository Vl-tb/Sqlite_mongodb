using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;

using MySolution.DAL;
using MySolution.Services;
using MongoDB.Driver;



var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Додайте DbContext iз налаштуваннями рядка пiдключення
builder.Services.AddDbContext<LabDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")).ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.AmbientTransactionWarning)));
builder.Services.AddScoped<SamplerService>();

// Конфiгурацiя MongoDB Settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
// Реєстрацiя MongoDB-клiєнта

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddSingleton<StudentService>();
builder.Services.AddHostedService<UpdateService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>();

    // dbContext.Database.ExecuteSqlRaw("DELETE FROM Students");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM Courses");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM Teachers");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM Classrooms");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM TeacherCourses");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM StudentCourses");
    // dbContext.Database.ExecuteSqlRaw("DELETE FROM ClassroomCourses");

    
    // dbContext.SaveChanges();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Connection to the SQLite database is successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error connecting to the database: {ex.Message}");
    }

    try
        {
            var databases = scope.ServiceProvider.GetRequiredService<IMongoClient>().ListDatabaseNames().ToList();
            Console.WriteLine("Successfully connected to MongoDB. Databases:");
            databases.ForEach(db => Console.WriteLine(db));
            
        }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to connect to MongoDB: {ex.Message}");
        
    }

    var seedService = scope.ServiceProvider.GetRequiredService<SamplerService>();
    // seedService.Sample();
}
app.Run();