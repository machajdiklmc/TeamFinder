using System;
using Microsoft.AspNetCore.Components;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Client.Shared;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class LoginDisplayTests : UserContextBase<LoginDisplay>
{
    private readonly NavigationManager _navigationManager;
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
    public void LogoutUrlIsCorrect()
    {
        TestSetup(AuthorizationState.Authorized);
        var logoutBtn = Component.FindById(LogoutId);
        logoutBtn.Click();
        Component.WaitForAssertion(() =>
        {
            Assert.Equal(_navigationManager.Uri, Localhost + "authentication/logout");
        });
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
        _navigationManager = Services.GetRequiredService<NavigationManager>();
    }
    
    public override IRenderedComponent<LoginDisplay> SetupComponent(params object[] args )
    {
        return RenderComponent<LoginDisplay>();
    }
}