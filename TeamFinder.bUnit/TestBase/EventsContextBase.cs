using System;
using Microsoft.AspNetCore.Components;
using TeamFinder.Client.Services;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.TestBase;

public abstract class EventsContextBase<TComponent> : ContextBase<TComponent> where TComponent : ComponentBase
{
    protected SportEvent TestEvent = null!;
    protected SportEvent SamplePastEvent => new("Test event", DateTime.Now.Subtract(TimeSpan.FromDays(30)), "desc");
    protected SportEvent SampleFutureEvent => new("Test event", DateTime.Now.Add(TimeSpan.FromDays(30)), "desc");
    
    protected const string JoinText = "Join";
    protected const string LeaveText = "Leave";
    protected const string FinishedText = "Finished";
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