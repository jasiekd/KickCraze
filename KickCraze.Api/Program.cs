using KickCraze.Api.Data;
using KickCraze.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.ML;
using KickCraze.Api.Model;

namespace KickCraze.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            string filePath = Path.GetFullPath(Path.Combine(currentDirectory, "..\\..\\..\\Model\\MatchResultModel.zip"));

            builder.Services.AddPredictionEnginePool<FootballMatchData, MatchPrediction>()
            .FromFile(modelName: "ResultMatchPrediction", filePath: filePath, watchForChanges: true);

            builder.Services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                  policy => {
                      policy.WithOrigins("http://localhost:3000", "https://jasiekd.github.io")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true)
                .SetIsOriginAllowedToAllowWildcardSubdomains();
                  });
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API KickCraze", Description = "Endpoints for my API", Version = "v1" });
            });

            builder.Services.AddSingleton(provider =>
            {
                return new CustomHttpClient(builder.Configuration["FootballApiInfo:APIURL"], builder.Configuration["FootballApiInfo:Token"]);
            });

            builder.Services.AddScoped<IMatchService, MatchService>();
            builder.Services.AddScoped<ILeagueService, LeagueService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}