using Amazon.Lambda.Annotations;
using FIAP.TechChallenge.LambdaPedido.API.Extensions;

namespace FIAP.TechChallenge.LambdaPedido.API
{
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
