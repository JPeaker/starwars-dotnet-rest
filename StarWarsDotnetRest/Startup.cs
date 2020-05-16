using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarWarsApiCSharp;
using StarWarsDotnetRest.Services;

namespace StarWarsDotnetRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            var personRepository = new Repository<Person>();
            services.AddSingleton<IRepository<Person>>(personRepository);
            services.AddSingleton<IRepository<Film>>(new Repository<Film>());
            services.AddSingleton<IRepository<Planet>>(new Repository<Planet>());
            services.AddSingleton<IRepository<Vehicle>>(new Repository<Vehicle>());
            services.AddSingleton<IRepository<Starship>>(new Repository<Starship>());
            services.AddSingleton<IRepository<Specie>>(new Repository<Specie>());

            var relationshipHandler = new RelationshipHandler(personRepository);
            services.AddSingleton<IRelationshipHandler>(relationshipHandler);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
