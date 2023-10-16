using API.Infrasturcture;
using API.Modules.MarkdownModule.Models;

namespace API.Modules.MarkdownModule.Ports;

public interface IMarkdownService
{
    public Task<Result<string>> ProcessTextAsync(string sourceText);
    string BuildMarkdownString(string sourceText, List<Token> tokens);
}