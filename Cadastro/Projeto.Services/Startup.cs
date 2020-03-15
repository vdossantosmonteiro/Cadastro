using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Projeto.DAL.Contracts;
using Projeto.DAL.Repositories;

namespace Projeto.Services
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Configurando Injeção de dependencia

            var connectionString = Configuration.GetConnectionString("Aula16");
            services.AddTransient<IClienteRepository, ClienteRepository>
                (config => new ClienteRepository(connectionString));
            #endregion

            #region Configuração Swagger
            services.AddSwaggerGen(swagger => {
                swagger.SwaggerDoc("v1", 
                    new Info
                {
                    Title = "Sistema Asp.Net Web API  - Controle de Cliente",
                    Version = "v1",
                    Description = "Projeto desenvolvido em aula - C# WebDeveloper",
                        Contact = new Contact { Name = "COTI Informática",
                            Url = "http://www.cotiinformatica.com.br", Email = "contato@cotiinformatica.com.br" }
                    });
            });

            #endregion

            #region Configuração do Cors

            services.AddCors(c => c.AddPolicy("DefaultPolicy",
                cors =>
                {
                    cors.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                }
                ));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Configuração Swagger

            app.UseSwagger();
            app.UseSwaggerUI(swagger => { swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto"); });
            #endregion

            #region Configuração do CORS
            app.UseCors("DefaultPolicy");
            #endregion

            app.UseMvc();
        }
    }
}
