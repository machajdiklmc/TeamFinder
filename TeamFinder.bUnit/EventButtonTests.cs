using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using FluentAssertions;
using IdentityModel;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Moq;
using RichardSzalay.MockHttp;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Client.Services;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit;

public class MyTestContext : TestContext
{
    protected const string Localhost = "http://localhost/";
    protected const string TestUserName = "qwe@qwe.qwe";
    protected const string TestUserEmail = "qwe@qwe.qwe";
    
    protected readonly MockHttpMessageHandler MockHttpMessageHandler;
    protected readonly ITestOutputHelper? TestOutputHelper;
    
    protected MyTestContext(ITestOutputHelper? testOutputHelper)
    {
        ComponentExtensions.Initialize(testOutputHelper);
        TestOutputHelper = testOutputHelper;
        JSInterop.Setup<Object>("matBlazor.matButton.init", _ => true);
        MockHttpMessageHandler = Services.AddMockHttpClient();
    }
    
    protected void Setup(AuthorizationState state)
    {
        var auth = this.AddTestAuthorization()
            .SetAuthorized(TestUserName, state);

        if (state is not AuthorizationState.Unauthorized)
        {
            auth.SetClaims(
                new Claim(ClaimTypes.Email, TestUserEmail),
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            );
        }
    }
}

public class EventButtonTests : MyTestContext
{
    private SportEvent TestPastEvent => new SportEvent("Test event", DateTime.Now.Subtract(TimeSpan.FromDays(30)), "desc");
    private SportEvent TestFutureEvent => new SportEvent("Test event", DateTime.Now.Add(TimeSpan.FromDays(30)), "desc");
    private const string JoinText = "Join";
    private const string LeaveText = "Leave";
    private const string FinishedText = "Finished";

    public EventButtonTests(ITestOutputHelper? testOutputHelper) : base(testOutputHelper)
    {
        Services.AddTransient<EventsService>();
    }
    [Fact]
    public void NotLoggedInTest()
    {
        Setup(AuthorizationState.Unauthorized);
        MockHttpMessageHandler.When(Localhost + Endpoints.JoinEvent).RespondJson(true);
        var nav = Services.GetRequiredService<NavigationManager>();

        var component = RenderButton(TestPastEvent);
        component.Find("a").Click();
        component.WaitForAssertion(() =>
        {
            Assert.Equal(nav.Uri, Localhost + "authentication/login");
        });
    }
    [Fact]
    public void CannotJoinFinishedEventTest()
    {
        Setup(AuthorizationState.Authorized);
        MockHttpMessageHandler.When(Localhost + Endpoints.JoinEvent).RespondJson(true);

        var testEvent = TestPastEvent;
        var component = RenderButton(testEvent);
        var btn = component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        component.WaitForElementTextToNotBe("button", FinishedText, TimeSpan.FromSeconds(1), false);
        
        var afterClickText = btn.GetInnerText();
        
        Assert.True(afterClickText == FinishedText);
        Assert.True(beforeClickText == FinishedText);
        Assert.False(testEvent.Type == RelationshipType.Joined);
    }
    
    [Fact]
    public void JoinEventTest()
    {
        Setup(AuthorizationState.Authorized);
        MockHttpMessageHandler.When(Localhost + Endpoints.JoinEvent).RespondJson(true);

        var testEvent = TestFutureEvent;
        var component = RenderButton(testEvent);
        var btn = component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        component.WaitForElementTextToBe("button", LeaveText);
        
        var afterClickText = btn.GetInnerText();
        
        Assert.True(beforeClickText == JoinText);
        Assert.True(afterClickText == LeaveText);
        Assert.True(testEvent.Type == RelationshipType.Joined);
    }

    private IRenderedComponent<EventButton> RenderButton(SportEvent ev)
    {
        return RenderComponent<EventButton>(parameters => parameters
            .Add(p => p.SportEv, ev)
        );
    }
}