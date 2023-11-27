using ELabel.Controllers;

namespace ELabel.Middleware
{
    public class DatabaseInitializationStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                next(builder);
                InitializeDatabase(builder);
            };
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var databaseInitializationController = services.GetRequiredService<DatabaseInitializationController>();
                    databaseInitializationController.InitializeDatabase().Wait();
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately, e.g., log or throw
                    var logger = services.GetRequiredService<ILogger<DatabaseInitializationStartupFilter>>();
                    logger.LogError(ex, "An error occurred while initializing the database.");
                }
            }
        }
    }

}
