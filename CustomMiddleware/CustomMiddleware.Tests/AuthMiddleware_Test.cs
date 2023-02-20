using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using MiddlewareWeb;
using Xunit;

namespace CustomMiddleware.Tests;

public class AuthMiddleware_Test
{
    [Fact]
    public async Task TestAuthMiddleware_NoUserAndPass_Unauthorized()
    {
        var middleware = new AuthMiddleware(next: (innerHttpContext) =>
        {
            innerHttpContext.Response.StatusCode = 200;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString("");

        await middleware.InvokeAsync(context);

        Assert.Equal(401, context.Response.StatusCode);
    }

    [Fact]
    public async Task TestAuthMiddleware_MissingPassword_Unauthorized()
    {
        var middleware = new AuthMiddleware(next: (innerHttpContext) =>
        {
            innerHttpContext.Response.StatusCode = 200;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString("?username=user1");

        await middleware.InvokeAsync(context);

        Assert.Equal(401, context.Response.StatusCode);
    }

    [Fact]
    public async Task TestAuthMiddleware_CorrectUserAndPass_Authorized()
    {
        var middleware = new AuthMiddleware(next: (innerHttpContext) =>
        {
            innerHttpContext.Response.StatusCode = 200;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString("?username=user1&password=password1");

        await middleware.InvokeAsync(context);

        Assert.Equal(200, context.Response.StatusCode);
    }

    [Fact]
    public async Task TestAuthMiddleware_IncorrectUserAndPass_Unauthorized()
    {
        var middleware = new AuthMiddleware(next: (innerHttpContext) =>
        {
            innerHttpContext.Response.StatusCode = 200;
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.QueryString = new QueryString("?username=user5&password=password2");

        await middleware.InvokeAsync(context);

        Assert.Equal(401, context.Response.StatusCode);
    }
}