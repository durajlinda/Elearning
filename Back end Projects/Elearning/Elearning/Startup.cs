using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Elearning
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "student-delete",
                    pattern: "Student/ConfirmDelete/{id}",
                    defaults: new { controller = "Student", action = "ConfirmDelete" }
                );

                endpoints.MapControllerRoute(
                    name: "course-delete",
                    pattern: "Course/ConfirmDelete/{id}",
                    defaults: new { controller = "Course", action = "ConfirmDelete" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            });
        }

    }
}
