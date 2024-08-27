using Microsoft.EntityFrameworkCore;
namespace Registration_server_project.startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's URL
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // Other ConfigureServices code
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other Configure code

            app.UseCors("AllowSpecificOrigin");

            // Other Configure code
        }

    }
}
