﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace User.Service.Test;

public class AutoMoqDataAttribute : AutoDataAttribute 
{
    public AutoMoqDataAttribute() : base(() =>
    {
        var fixture = new Fixture().Customize(new CompositeCustomization(
            new AutoMoqCustomization(),
            new SupportMutableValueTypesCustomization()));

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    })
    {

    }
}
