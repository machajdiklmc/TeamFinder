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
    public void CannotJoinFinishedEventTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSamplePastEvent());
        MockHttpResponses(new Dictionary<string, object>
            {
                { Endpoints.JoinEvent, true }
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
    public void LeaveEventTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleEvent(RelationshipType.Joined));
        MockHttpResponses(new Dictionary<string, object>
            {
                { Endpoints.LeaveEvent, true }
            }
        );
        
        var btn = Component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        Component.WaitForElementTextToBe("button", JoinText);
        
        var afterClickText = btn.GetInnerText();
        
        Assert.True(beforeClickText == LeaveText);
        Assert.True(afterClickText == JoinText);
        Assert.True(TestEvent.Type == RelationshipType.None);
    }
    
    [Fact]
    public void OwnerCantJoinOrLeaveTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleEvent(RelationshipType.Owner));
        MockHttpResponses(new Dictionary<string, object>
            {
                { Endpoints.JoinEvent, true },
                { Endpoints.LeaveEvent, true }
            }
        );
        
        var btn = Component.Find("button");
        var beforeClickText = btn.GetInnerText();
        
        btn.Click();
        Component.WaitForElementTextToBe("button", JoinText,TimeSpan.FromMilliseconds(500), throwOnFailure: false);
        Component.WaitForElementTextToBe("button", LeaveText,TimeSpan.FromMilliseconds(500), throwOnFailure: false);

        var afterClickText = btn.GetInnerText();
        
        Assert.True(beforeClickText == OwnerText);
        Assert.True(afterClickText == OwnerText);
        Assert.True(TestEvent.Type == RelationshipType.Owner);
    }

    [Fact]
    public void JoinEventTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleEvent(RelationshipType.None));
        MockHttpResponses(new Dictionary<string, object>
            {
                { Endpoints.JoinEvent, true }
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

    [Fact]
    public void NotLoggedInTest()
    {
        TestSetup(AuthorizationState.Unauthorized, GetSampleEvent(RelationshipType.None));
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