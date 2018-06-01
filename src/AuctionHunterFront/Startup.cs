using System;
using AuctionHunter;
using AuctionHunterFront.Models;
using AuctionHunterFront.Services;
using AuctionHunterFront.Services.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuctionHunterFront
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			Configuration = configuration;
			Environment = hostingEnvironment;
		}

		public IConfiguration Configuration { get; }
		public IHostingEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<UsersDbContext>();
			services.AddDbContext<AuctionHunterDbContext>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<UsersDbContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Auth/Login";
			});

			services.AddSingleton<IConfiguration>(Configuration);
			services.AddSingleton<IG2AAuctionHunterService, G2AAuctionHunterService>();
			services.AddSingleton<ICdKeysAuctionHunterService, CdKeysAuctionHunterService>();
			services.AddSingleton<IEmailSender, EmailSender>();

			services.AddMvc();

			services.Configure<MvcOptions>(options =>
			{
				if (Environment.IsProduction())
				{
					options.Filters.Add(new RequireHttpsAttribute());
				}
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
			loggerFactory.AddAzureWebAppDiagnostics();

			AHInitializer.Init();
			serviceProvider.GetService<IG2AAuctionHunterService>().Start();
			serviceProvider.GetService<ICdKeysAuctionHunterService>().Start();
		}
	}
}
