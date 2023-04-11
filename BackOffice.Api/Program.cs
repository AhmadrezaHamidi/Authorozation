using BackOffice.Api.Mapping;
using BackOffice.Application;
using BackOffice.Persistance;
using Houshmand.Framework.ExceptionHandler;
using Houshmand.Framework.Logging;
using Houshmand.Framework.Logging.Console.Extensions;
using Houshmand.Framework.Logging.Files.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddApplicationServices(connectionString);
builder.Services.AddPersistanceService(builder.Configuration);


var logManager = new LogBuilder()
    .MinimumLevel.Info()
    .WriteTo.Console()
    .WriteTo.File()
    .CreateLogger<BaseResponse>();
builder.Services.AddTransient<Houshmand.Framework.Logging.ILogger<BaseResponse>>(x => logManager);

//builder.Services.AddMediatR(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();
app.UseRouting();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.InjectStylesheet("/SwaggerUi/SwaggerDark.css");
//        c.InjectJavascript("/SwaggerUi/swagger-ui-bundle.js");
//        c.DefaultModelExpandDepth(depth: 1);
//        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
//        c.DocExpansion(DocExpansion.None);
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        c.RoutePrefix = string.Empty;
//    });
//}

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseMiddleware<GlobalExceptionMiddleware>();

app.Run();

