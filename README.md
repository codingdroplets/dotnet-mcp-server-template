# dotnet-mcp-server-template

A clean, minimal **Model Context Protocol (MCP) server** in C# / ASP.NET Core that you can
clone and fill with your own tools. It uses the official
[MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk) over HTTP, so any MCP client -
**Claude, GitHub Copilot, VS Code**, and others - can discover and call your tools.

"How do I build an MCP server in C#?" is one of the most-searched questions in the .NET AI
space right now. This is the fastest answer: a working server in ~20 lines, ready for you to
drop your own logic into.

---

## Why this exists

This is a free starter from the Coding Droplets course
[**AI-Powered APIs in .NET**](https://aiapis.codingdroplets.com) (Chapter 14, "MCP - Make Your
API Speak AI"). MCP is "USB-C for AI tools": a standard way to expose what your application can
do so an AI assistant can use it. This template gives you the server skeleton; the course shows
you how to wire it over a real API, secure it, and connect it to a live client.

> **Go deeper:** the full course builds a production-shaped AI API end to end - chat, structured
> outputs, RAG, tool calling, agents, MCP, security, evaluation, observability, and deployment,
> all in .NET. **[AI-Powered APIs in .NET →](https://aiapis.codingdroplets.com)**

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

That's it. No database, no API keys - the example tools run anywhere.

## Quick start

```
git clone https://github.com/codingdroplets/dotnet-mcp-server-template.git
cd dotnet-mcp-server-template
dotnet run
```

The server listens on `http://localhost:5080`, with the MCP endpoint at `/mcp`.

## Try it

The included `McpServerTemplate.http` file has ready-to-send requests (use the REST Client in
VS Code or your IDE). Or from a terminal - MCP is JSON-RPC 2.0 over HTTP, and each request needs
both `Accept` types:

```
# macOS / Linux - list the available tools
curl -s -X POST http://localhost:5080/mcp \
     -H "Content-Type: application/json" \
     -H "Accept: application/json, text/event-stream" \
     -d '{"jsonrpc":"2.0","id":1,"method":"tools/list","params":{}}'
```

```
# Windows (PowerShell) - call curl.exe and use --% so the JSON passes through verbatim
curl.exe --% -s -X POST http://localhost:5080/mcp -H "Content-Type: application/json" -H "Accept: application/json, text/event-stream" -d "{\"jsonrpc\":\"2.0\",\"id\":1,\"method\":\"tools/list\",\"params\":{}}"
```

You'll get back the three example tools: `echo`, `reverse_text`, and `add`.

## Connect it to an MCP client

Point any MCP client at `http://localhost:5080/mcp` (HTTP transport). For example, in VS Code's
MCP configuration or Claude's developer settings, add an HTTP MCP server with that URL, then ask
the assistant to use one of the tools.

## Add your own tools

Open [`ExampleTools.cs`](ExampleTools.cs) and replace the example methods. Adding a tool is three steps:

1. Write a static method (in any `[McpServerToolType]` class).
2. Mark it `[McpServerTool]` and describe it - and each parameter - with `[Description]`.
3. Done. `WithToolsFromAssembly()` discovers it automatically.

```csharp
[McpServerTool]
[Description("Get the current status of an order by its id.")]
public static string GetOrderStatus(
    [Description("The order id, e.g. A1001")] string orderId)
    => /* call your real service here */;
```

Method names are exposed to clients in snake_case (`GetOrderStatus` becomes `get_order_status`).
The `[Description]` text is how the client's model decides when and how to call your tool, so
write it for a reader who can't see your code.

## How it works

| File | Job |
|------|-----|
| `Program.cs` | Registers the MCP server with HTTP transport and maps it at `/mcp`. |
| `ExampleTools.cs` | The example tools. Replace these with your own. |

```csharp
builder.Services
    .AddMcpServer()
    .WithHttpTransport(options => options.Stateless = true)
    .WithToolsFromAssembly();
// ...
app.MapMcp("/mcp");
```

## Built with

- [`ModelContextProtocol.AspNetCore`](https://www.nuget.org/packages/ModelContextProtocol.AspNetCore) - the official MCP C# SDK with ASP.NET Core hosting

## Learn more

- **Course:** [AI-Powered APIs in .NET](https://aiapis.codingdroplets.com) - the full guide this starter belongs to
- **MCP C# SDK:** [github.com/modelcontextprotocol/csharp-sdk](https://github.com/modelcontextprotocol/csharp-sdk)
- **Blog:** [codingdroplets.com](https://codingdroplets.com) - keeping up with the .NET AI stack
- **More free starters:** [github.com/codingdroplets](https://github.com/codingdroplets)

## License

[MIT](LICENSE) - free to use, modify, and build on.
