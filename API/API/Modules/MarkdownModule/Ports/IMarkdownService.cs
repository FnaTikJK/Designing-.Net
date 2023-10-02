using API.Infrasturcture;

namespace API.Modules.MarkdownModule.Ports;

public interface IMarkdownService
{
    public Task<Result<string>> ProcessTextAsync(string sourceText);
}