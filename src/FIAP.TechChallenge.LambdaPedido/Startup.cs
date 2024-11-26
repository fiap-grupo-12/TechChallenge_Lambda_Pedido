using Amazon.Lambda.Annotations;
using FIAP.TechChallenge.LambdaPedido.API.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.TechChallenge.LambdaPedido.API
{
    [ExcludeFromCodeCoverage]
    [LambdaStartup]
    public class Startup
    {
        public Startup()
        { }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProjectDependencies();

            services.AddCors();
            services.AddControllers();
        }
    }
}
