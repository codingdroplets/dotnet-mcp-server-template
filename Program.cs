var builder = WebApplication.CreateBuilder(args);

// Register the MCP server with HTTP transport, and expose every [McpServerToolType]
// class in this assembly as tools. Stateless = true makes each request self-contained,
// which is the simplest mode to host and to test.
builder.Services
    .AddMcpServer()
    .WithHttpTransport(options => options.Stateless = true)
    .WithToolsFromAssembly();

var app = builder.Build();

// An MCP client (Claude, GitHub Copilot, VS Code, ...) connects here over HTTP.
app.MapMcp("/mcp");

app.Run();
