using System.Reflection;
using Infra.Data;
using IoC;

//Builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyRegistration.Register(Assembly.GetExecutingAssembly(), builder.Configuration, builder.Host, builder.Services);

// App

var app = builder.Build();

AppDbContext.InitializeDatabase(app);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
    c.RoutePrefix = "swagger";
    c.OAuthClientId("swagger");
    c.OAuthAppName("Swagger UI");
});

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

