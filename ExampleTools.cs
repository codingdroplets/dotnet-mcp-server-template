using System.ComponentModel;
using ModelContextProtocol.Server;

namespace CodingDroplets.McpServerTemplate;

/// <summary>
/// Example MCP tools. Each <c>[McpServerTool]</c> method becomes a tool that any MCP client
/// (Claude, Copilot, VS Code, ...) can discover and call. The <c>[Description]</c> attributes
/// are what the client and its model read to decide when and how to call each tool.
///
/// ----------------------------------------------------------------------------------------
///  THIS IS A TEMPLATE. Replace these example tools with your own.
///  A real tool would call your services, database, or external APIs. To add one:
///    1. Write a static method here (or in another [McpServerToolType] class).
///    2. Mark it [McpServerTool] and describe it (and each parameter) with [Description].
///    3. That's it - WithToolsFromAssembly() picks it up automatically.
///  Method names are exposed to clients in snake_case: ReverseText -> "reverse_text".
/// ----------------------------------------------------------------------------------------
/// </summary>
[McpServerToolType]
public static class ExampleTools
{
    [McpServerTool]
    [Description("Echo a message back unchanged. A simple check that the server is reachable.")]
    public static string Echo(
        [Description("The message to echo back")] string message)
        => message;

    [McpServerTool]
    [Description("Reverse the characters in a piece of text.")]
    public static string ReverseText(
        [Description("The text to reverse")] string text)
        => string.IsNullOrEmpty(text) ? text : new string(text.Reverse().ToArray());

    [McpServerTool]
    [Description("Add two integers and return their sum.")]
    public static int Add(
        [Description("The first number")] int a,
        [Description("The second number")] int b)
        => a + b;
}
