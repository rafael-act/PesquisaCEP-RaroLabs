using Domain.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaCEP
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PesquisaCEP",
                    Version = "v1",
                    Description = "Pesquisar um endereço na api livre ViaCep (https://viacep.com.br) para um determinado cep, retornando todas as informações do respectivo cep.",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Castro",
                        Email = "rafael.actt@gmail.com",
                        Url = new Uri("https://github.com/rafael-act/PesquisaCEP-RaroLabs")
                    }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            //DI do repositório address
            //O AddScoped método registra o serviço com um tempo de vida com escopo, o tempo de vida de uma única solicitação.
            services.AddScoped<IAddressRepository, AddressRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PesquisaCEP v1"));
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
