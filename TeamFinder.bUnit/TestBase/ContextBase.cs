using System;
using System.Collections.Generic;
using System.Security.Claims;
using FluentAssertions;
using IdentityModel;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using RichardSzalay.MockHttp;
using TeamFinder.bUnit.Extensions;
using TeamFinder.bUnit.TestBase.Helpers;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.TestBase;

public abstract class ContextBase<TComponent> : TestContext, IContextBase<TComponent> where TComponent : ComponentBase
{
    protected const string Localhost = "http://localhost/";
    protected virtual string TestUserName => "test-user";
    protected virtual string TestUserEmail => "test-user@utb.cz";
    protected TestAuthorizationContext TestAuthorizationContext;

    protected MockHttpMessageHandler MockHttpMessageHandler;
    protected readonly ITestOutputHelper TestOutputHelper;
    protected IRenderedComponent<TComponent> Component = null!;
    protected ContextBase(ITestOutputHelper testOutputHelper)
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
  
        TestOutputHelper = testOutputHelper;
        ComponentExtensions.Initialize(testOutputHelper);
        TestAuthorizationContext = this.AddTestAuthorization();
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
        TestAuthorizationContext.SetAuthorized(TestUserName, state);

        if (state is not AuthorizationState.Unauthorized)
        {
            TestAuthorizationContext.SetClaims(
                new Claim(ClaimTypes.Email, TestUserEmail),
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            );
        }
    }

    public virtual IRenderedComponent<TComponent> SetupComponent(params object[] args)
    {
        return RenderComponent<TComponent>();
    }

    public void MockHttpResponses(Dictionary<string, object> dictionary)
    {
        foreach (var (endpoint, returnValue) in dictionary)
        {
            MockHttpMessageHandler.When(Localhost + endpoint).RespondJson(returnValue);
        }
    }

    private IRenderedComponent<TComponent> _componentSetup(params object[] args)
    {
        #if DEBUG
            TestOutputHelper.WriteLine("Setting component");
        #endif
        return SetupComponent(args);
    }
}