using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Components;
using RichardSzalay.MockHttp;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase.Helpers;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.TestBase;

public abstract class ContextBase<TComponent> : TestContext, IContextBase<TComponent> where TComponent : ComponentBase
{
    protected const string Localhost = "http://localhost/";
    protected virtual string TestUserName => "qwe@qwe.qwe";
    protected virtual string TestUserEmail => "qwe@qwe.qwe";

    protected readonly MockHttpMessageHandler MockHttpMessageHandler;
    protected readonly ITestOutputHelper TestOutputHelper;
    protected IRenderedComponent<TComponent> Component = null!;
    protected ContextBase(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
        ComponentExtensions.Initialize(testOutputHelper);
        
        JSInterop.Setup<Object>("matBlazor.matButton.init", _ => true);
        MockHttpMessageHandler = Services.AddMockHttpClient();
    }

    public virtual void TestSetup(AuthorizationState authorizationState, params object[] componentArgs)
    {
        SetupAuthorization(authorizationState);
        Component = _componentSetup(componentArgs);
    }    
    
    protected virtual void SetupAuthorization(AuthorizationState state)
    {
        TestOutputHelper.WriteLine("Setting Authorization");
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

    public abstract IRenderedComponent<TComponent> SetupComponent(params object[] args);
    
    public virtual void MockHttpResponses(Dictionary<string, object> dictionary)
    {
        foreach (var (endpoint, returnValue) in dictionary)
        {
            MockHttpMessageHandler.When(Localhost + endpoint).RespondJson(returnValue);
        }
    }

    private IRenderedComponent<TComponent> _componentSetup(params object[] args)
    {
        TestOutputHelper.WriteLine("Setting component");
        return SetupComponent(args);
    }
}