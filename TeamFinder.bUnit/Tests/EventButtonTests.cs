using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Components;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class EventButtonTests : EventsContextBase<EventButton>
{
    [Fact]
    public void NotLoggedInTest()
    {
        TestSetup(AuthorizationState.Unauthorized, SampleFutureEvent);
        MockHttpResponses(new Dictionary<string, object>
            {
                { Endpoints.JoinEvent, true }
            }
        );
        var nav = Services.GetRequiredService<NavigationManager>();

        Component.Find("a").Click();
        Component.WaitForAssertion(() =>
        {
            Assert.Equal(nav.Uri, Localhost + "authentication/login");
        });
    }

    [Fact]
    public void CannotJoinFinishedEventTest()
    {
        TestSetup(AuthorizationState.Authorized, SamplePastEvent);
        MockHttpResponses(new Dictionary<string, object>
            {
                {Endpoints.JoinEvent, true}
            }
        );
        var btn = Component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        Component.WaitForElementTextToNotBe("button", FinishedText, TimeSpan.FromSeconds(1), false);
        
        var afterClickText = btn.GetInnerText();
        
        Assert.True(afterClickText == FinishedText);
        Assert.True(beforeClickText == FinishedText);
        Assert.False(TestEvent.Type == RelationshipType.Joined);
    }
    
    [Fact]
    public void JoinEventTest()
    {
        TestSetup(AuthorizationState.Authorized, SampleFutureEvent);
        MockHttpResponses(new Dictionary<string, object>
            {
                {Endpoints.JoinEvent, true}
            }
        );
        
        var btn = Component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        Component.WaitForElementTextToBe("button", LeaveText);
        
        var afterClickText = btn.GetInnerText();
        
        Assert.True(beforeClickText == JoinText);
        Assert.True(afterClickText == LeaveText);
        Assert.True(TestEvent.Type == RelationshipType.Joined);
    }

    public override IRenderedComponent<EventButton> SetupComponent(params object[] args)
    {
        var ev = args[0] as SportEvent;
        return RenderComponent<EventButton>(parameters => parameters
            .Add(p => p.SportEv, ev)
        );
    }
    
    public EventButtonTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
       
    }
}