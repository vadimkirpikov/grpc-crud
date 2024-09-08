using System.Data;
using CrudAppGrpc.Interceptors;
using CrudAppGrpc.Repositories;
using CrudAppGrpc.Repositories.ProjectRepository;
using CrudAppGrpc.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.WebHost.UseUrls("http://*:1234");

var postgresConnection = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddCors(options => options.AddPolicy("AllowAny",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }))
    .AddScoped<IDbConnection>(serviceProvider => new NpgsqlConnection(postgresConnection))
    .AddScoped<IProjectRepository, Repository>()
    .AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());
var app = builder.Build();

app.UseCors("AllowAny");

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

app.MapGrpcService<CrudOptionsService>().EnableGrpcWeb();
app.Run();