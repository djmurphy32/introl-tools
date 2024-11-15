﻿using System.Collections;
using Introl.Tools.Timesheets.ActivityCode.Models;

namespace Introl.Tools.Api.Tests.Unit.Timesheets;

public class ActCodeHoursProcessorTestData : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { "40hrs", FortyHrsInput, FortyHrsResult },
        new object[] { "41hrs Single", FortyOneHrsSingleActInput, FortyOneHrsSingleActResult },
        new object[] { "Over 40hrs Multiple", OverFortyHrsMultipleActInput, OverFortyHrsMultipleActResult },
        new object[] { "Over 40hrs Ordering", OverFortyHrsOrderingInput, OverFortyHrsOrderingResult },
        new object[] { "Real world example", RealWorldInput, RealWorldResult },
    };

    public IEnumerator<object[]> GetEnumerator()

    {
        return _data.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator()

    {
        return GetEnumerator();
    }

    private static DateOnly StartDate => new(2021, 1, 1);

    private static List<ActCodeHours> FortyHrsInput =>
    [
        new ActCodeHours { StartTime = new DateTime(StartDate, new TimeOnly(9, 0)), Hours = 5, ActivityCode = "A" },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(4), new TimeOnly(8, 30)),
            Hours = 10,
            ActivityCode = "A"
        },
        new ActCodeHours { StartTime = new DateTime(StartDate, new TimeOnly(14, 0)), Hours = 2, ActivityCode = "B" },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(5), new TimeOnly(13, 0)),
            Hours = 6,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(10, 0)),
            Hours = 10,
            ActivityCode = "B"
        },
        new ActCodeHours { StartTime = new DateTime(StartDate, new TimeOnly(16, 0)), Hours = 7, ActivityCode = "A" }
    ];


    private static Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>> FortyHrsResult =>
        new()
        {
            {
                "A",
                new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (12, 0) },
                    { StartDate.AddDays(4), (10, 0) },
                }
            },
            {
                "B",
                new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (2, 0) },
                    { StartDate.AddDays(3), (10, 0) },
                    { StartDate.AddDays(5), (6, 0) },
                }
            },
        };

    private static List<ActCodeHours> FortyOneHrsSingleActInput =>
    [
        new ActCodeHours { StartTime = new DateTime(StartDate, new TimeOnly(9, 0)), Hours = 41, ActivityCode = "A" }
    ];

    private static Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>>
        FortyOneHrsSingleActResult =>
        new() { { "A", new Dictionary<DateOnly, (double regHours, double otHours)> { { StartDate, (40, 1) } } } };

    private static List<ActCodeHours> OverFortyHrsMultipleActInput =>
    [
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate, new TimeOnly(9, 0)),
            Hours = 2,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate, new TimeOnly(11, 30)),
            Hours = 8,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(1), new TimeOnly(8, 30)),
            Hours = 10,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(2), new TimeOnly(8, 30)),
            Hours = 10,
            ActivityCode = "C"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(8, 30)),
            Hours = 8,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(12, 30)),
            Hours = 4,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(16, 30)),
            Hours = 3,
            ActivityCode = "C"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(4), new TimeOnly(16, 30)),
            Hours = 5,
            ActivityCode = "B"
        },
    ];

    private static Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>>
        OverFortyHrsMultipleActResult =>
        new()
        {
            {
                "A",
                new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (2, 0) },
                    { StartDate.AddDays(3), (10, 2) },
                }
            },
            {
                "B", new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (8, 0) },
                    {StartDate.AddDays(1), (10, 0)},
                    { StartDate.AddDays(4), (0, 5) }
                }
            },
            {
                "C", new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate.AddDays(2), (10, 0) },
                    { StartDate.AddDays(3), (0, 3) },
                }
            }
        };

    private static List<ActCodeHours> OverFortyHrsOrderingInput =>
    [
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(1), new TimeOnly(9, 0)),
            Hours = 8,
            ActivityCode = "A"
        },
        new ActCodeHours { StartTime = new DateTime(StartDate, new TimeOnly(9, 0)), Hours = 40, ActivityCode = "A" }
    ];

    private static Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>>
        OverFortyHrsOrderingResult =>
        new()
        {
            {
                "A", new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (40, 0) },
                    { StartDate.AddDays(1), (0, 8) }
                }
            },
        };

    private static List<ActCodeHours> RealWorldInput =>
    [
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate, new TimeOnly(9, 0)),
            Hours = 3,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate, new TimeOnly(12, 0)),
            Hours = 9,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(1), new TimeOnly(9, 0)),
            Hours = 8.5,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(1), new TimeOnly(17, 30)),
            Hours = 3.5,
            ActivityCode = "A"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(9, 30)),
            Hours = 5,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(14, 30)),
            Hours = 3.5,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(3), new TimeOnly(18, 0)),
            Hours = 3.5,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(4), new TimeOnly(9, 0)),
            Hours = 3,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(4), new TimeOnly(12, 0)),
            Hours = 9,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(5), new TimeOnly(9, 0)),
            Hours = 5,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(5), new TimeOnly(14, 0)),
            Hours = 3.5,
            ActivityCode = "B"
        },
        new ActCodeHours
        {
            StartTime = new DateTime(StartDate.AddDays(5), new TimeOnly(18, 0)),
            Hours = 3.5,
            ActivityCode = "B"
        },
    ];



    private static Dictionary<string, Dictionary<DateOnly, (double regHours, double otHours)>>
        RealWorldResult =>
        new()
        {
            {
                "A", new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate, (12, 0) },
                    { StartDate.AddDays(1), (12, 0) }
                }
            },
            {
                "B", new Dictionary<DateOnly, (double regHours, double otHours)>
                {
                    { StartDate.AddDays(3), (12, 0) },
                    { StartDate.AddDays(4), (4, 8) },
                    { StartDate.AddDays(5), (0, 12) }
                }
            }
        };
}
