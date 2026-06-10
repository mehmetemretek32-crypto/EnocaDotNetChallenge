using Microsoft.EntityFrameworkCore;
using EnocaChallenge.Data.Contexts;
using EnocaChallenge.Service.Services;
using Hangfire;
using EnocaChallenge.Service.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EnocaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Hangfire'ı SQL Server ile yapılandırıyoruz ve arka planda çalışacak sunucuyu ekliyoruz.
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<ReportService>();  //Rapor servisi

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard(); // Hangfire paneline /hangfire adresinden erişim

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var reportService = scope.ServiceProvider.GetRequiredService<ReportService>();

    recurringJobManager.AddOrUpdate(
        "hourly-carrier-report",
        () => reportService.GenerateHourlyCarrierReportsAsync(),
        Cron.Hourly); 
}

app.Run();
