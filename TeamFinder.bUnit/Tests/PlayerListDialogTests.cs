using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using MatBlazor;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Molecules;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class PlayerListDialogTests : EventsContextBase<PlayerListDialog>
{
    private List<User> GetUsers() => new List<User>()
    {
        new User() { Email = TestUserEmail, UserName = TestUserName},
        new User() { Email = "newtestuser@gmail.com", UserName = "new-test-user"}
    };
    
    [Fact]
    public void IsNotExpandedAfterCloseButtonClick()
    {
        var users = GetUsers();
        TestSetup(AuthorizationState.Unauthorized, users);
        var beforeClick = Component.Instance.PlayersExpanded;
        Component.Find("button")
            .Click();

        var afterClick = Component.Instance.PlayersExpanded;
        Assert.True(beforeClick);
        Assert.False(afterClick);
    }
    
    [Fact]
    public void AllPlayersAreListed()
    {
        TestSetup(AuthorizationState.Unauthorized, GetUsers());

        var users = Component.Find("ul")
            .GetInnerText();

        var allUsersAreListed = UsersInEvent.TrueForAll(u => users.Contains(u.UserName!));
        Assert.True(allUsersAreListed);
    }
    
    public PlayerListDialogTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }
    
    public override IRenderedComponent<PlayerListDialog> SetupComponent(params object[] args)
    {
        var users = args[0] as List<User>;
        UsersInEvent = users!;
        return RenderComponent<PlayerListDialog>(parameters => parameters
            .Add(p => p.PeopleInEvent, users)
        );
    }

    private List<User> UsersInEvent { get; set; } = null!;
}