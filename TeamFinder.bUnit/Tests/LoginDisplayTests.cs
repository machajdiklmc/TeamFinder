using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Client.Shared;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class LoginDisplayTests : UserContextBase<LoginDisplay>
{
    private readonly FakeSignOutSessionStateManager _signOutManager;
    private const string RegisterId = "register";
    private const string LoginId = "login";
    private const string LogoutId = "logout";
    private const string ProfileId = "profile";
    
    [Fact]
    public void ProfileUrlIsCorrect()
    {
        TestSetup(AuthorizationState.Authorized);
        var login = Component.FindById(ProfileId);
        var href = login.Attributes["href"]?.Value;
        Assert.Equal("authentication/profile",href);
    }
    
    [Fact]
    public void LoginUrlIsCorrect()
    {
        TestSetup(AuthorizationState.Unauthorized);
        var login = Component.FindById(LoginId);
        var href = login.Attributes["href"]?.Value;
        Assert.Equal("authentication/login",href);
    }
    
    [Fact]
    public async Task LogoutStatesAreCorrect()
    {
        TestSetup(AuthorizationState.Authorized);
        var beforeClickIsSignedOut = _signOutManager.IsSignedOut;
        var logoutBtn = Component.FindById(LogoutId);
        
        await logoutBtn.ClickAsync(new MouseEventArgs());
        
        Assert.False(beforeClickIsSignedOut);
        Assert.True(_signOutManager.IsSignedOut);
    }

    [Fact]
    public void RegisterUrlIsCorrect()
    {
        TestSetup(AuthorizationState.Unauthorized);
        var login = Component.FindById(RegisterId);
        var href = login.Attributes["href"]?.Value;
        Assert.Equal("authentication/register",href);
    }

    public LoginDisplayTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _signOutManager = Services.GetRequiredService<FakeSignOutSessionStateManager>();
    }
}