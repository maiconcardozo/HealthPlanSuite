namespace HealthPlan.API
{
    /// <summary>
    /// Main application class responsible for host initialization and execution.
    /// Simplified to use Startup.cs pattern for organized configuration management.
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// Main application entry point.
        /// Creates and runs the web host using configurations defined in the Startup class.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            // Creates the web application builder with provided arguments
            var builder = WebApplication.CreateBuilder(args);

            // Configures application services using the Startup class
            // This promotes better organization and separation of concerns
            var startup = new Startup(builder.Configuration, builder.Environment);
            startup.ConfigureServices(builder.Services);

            // Builds the application with all service configurations applied
            var app = builder.Build();

            // Configures the middleware pipeline using the Startup class
            // Defines how HTTP requests will be processed
            startup.Configure(app, builder.Environment);

            // Starts application execution
            // The application will run until it receives a stop signal
            app.Run();
        }
    }
}

// Required for WebApplicationFactory in integration tests:
public partial class Program { }