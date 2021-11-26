using DevIO.APIYouCat.Data;
using DevIO.APIYouCat.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DevIO.APIYouCat.Configuration
{
    //adicionar no comando:
    //add-migration Identity -Context ApplicationDbContext -> precisa especificar, pois irá apresentar erro que existe
    //update-database -Context ApplicationDbContext -> Vai criar as tabelas no banco de dados (login)
    //add nova migration renomeado - para inserir novas ou alterar tabelas
    //dotnet ef migrations add CriandoSQLServerV1 --context ApplicationDbContext --project "H:\PROJETO API\APIYoutCat\DevIO.APIYouCat\DevIO.APIYouCat.csproj"
    //atualiza nova migrations - para inserir novas ou alterar tabelas
    //dotnet ef database update CriandoSQLServerV1 --context ApplicationDbContext --project "H:\PROJETO API\APIYoutCat\DevIO.APIYouCat\DevIO.APIYouCat.csproj"
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();

            // JWT

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });

            return services;
        }
    }
}
