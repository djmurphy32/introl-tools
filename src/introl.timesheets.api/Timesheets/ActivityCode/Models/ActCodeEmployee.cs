﻿namespace Introl.Timesheets.Api.Timesheets.ActivityCode.Models;

public class ActCodeEmployee
{
    public required string Name { get; init; }
    public required string MemberCode { get; init; }
    public required List<ActCodeHours> ActivityCodeHours { get; init; }
}