﻿using Introl.Tools.Api.Authorization;
using Introl.Tools.Racks.Services;
using Introl.Tools.Timesheets.ActivityCode.Services;
using Introl.Tools.Timesheets.Team.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.EnableAnnotations();
    opts.AddSecurityDefinition("ApiKey",
        new OpenApiSecurityScheme
        {
            Description = "The API Key to access the API.",
            Type = SecuritySchemeType.ApiKey,
            Name = AuthorizationConstants.ApiKeyHeader,
            In = ParameterLocation.Header,
            Scheme = "ApiKeyScheme"
        });

    var apiKeyScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
    };

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement { [apiKeyScheme] = new List<string>() });


    opts.AddSecurityRequirement(new OpenApiSecurityRequirement { [apiKeyScheme] = new List<string>() });

});

builder.Services.AddScoped<ITeamSourceReader, TeamSourceReader>();
builder.Services.AddScoped<ITeamSourceParser, TeamSourceParser>();
builder.Services.AddScoped<ITeamResultCellFactory, TeamResultCellFactory>();
builder.Services.AddScoped<ITeamResultWriter, TeamResultWriter>();
builder.Services.AddScoped<IEmployeeEmployeeTimesheetProcessor, EmployeeEmployeeEmployeeEmployeeTimesheetProcessor>();

builder.Services.AddScoped<IActCodeTimesheetProcessor, ActCodeTimesheetProcessor>();
builder.Services.AddScoped<IActCodeSourceReader, ActCodeSourceReader>();
builder.Services.AddScoped<IActCodeResultsWriter, ActCodeResultsWriter>();
builder.Services.AddScoped<IActCodeResultCellFactory, ActCodeResultCellFactory>();
builder.Services.AddScoped<IActCodeHoursProcessor, ActCodeHoursProcessor>();

builder.Services.AddScoped<IRackSourceReaderFactory, RackSourceReaderFactory>();
builder.Services.AddScoped<IRackSourceReader, RackSourceXlsxReader>();
builder.Services.AddScoped<IRackSourceReader, RackSourceCsvReader>();
builder.Services.AddScoped<IRackProcessor, RackProcessor>();
builder.Services.AddScoped<IRackCellFactory, RackCellFactory>();
builder.Services.AddScoped<IRackResultsWriter, RackResultsWriter>();

builder.Services.AddScoped<ApiKeyMiddleware>();
builder.Services.AddLogging();

var app = builder.Build();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.UseHttpsRedirection();


app.Run();
