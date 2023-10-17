﻿using LiteDBApp.DB;
using LiteDBApp.Services;

namespace LiteDBApp
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
			services.Configure<LiteDbOptions>(Configuration.GetSection("LiteDbOptions"));


			services.AddSingleton<ILiteDbContext, LiteDbContext>();
			services.AddScoped(typeof(ILiteDbRepository<>), typeof(LiteDbRepository<>));
			services.AddScoped<UserService>();
			// Add Swagger services
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				// Use Swagger in development
				app.UseSwagger();
				app.UseSwaggerUI();
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
