using System;
using Microsoft.AspNetCore.Components;
using TeamFinder.Client.Services;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.TestBase;

public abstract class EventsContextBase<TComponent> : ContextBase<TComponent> where TComponent : ComponentBase
{
    protected SportEvent TestEvent = null!;
    protected SportEvent GetSamplePastEvent(RelationshipType type) => new("Test event", DateTime.Now.Subtract(TimeSpan.FromDays(30)), "desc")
    {
        Type = type,
        Location = GetSampleLocation()
    };
    protected SportEvent GetSampleEvent(RelationshipType type) => new("Test event", DateTime.Now.Add(TimeSpan.FromDays(30)), "desc")
    {
        Type = type,
        Location = GetSampleLocation()
    };

    protected SportEventLocation GetSampleLocation()
    {
        return new SportEventLocation()
        {
            City = "Zlín",
            Id = Guid.NewGuid(),
            Latitude = 49.22,
            Longitude = 17.66
        };
    }

    protected SportEventLocation GetSampleLocation2()
    {
        return new SportEventLocation()
        {
            City = "Praha",
            Id = Guid.NewGuid(),
            Latitude = 50.08,
            Longitude = 14.42
        };
    }
    protected EventsContextBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        Services.AddTransient<EventsService>();
    }

    public override void TestSetup(AuthorizationState authorizationState, params object[] componentArgs)
    {
        base.TestSetup(authorizationState, componentArgs);
        TestEvent = (componentArgs[0] as SportEvent)!;
    }    
}