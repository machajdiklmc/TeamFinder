using Microsoft.AspNetCore.Components;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.TestBase;

public abstract class UserContextBase<TComponent> : ContextBase<TComponent> where TComponent : ComponentBase
{
    protected UserContextBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
}