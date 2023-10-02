using API.Infrasturcture;
using API.Modules.MarkdownModule.Ports;

namespace API.Modules.MarkdownModule.Adapters;

public class MarkdownService : IMarkdownService
{
    public async Task<Result<string>> ProcessTextAsync(string sourceText)
    {
        return Result.Fail<string>("Not implemented");
    }
}