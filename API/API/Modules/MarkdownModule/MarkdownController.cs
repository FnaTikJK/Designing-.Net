using API.Modules.MarkdownModule.DTO;
using API.Modules.MarkdownModule.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.MarkdownModule;

[ApiController]
[Route("[controller]")]
public class MarkdownController : ControllerBase
{
    private readonly IMarkdownService markdownService;

    public MarkdownController(IMarkdownService markdownService)
    {
        this.markdownService = markdownService;
    }

    [HttpPost]
    public async Task<ActionResult<ProcessTextResponse>> ProcessTextAsync(ProcessTextRequest textRequest)
    {
        var response = await markdownService.ProcessTextAsync(textRequest.Text);

        return response.IsSuccess
            ? Ok(response.Value)
            : BadRequest(response.Error);
    }
}