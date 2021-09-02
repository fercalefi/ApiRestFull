using System;
using System.Collections.Generic;
using ApiRestFull.Model.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiRestFull.Business;
using ApiRestFull.Repository;
using ApiRestFull.Repository.Implementations;
using ApiRestFull.Business.Implementations;
using Serilog;
using ApiRestFull.Repository.Generic;
using Microsoft.Net.Http.Headers;

namespace ApiRestFull
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; } 
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Vai no arquivo de configuração appsetings.json e busca o conteudo das chaves
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];

            // Adiciona o DbContext passando no options a variavel de conexao informada acima
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            // alterar para xml no envio/retorno nugget: microsoft.aspnetcore.mvc.formatters.xml
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();
                ;

            // pacote versionamento nuget: Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            //Injeção de dependencia - busines para ser injetado no controller
            services.AddScoped<IPessoaBusiness, PessoaBusinessImplementation>();

            //Injeção de dependencia - repository para ser injetado no business
            services.AddScoped<IPessoaRepository, PessoaRepositoryImplementation>();

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

            //Injeção de dependencia do repositorio generico para ser usado no Business
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));


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

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                { 
                    Locations = new List<string> { "db/migrations","db/dataset"},
                    IsEraseDisabled = true, 
                };
                evolve.Migrate();

            }
            catch (Exception ex)
            {

                Log.Error("DataBase Migration failed", ex.Message);
            }
        }
    }
}
