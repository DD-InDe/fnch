using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EAS_Hub.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EAS_API.Controllers;

[ApiController]
[Route("api")]
public class Authorization(EasFullDbContext context) : ControllerBase
{
    [HttpGet("session/login={login},password={password}")]
    public async Task<IActionResult> LogIn(string login, string password)
    {
        try
        {
            Employee? employee =
                await context.Employees.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
            if (employee != null)
            {
                employee.Jwt = CreateToken(employee);
                return Ok(employee);
            }

            return NotFound("Пользователь не найден");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    private string CreateToken(Employee employee)
    {
        try
        {
            string role = employee.PositionId switch
            {
                1 or 2 or 3 => "Developer",
                4 => "Accessor",
                _ => "None"
            };

            var claims = new List<Claim>()
                { new Claim(ClaimTypes.Name, employee.Login), new Claim(ClaimTypes.Role, role) };
            var jwt = new JwtSecurityToken(issuer: "host", audience: "user", claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(3)),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey("QWERTY-QWERTY-QWERTY-QWERTY-QWERTY"u8.ToArray()),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}