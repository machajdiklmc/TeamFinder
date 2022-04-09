using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using TeamFinder.bUnit.TestBase;
using TeamFinder.Client.Pages.Components.Atoms;
using TeamFinder.Client.Pages.Components.Molecules;
using TeamFinder.Shared.Models;
using Xunit.Abstractions;

namespace TeamFinder.bUnit.Tests;

public class LocationFieldsTests : EventsContextBase<LocationFields>
{
    [Fact]
    public void LatitudeValueChangesTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleLocation());
        var newLatitude = GetSampleLocation2().Latitude;
        var latitudeComponent = Component.FindComponent<LatitudeField>();
        var latitudeBeforeChange = latitudeComponent.Instance.Value;
        var latitudeBeforeChangeShouldBe = TestLocation.Latitude;
        
        TestLocation.Latitude = newLatitude;
        UpdateLocationParameter(TestLocation);
        
        Assert.Equal(latitudeBeforeChange, latitudeBeforeChangeShouldBe);
        Assert.Equal(newLatitude, latitudeComponent.Instance.Value);
    }
    
    [Fact]
    public void LongitudeValueChangesTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleLocation());
        var newLongitude = GetSampleLocation2().Longitude;
        var longitudeComponent = Component.FindComponent<LongitudeField>();
        var longitudeBeforeChange = longitudeComponent.Instance.Value;
        var longitudeBeforeChangeShouldBe = TestLocation.Longitude;
        
        TestLocation.Longitude = newLongitude;
        UpdateLocationParameter(TestLocation);
        
        Assert.Equal(longitudeBeforeChange, longitudeBeforeChangeShouldBe);
        Assert.Equal(newLongitude, longitudeComponent.Instance.Value);
    }
    
    [Fact]
    public void CityValueChangesTest()
    {
        TestSetup(AuthorizationState.Authorized, GetSampleLocation());
        var newCity = GetSampleLocation2().City;
        var cityComponent = Component.FindComponent<CityField>();
        var cityBeforeChange = cityComponent.Instance.Value;
        var cityBeforeChangeShouldBe = TestLocation.City;
        
        TestLocation.City = newCity;
        UpdateLocationParameter(TestLocation);
        
        Assert.Equal(cityBeforeChange, cityBeforeChangeShouldBe);
        Assert.Equal(newCity, cityComponent.Instance.Value);
    }
    public LocationFieldsTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        
    }

    public override IRenderedComponent<LocationFields> SetupComponent(params object[] args)
    {
        var location = args[0] as SportEventLocation;
        TestLocation = location!;
        return RenderComponent<LocationFields>(parameters => parameters
            .Add(p => p.Location, location)
        );
    }
    
    private void UpdateLocationParameter(SportEventLocation newLocation)
    {
        Component.InvokeAsync(() =>
        {
            Component.Instance.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>()
            {
                {"Location", newLocation}
            }));
        });
    }
    
    private SportEventLocation TestLocation { get; set; } = null!;
}