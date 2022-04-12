using System;
using System.Configuration;
using AngleSharp.Dom;
using MatBlazor;
using Microsoft.Extensions.Configuration;
using Sprache;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Client.Pages.Components.Organisms;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class EventCreateFormTests : EventsContextBase<EventCreateForm>
{
    private SportEvent SportEvent { get; set; } = null!;
    private IElement EventNameTextField => Component.Find("input[aria-label=\"Name\"]");
    private IElement EventSportTextField => Component.Find("input[aria-label=\"Sport\"]");
    private IElement EventDescriptionTextField => Component.Find("textarea[aria-label=\"Description\"]");
    private IElement EventDateTextField => Component.Find("input[aria-label=\"Date\"]");
    
    public EventCreateFormTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void EventPropertiesMatchEventData()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleEvent(RelationshipType.Joined), Map.MapType.PositionalMap);
        var name = EventNameTextField.GetAttribute("value");
        var sport = EventSportTextField.GetAttribute("value");
        var description = EventDescriptionTextField.GetAttribute("value");
        var date = EventDateTextField.GetAttribute("value");
        Assert.Equal(SportEvent.Name, name);
        Assert.Equal(SportEvent.Sport, sport);
        Assert.Equal(SportEvent.Description, description);

        if (DateTime.TryParse(date, out var dateTime))
            Assert.Equal(SportEvent.Date.TrimMilliseconds(), dateTime.TrimMilliseconds());
        else
            throw new ParseException("Event date could not be parsed to date time");
    }
    
    public override IRenderedComponent<EventCreateForm> SetupComponent(params object[] args)
    {
        var ev = args[0] as SportEvent;
        SportEvent = ev!;
        return RenderComponent<EventCreateForm>(parameters => parameters
            .Add(p => p.SportEvent, ev)
        );
    }
}