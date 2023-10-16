using API.Infrasturcture;
using API.Modules.MarkdownModule.Adapters;
using API.Modules.MarkdownModule.Ports;

namespace API.Modules.MarkdownModule;

public class MarkdownModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IMarkdownService, MarkdownService>();

        return services;
    }
}