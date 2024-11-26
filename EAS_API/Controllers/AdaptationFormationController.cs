using EAS_Hub.ApiModels;
using EAS_Hub.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EAS_API.Controllers;

[ApiController]
[Route("api/formation")]
public class AdaptationFormationController(EasFullDbContext context) : ControllerBase
{
    [Authorize(Policy = "Developer")]
    [HttpGet("modules/develop/employeeId={employeeId:int}")]
    public async Task<IActionResult> GetDevModules(int employeeId)
    {
        try
        {
            return Ok(await context
                .ModuleWorks.Where(c => c.EmployeeId == employeeId && c.RoleId == 1)
                .Select(c => c.Module)
                .ToListAsync());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [Authorize(Policy = "Accessor")]
    [HttpGet("modules/access/employeeId={employeeId:int}")]
    public async Task<IActionResult> GetAccessModules(int employeeId)
    {
        try
        {
            List<ModuleForAccessor> forAccessors = new();
            List<Module> modules = await context
                .ModuleWorks.Where(c => c.EmployeeId == employeeId && c.RoleId == 2)
                .Select(c => c.Module)
                .ToListAsync();
            foreach (var module in modules)
            {
                forAccessors.Add(new()
                {
                    Module = module,
                    Developers = await context
                        .ModuleWorks.Where(c => c.ModuleId == module.Id && c.RoleId == 1)
                        .Select(c => c.Employee)
                        .ToListAsync()
                });
            }

            return Ok(forAccessors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("modules/develop/moduleId={moduleId:int}")]
    public async Task<IActionResult> GetAllInfo(int moduleId)
    {
        try
        {
            ModuleDevelopInfo developInfo = new()
            {
                Module = await context
                    .Modules.Include(c => c.ResponsibleEmployee)
                    .Include(c => c.Previous)
                    .FirstAsync(c => c.Id == moduleId),
                Events = await context
                    .Events.Include(c => c.Type)
                    .Where(c => c.ModuleId == moduleId)
                    .ToListAsync(),
                Materials = await context
                    .Materials.Where(c => c.ModuleId == moduleId)
                    .ToListAsync(),
                Questions = await context
                    .Questions.Include(c => c.Testing)
                    .Include(c => c.Testing.Type)
                    .Where(c => c.Testing.ModuleId == moduleId)
                    .ToListAsync(),
                InlcudedPositions = await context
                    .ModuleAccesses.Where(c => c.ModuleId == moduleId)
                    .Select(c => c.Position)
                    .ToListAsync()
            };

            return Ok(developInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPut("modules/develop")]
    public async Task<IActionResult> UpdateModule([FromBody] ModuleDevelop develop)
    {
        try
        {
            Module tempModule = await context.Modules.FindAsync(develop.Module.Id);
            if (tempModule.StatusId is 3 or 4) return BadRequest("Этот модуль нельзя изменять");
            context.Entry(tempModule)
                .State = EntityState.Detached;

            Console.WriteLine(develop.Module.ResponsibleEmployeeId);
            develop.Module.StatusId = 2;
            context.Modules.Update(develop.Module);
            List<ModuleAccess> accesses = await context
                .ModuleAccesses.Where(c => c.ModuleId == develop.Module.Id)
                .ToListAsync();
            context.ModuleAccesses.RemoveRange(accesses);
            foreach (var developPosition in develop.Positions)
            {
                await context.ModuleAccesses.AddAsync(new()
                    { ModuleId = develop.Module.Id, PositionId = developPosition.Id });
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

    [HttpPost("modules/develop/events")]
    public async Task<IActionResult> AddEvent([FromBody] Event _event)
    {
        try
        {
            Module tempModule = await context
                .Modules.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == _event.ModuleId);
            if (tempModule.StatusId is 3 or 4) return BadRequest("Этот модуль нельзя изменять");
            if (tempModule.StatusId == 1)
            {
                tempModule.StatusId = 2;
                await context.SaveChangesAsync();
            }

            _event.Date = DateTime.Now;
            await context.Events.AddAsync(_event);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("modules/develop/materials")]
    public async Task<IActionResult> AddMaterial([FromBody] Material material)
    {
        try
        {
            Module tempModule = await context
                .Modules.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == material.ModuleId);
            if (tempModule.StatusId is 3 or 4) return BadRequest("Этот модуль нельзя изменять");
            if (tempModule.StatusId == 1)
            {
                tempModule.StatusId = 2;
                await context.SaveChangesAsync();
            }

            await context.Materials.AddAsync(material);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("modules/develop/questions")]
    public async Task<IActionResult> AddQuestion([FromBody] NewQuestion newQuestion)
    {
        try
        {
            Module tempModule = await context
                .Modules.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == newQuestion.ModuleId);
            if (tempModule.StatusId is 3 or 4) return BadRequest("Этот модуль нельзя изменять");
            if (tempModule.StatusId == 1)
            {
                tempModule.StatusId = 2;
                await context.SaveChangesAsync();
            }

            Testing? testing = await context.Testings.FirstOrDefaultAsync(c =>
                c.ModuleId == newQuestion.ModuleId && c.TypeId == newQuestion.TypeId);
            if (testing == null)
            {
                testing = new()
                {
                    ModuleId = newQuestion.ModuleId,
                    TypeId = newQuestion.TypeId
                };
                await context.Testings.AddAsync(testing);
                await context.SaveChangesAsync();
            }

            Question question = new()
            {
                Text = newQuestion.Text,
                Answer = newQuestion.Answer,
                TestingId = testing.Id
            };
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet("modules/toAccess/moduleId={moduleId:int}")]
    public async Task<IActionResult> ModuleToAccess(int moduleId)
    {
        try
        {
            Module module = await context.Modules.FindAsync(moduleId);
            module.StatusId = 3;
            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpPost("modules/access")]
    public async Task<IActionResult> AccessModule([FromBody] ModuleAccept accept)
    {
        try
        {
            List<ModuleWork> works = await context
                .ModuleWorks.Where(c => c.ModuleId == accept.ModuleId && c.RoleId == 2)
                .ToListAsync();
            ModuleWork work = works.First(c => c.EmployeeId == accept.ModuleId);
            work.Accepted = accept.IsAccepted;
            Module module = await context.Modules.FindAsync(accept.ModuleId);
            if (!accept.IsAccepted)
                module.StatusId = 2;
            if (works.All(c => c.Accepted != null && c.Accepted.Value))
                module.StatusId = 4;


            await context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}