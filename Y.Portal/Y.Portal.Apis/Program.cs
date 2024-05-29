using Y.Infrastructure.Library.Core.WebInfrastructure;
using Y.Portal.Apis.Controllers.Extension;
using Y.Portal.Apis.Controllers.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppConfigurationServices();
builder.Services.AddCrossDoaminServices();
builder.Services.AddInversionOfControlServices();
builder.Services.AddSwaggerServices();
// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://*:5015");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfigure();
}
app.UseMiddleware(typeof(GlobalExceptionMiddelware));

app.UseCrossDoaminConfigure();

app.UseMiddleware(typeof(AuthorizedSysMiddleware));
app.UseMiddleware(typeof(AuthorizedMerchantMiddleware));

app.UseMiddleware(typeof(InitSessionDataMiddleware));

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.UseRouting();

app.MapControllers();
//app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();
