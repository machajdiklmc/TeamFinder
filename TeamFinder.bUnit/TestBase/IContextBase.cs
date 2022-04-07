using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TeamFinder.bUnit.TestBase;

public interface IContextBase<out TComponent> where TComponent : ComponentBase
{
    void TestSetup(AuthorizationState authorizationState, params object[] componentArgs);
    IRenderedComponent<TComponent> SetupComponent(params object[] args);
    void MockHttpResponses(Dictionary<string, object> dictionary);
}