using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;
using RichardSzalay.MockHttp;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase;
using TeamFinder.bUnit.TestBase.Helpers;
using TeamFinder.Client.Pages.Components.Molecules;
using TeamFinder.Client.Pages.Components.Organisms;
using TeamFinder.Shared;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class EventCardTests : EventsContextBase<EventCard>
{
    private SportEvent SportEvent { get; set; } = null!;
    
    [Fact]
    public void MapIsSimpleMap()
    {
        TestSetup(AuthorizationState.Unauthorized, GetSampleEvent(RelationshipType.None));
        var map = Component.FindComponent<Map>();
        Assert.Equal(Map.MapType.SimpleMap , map.Instance.Type);
    }

    [Fact]
    public async Task PeopleListExpandsOnClick()
    {
        TestSetup(AuthorizationState.Unauthorized, GetSampleEvent(RelationshipType.None));
        var playerListComponent = Component.FindComponent<PlayerListDialog>();
        var beforeClickExpanded = playerListComponent.Instance.PlayersExpanded;
        var btn = Component.FindById("showPeopleBtn1");
        await btn.ClickAsync(new DragEventArgs());
        Assert.False(beforeClickExpanded);
        Assert.True(playerListComponent.Instance.PlayersExpanded);
    }
    
    [Fact]
    public void EventDetailsAreCorrect()
    {
        TestSetup(AuthorizationState.Unauthorized, GetSampleEvent(RelationshipType.None));
        var name = Component.FindById("name1").GetInnerText();
        var description = Component.FindById("description1").GetInnerText();
        var sport = Component.FindById("sport1").GetInnerText();
        var cityDate = Component.FindById("cityDate1").GetInnerText().Split(", ");
        var city = cityDate[0];
        var date = DateTime.Parse(cityDate[1]);
        
        Assert.Equal(SportEvent.Name, name);
        Assert.Equal(SportEvent.Description, description);
        Assert.Equal(SportEvent.Sport, sport);
        Assert.Equal(SportEvent.Date.TrimMilliseconds(), date.TrimMilliseconds());
        Assert.Equal(SportEvent.Location.City, city);
    }
    
    public EventCardTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        MockHttpResponses(new Dictionary<string, object>()
        {
            { Endpoints.GetAllUsersInEvent, new List<User>() }
        });
    }
    
    public override IRenderedComponent<EventCard> SetupComponent(params object[] args)
    {
        var ev = args[0] as SportEvent;
        SportEvent = ev!;
        return RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.SportEv, ev)
        );
    }
}