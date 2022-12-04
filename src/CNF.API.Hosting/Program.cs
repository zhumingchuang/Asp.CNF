namespace CNF.API.Hosting
{
    using CNF.Share.Domain.Repository;
    using CNF.Share.Infrastructure.Common;
    using NLog.Web;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            WebHelper.InjectAssembly(builder.Services, "CNF.Share.Domain");
            builder.Services.AddHttpContextAccessor();
            Console.WriteLine(builder.Configuration["ConnectionStrings:MySql"]);
            builder.Services.AddScoped(typeof(IBaseServer<>), typeof(BaseServer<>));
            builder.Logging.AddNLogWeb();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}