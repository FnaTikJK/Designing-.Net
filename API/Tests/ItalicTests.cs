namespace Tests;

public class ItalicTests
{
    private MarkdownService markdownService;
    
    [SetUp]
    public void SetUp()
    {
        markdownService = new MarkdownService(new[] {new ItalicBuilder()});
    }

    [Test]
    public void WithoutMark()
    {
        var text = "text without marks";
        var expected = text;
        
        ProcessTest(text, expected);
    }

    [Test]
    public void SimpleMark()
    {
        var text = "_text with mark_";
        var expected = "<em>text with mark</em>";
        
        ProcessTest(text, expected);
    }
    
    [Test]
    public void UnfinishedMark()
    {
        var text = "_unfinished";
        var expected = text;
        
        ProcessTest(text, expected);
    }

    [TestCase("_part_ial", "<em>part</em>ial", TestName = "partial start") ]
    [TestCase("pa_rt_ial", "pa<em>rt</em>ial", TestName = "partial middle")]
    [TestCase("part_ial_", "part<em>ial</em>", TestName = "partial end")]
    public void SpecificMarkedTexts(string text, string expected)
    {
        ProcessTest(text, expected);
    }

    [TestCase("__", TestName = "empty tag")]
    [TestCase("_1234567890_", TestName = "numbers")]
    [TestCase("part_ial ial_", TestName = "partial")]
    [TestCase("_partial i_al", TestName = "partial 2")]
    [TestCase("incorrect_ start_", TestName = "incorrect token start")]
    [TestCase("_incorrect _end ", TestName = "incorrect token end")]

    public void SpecificUnmarkedTexts(string text)
    {
        ProcessTest(text, text);
    }

    private void ProcessTest(string text, string expected)
    {
        var processed = markdownService.ProcessTextAsync(text).Result;

        processed.IsSuccess.Should().Be(true);
        processed.Value.Should().Be(expected);
    }
}