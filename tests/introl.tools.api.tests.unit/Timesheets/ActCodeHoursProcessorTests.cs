﻿using FluentAssertions;
using Introl.Tools.Timesheets.ActivityCode.Models;
using Introl.Tools.Timesheets.ActivityCode.Services;
using Xunit;

namespace Introl.Tools.Api.Tests.Unit.Timesheets;

public class ActCodeHoursProcessorTests
{
    private readonly IActCodeHoursProcessor _sut = new ActCodeHoursProcessor();

    [Theory, ClassData(typeof(ActCodeHoursProcessorTestData))]
    public void Process_WhenCalled_ReturnsExpectedResult(string _, List<ActCodeHours> input, Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>> expected)
    {
        var result = _sut.Process(input, true);

        result.Should().BeEquivalentTo(expected);
    }
}
