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
using ApiRestFull.Hypermedia.Filters;
using ApiRestFull.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using ApiRestFull.Services.Implementations;
using ApiRestFull.Services;
using Microsoft.Extensions.Options;
using ApiRestFull.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
            
            // armazenando as configurações de token em uma classe.
            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
                )
                .Configure(tokenConfigurations);

            //adicionando apenas uma instancia do serviço, não tem duas configurações.
            services.AddSingleton(tokenConfigurations);

            // definindo parametros de autenticaçaõ
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options => 
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = tokenConfigurations.Issuer,
                   ValidAudience = tokenConfigurations.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
               };
           });

            // adicionando autorização ao serviço
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());

            });




            //adicionando cors(prevenir problemas de restrição de acesso) para que não se limite a outras urls
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson();

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

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOptions);

            // pacote versionamento nuget: Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api RestFull do zero ao azure com aspnetcore e Docker",
                        Version = "v1",
                        Description = "Desenvolvimento api Restful",
                        Contact = new OpenApiContact
                        {
                            Name = "Fernando Calefi",
                            Url = new Uri("https://github.com/fercalefi/ApiRestFull")
                        }
                    });
            });


            //Injeção de dependencia - busines para ser injetado no controller
            services.AddScoped<IPessoaBusiness, PessoaBusinessImplementation>();

          

            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            services.AddTransient<ITokenService, TokenService>();



            //Injeção de dependencia - userrepository 
            services.AddScoped<IUserRepository, UserRepository>();

            //Injeção de dependencia - repository para ser injetado no business
            services.AddScoped<IPessoaRepository, PessoaRepositoryImplementation>();

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

            // sempre depois de usehhttpsrediretion e userouting e antes de useendpoints
            app.UseCors();

            // gera o json com a documentação
            app.UseSwagger();

            // gera o html
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api RestFull do zero ao azure com aspnetcore e Docker - v1");
            });

            var option = new RewriteOptions();

            // redirecionar para pagina do swagger
            option.AddRedirect("^$", "swagger");
            // configurar a nossa swaggerpage
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
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
