using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Survey.Model;
using Survey.Model.Entities;
using Survey.Model.Repositories;

namespace Survey
{
	public class Startup
	{
		public Startup ( IConfiguration configuration ) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices ( IServiceCollection services ) {

			services.AddDbContext<SurveyContext> ();

			services.AddTransient<ISurveyRepository<Question> , SurveyRepository<Question>> ();
			services.AddTransient<ISurveyRepository<Answer> , SurveyRepository<Answer>> ();

			services.AddControllersWithViews ();

			services.AddSpaStaticFiles ( configuration => {
				configuration.RootPath = "ClientApp/build";
			} );
		}

		public void Configure ( IApplicationBuilder app , IWebHostEnvironment env ) {
			if ( env.IsDevelopment () ) {
				app.UseDeveloperExceptionPage ();
			} else {
				app.UseExceptionHandler ( "/Error" );
			}

			app.UseStaticFiles ();
			app.UseSpaStaticFiles ();

			app.UseRouting ();

			app.UseEndpoints ( endpoints => {
				endpoints.MapControllerRoute (
					name: "default" ,
					pattern: "{controller}/{action=Index}/{id?}" );
			} );

			app.UseSpa ( spa => {
				spa.Options.SourcePath = "ClientApp";

				if ( env.IsDevelopment () ) {
					spa.UseReactDevelopmentServer ( npmScript: "start" );
				}
			} );
		}
	}
}
