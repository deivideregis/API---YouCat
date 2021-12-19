using AutoMapper;
using DevIO.APIYouCat.Configuration;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevIO.APIYouCat
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            //Para não deixar endereço de conexão exposto no Github para outros acessarem o banco de dados
            //altere o arquivo de conexão para a secret do arquivo json: appsettings.Production/opção Gerenciar Segredos do usuários
            //botão direito do mouseno projeto: AspNetCoreIdentity
            //vai gerar um arquivo 'secret.json' cola o endereço da conexão
            //vai ficar local essa configuração da conexão
            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<YouCatDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityConfig(Configuration);

            //instalado o pacote: AutoMapper.Extensions.Microsoft.DependencyInjection
            services.AddAutoMapper(typeof(Startup));

            //***PARA IGNORAR VALIDAÇÃO AUTOMÁTICA*********************
            //services.Configure<ApiBehaviorOptions>(Options =>
            //{
            //    Options.SuppressModelStateInvalidFilter = true;
            //});
            //*********************************************************

            services.AddApiConfig();

            services.AddSwaggerConfig();

            services.AddLoggingConfig(Configuration);

            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfig(env);

            app.UseSwaggerConfig(provider);

            app.UseLoggingConfiguration();
        }
    }
}
