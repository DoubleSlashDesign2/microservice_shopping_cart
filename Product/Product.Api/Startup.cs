﻿namespace Product.Api
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Middleware;
    using Infrastructure.Persistence;
    using Infrastructure.Persistence.NHibernate;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using Domain.Repository;
    using Infrastructure.Persistence.NHibernate.Repository;
    using Application.Service;
    using Application.Assembler;
    using Product.Application.Contracts;

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
            services.AddAutoMapper();
            services.AddMvc();
            services.AddSingleton(new SessionFactory(Environment.GetEnvironmentVariable("MYSQL_CONECTION_STRING_LOCAL")));
            var serviceProvider = services.BuildServiceProvider();
            var mapper = serviceProvider.GetService<IMapper>();

            services.AddSingleton(new ProductCreateAssembler(mapper));
            services.AddSingleton(new CategoryCreateAssembler(mapper));

            services.AddScoped<IUnitOfWork, UnitOfWorkNHibernate>();

            services.AddTransient<IProductRepository, ProductNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new ProductNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<ICategoryRepository, CategoryNHibernateRepository>((ctx) =>
            {
                IUnitOfWork unitOfWork = ctx.GetService<IUnitOfWork>();
                return new CategoryNHibernateRepository((UnitOfWorkNHibernate)unitOfWork);
            });

            services.AddTransient<IProductApplicationService, ProductApplicationService>();
            services.AddTransient<ICategoryApplicationService, CategoryApplicationService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "System API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseMiddleware(typeof(ErrorMiddleware))
                .UseMvc()
               .UseDefaultFiles(options)
               .UseStaticFiles()
                .UseSwagger()
               .UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }
    }
}
