
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Pipr
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPipr(this IServiceCollection services)
        {
            PipelineConfiguration.ServiceProvider = services.BuildServiceProvider();
            services.AddTransient<PipelineBuilder>();
        }
    }
}