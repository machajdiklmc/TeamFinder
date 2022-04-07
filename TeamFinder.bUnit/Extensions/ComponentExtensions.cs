using System;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Components;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Extensions;

public static class ComponentExtensions
{
    private static ITestOutputHelper? _testOutputHelper;

    public static void WaitForComponentStateToBe<TComponent>(this IRenderedComponent<TComponent> renderedComponent, 
        Func<IRenderedComponent<TComponent>, bool> predicate, TimeSpan? timeSpan = null, bool throwOnFailure = true)
        where TComponent : IComponent
    {
        try
        {
            timeSpan ??= new TimeSpan(0, 0, 3);
            renderedComponent.WaitForState(() => predicate.Invoke(renderedComponent),
                timeSpan);
        }
        catch (Exception e)
        {
            if (throwOnFailure)
            {
                if (_testOutputHelper is null)
                    Console.WriteLine(e.Message);
                else
                    _testOutputHelper.WriteLine(e.Message);
                
                throw;
            }
        }  
    }
    public static void WaitForElementStateToBe<TComponent>(this IRenderedComponent<TComponent> renderedComponent,
        string elementCssSelector, Func<IElement, bool> predicate, TimeSpan? timeSpan = null, bool throwOnFailure = true)
        where TComponent : IComponent
    {
        WaitForComponentStateToBe(renderedComponent, c => predicate.Invoke(c.Find(elementCssSelector)), timeSpan, throwOnFailure);
    }
    
    public static void WaitForElementTextToBe<TComponent>(this IRenderedComponent<TComponent> renderedComponent, string elementCssSelector, string text, TimeSpan? timeSpan = null, bool throwOnFailure = true)
        where TComponent : IComponent
    {
        WaitForElementStateToBe(renderedComponent,elementCssSelector, e => e.GetInnerText() == text, timeSpan, throwOnFailure);
    }
    
    public static void WaitForElementTextToNotBe<TComponent>(this IRenderedComponent<TComponent> renderedComponent, string elementCssSelector, string text, TimeSpan? timeSpan = null, bool throwOnFailure = true)
        where TComponent : IComponent
    {
        WaitForElementStateToBe(renderedComponent,elementCssSelector, e => e.GetInnerText() != text, timeSpan, throwOnFailure);
    }

    public static void Initialize(ITestOutputHelper? testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
}