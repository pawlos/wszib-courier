﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands;
using Courier.Core.Commands.Parcels;
using Courier.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Courier.Api
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
            services.AddMvc();
            services.AddTransient<ILocationService,LocationService>();
            services.AddTransient<IParcelService,ParcelService>();
            services.AddTransient<ICommandHandler<CreateParcel>,CreateParcelHandler>();
            services.AddSingleton<ICommandDispatcher>(new CommandDispatcher(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
