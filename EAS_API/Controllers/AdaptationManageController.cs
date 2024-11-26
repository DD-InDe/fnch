using Aspose.Words.Lists;
using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EAS_API.Controllers;

[ApiController]
[Route("api/manage")]
public class AdaptationManageController(EasFullDbContext context) : ControllerBase
{
    [HttpGet("modules")]
    public async Task<IActionResult> GetModules()
    {
        try
        {
            List<ModuleForHr> list = new();
            List<Module> modules = await context.Modules.ToListAsync();

            foreach (var module in modules)
            {
                list.Add(new()
                {
                    Module = module,
                    Accessors = await context
                        .ModuleWorks.Where(c => c.ModuleId == module.Id && c.RoleId == 2)
                        .Select(c => c.Employee)
                        .ToListAsync(),
                    Developers = await context
                        .ModuleWorks.Where(c => c.ModuleId == module.Id && c.RoleId == 1)
                        .Select(c => c.Employee)
                        .ToListAsync(),
                    Positions = await context
                        .ModuleAccesses.Where(c => c.ModuleId == module.Id)
                        .Select(c => c.Position)
                        .ToListAsync()
                });
            }

            return Ok(list);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("modules")]
    public async Task<IActionResult> AddModule([FromBody] NewModule newModule)
    {
        try
        {
            Module module = new()
            {
                CodeName = newModule.Name,
                DevelopDeadline = newModule.Deadline,
                StatusId = 1,
                DateCreate = DateTime.Now
            };
            await context.Modules.AddAsync(module);
            await context.SaveChangesAsync();

            foreach (var accessor in newModule.Accessors)
            {
                await context.ModuleWorks.AddAsync(new()
                {
                    ModuleId = module.Id,
                    EmployeeId = accessor.Id,
                    RoleId = 2,
                    Main = accessor == newModule.Main
                });
            }

            foreach (var developer in newModule.Developers)
            {
                await context.ModuleWorks.AddAsync(new()
                {
                    ModuleId = module.Id,
                    EmployeeId = developer.Id,
                    RoleId = 1,
                });
            }

            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("adaptationMaps")]
    public async Task<IActionResult> AddMap([FromBody] NewAdaptationMap newAdaptationMap)
    {
        try
        {
            AdaptationMap map = new()
            {
                EmployeeId = newAdaptationMap.EmployeeId,
                PositionId = newAdaptationMap.PositionId,
                DepartmentId = newAdaptationMap.DepartmentId,
                DateCreate = DateTime.Now
            };
            await context.AdaptationMaps.AddAsync(map);
            await context.SaveChangesAsync();

            foreach (var module in newAdaptationMap.ModuleList)
                await context.MapModules.AddAsync(new()
                {
                    ModuleId = module.Module.Id,
                    MapId = map.Id,
                    IsComplete = false
                });

            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("events/filterId={filterId:int}")]
    public async Task<IActionResult> GetEvents(int filterId)
    {
        try
        {
            List<EventAnalyze> eventAnalyzes = new();
            switch (filterId)
            {
                case 0:
                {
                    List<Position> positions = await context.Positions.ToListAsync();
                    foreach (var position in positions)
                    {
                        List<Module> modules = await context
                            .ModuleAccesses.Where(c => c.PositionId == position.Id)
                            .Select(c => c.Module)
                            .ToListAsync();
                        List<Event> events = new();
                        foreach (var module in modules)
                            events.AddRange(await context
                                .Events.Where(c => c.ModuleId == module.Id)
                                .ToListAsync());

                        eventAnalyzes.Add(new() { Name = position.Name, Count = events.Count });
                    }

                    break;
                }
                case 1:
                {
                    /*
                     * Фильтрация по отделу
                     * У должности есть отдел. У отдела есть должности.
                     */
                    break;
                }
                case 2:
                {
                    // eventAnalyzes.add
                    int year = DateTime.Now.Year;
                    int month = 1;
                    // цикл по кварталам
                    for (int i = 1; i < 5; i++)
                    {
                        int count = 0;
                        for (int j = 0; j < 3; j++)
                        {
                            count += await context
                                .Events.Where(c =>
                                    c.Date.Value.Month == month && c.Date.Value.Year == year)
                                .CountAsync();
                            month++;
                        }

                        eventAnalyzes.Add(new()
                        {
                            Name = $"{i} Квартал",
                            Count = count
                        });
                    }

                    break;
                }
            }

            return Ok(eventAnalyzes);
        }
        catch
            (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("results/filterId={filterId}")]
    public async Task<IActionResult> GetResults(int filterId)
    {
        try
        {
            List<ModuleAnalyze> analyzes = new();
            switch (filterId)
            {
                case 0:
                {
                    List<Position> positions = await context.Positions.ToListAsync();
                    foreach (var position in positions)
                    {
                        List<int?> errors = await context
                            .TestingResults.Include(c => c.Map)
                            .Where(c => c.Map.PositionId == position.Id)
                            .Select(c => c.Error)
                            .ToListAsync();

                        analyzes.Add(new()
                        {
                            Name = position.Name,
                            Errors = errors.Sum(c => c.Value)
                        });
                    }

                    break;
                }
                case 1:
                {
                    List<Department> departments = await context.Departments.ToListAsync();
                    foreach (var department in departments)
                    {
                        List<int?> errors = await context
                            .TestingResults.Include(c => c.Map)
                            .Where(c => c.Map.DepartmentId == department.Id)
                            .Select(c => c.Error)
                            .ToListAsync();

                        analyzes.Add(new()
                        {
                            Name = department.Name,
                            Errors = errors.Sum(c => c.Value)
                        });
                    }

                    break;
                }
            }

            return Ok(analyzes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}