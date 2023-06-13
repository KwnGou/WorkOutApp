
using Microsoft.EntityFrameworkCore;
using WorkOutAPI.Model;

namespace WorkOutAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyCorsPolicy = "_MyCorsPolicy";


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
                {
                    options.AddPolicy(name: MyCorsPolicy,
                        policy =>
                        {
                            policy.WithOrigins(builder.Configuration.GetSection("CORSorigins").Get<string[]>());
                            policy.AllowAnyMethod();
                            policy.AllowAnyHeader();
                        });

                });
            // Add services to the container.
            builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WorkOutDB")));
            builder.Services.AddAutoMapper(typeof(MapperProfile));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyCorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
