using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace asp_net_test.test;

[Collection("DiagnosticListenerTest")] //To avoid tests from DiagnosticListenerTests running in parallel with this we add them to 1 collection.
public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestPost()
    {
        var client = _factory.CreateClient();
        var body = "{'foo': 'bar'}";
        await client.PostAsync("/", new StringContent(body, Encoding.UTF8, "application/json"));
    }
}