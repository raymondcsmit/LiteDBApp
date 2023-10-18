using LiteDBApp.DB;
using LiteDBApp.IdentityStore;
using LiteDBApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
			services.AddIdentity<LiteDbUser, LiteDbRole>()
					.AddUserStore<LiteDbUserStore>()
					.AddRoleStore<LiteDbRoleStore>()
					.AddDefaultTokenProviders();



			services.AddSingleton<ILiteDbContext, LiteDbContext>();
			services.AddScoped(typeof(ILiteDbRepository<>), typeof(LiteDbRepository<>));
			services.AddScoped(typeof(IIdentityRepository<>), typeof(IdentityRepository<>));
			services.AddScoped<UserService>();


			var jwtSettings = Configuration.GetSection("JwtSettings");
			var secret = jwtSettings["Secret"];
			if (Encoding.UTF8.GetByteCount(secret) * 8 < 256)
			{
				throw new Exception("The key size is smaller than the minimum required size of 256 bits.");
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings["Issuer"],
				ValidAudience = jwtSettings["Audience"],
				IssuerSigningKey = key,
			};

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = tokenValidationParameters;
				});

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
