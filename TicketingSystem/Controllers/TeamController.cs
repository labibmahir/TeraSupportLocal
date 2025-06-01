using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace TicketingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : Controller
{
    private IUnitOfWork context;
    private readonly JwtExtensions jwtExtensions;

    public TeamController(IUnitOfWork context, JwtExtensions jwtExtensions)
    {
        this.context = context;
        this.jwtExtensions = jwtExtensions;
    }


    [HttpPost]
    [Route("Team")]
    public async Task<ApiResponse<Team>> CreateTeam(Team team)
    {
        try
        {
            if (team == null)
            {
                return ApiResponse<Team>.Fail("Team cannot be null.");
            }

            team.DateCreated = DateTime.Now;
            team.IsDeleted = false;

            context.TeamRepository.Add(team);
            await context.SaveChangesAsync();

            return ApiResponse<Team>.Success(team);
        }
        catch (Exception ex)
        {
            return ApiResponse<Team>.Fail(ex.Message);
        }
    }

    [HttpGet]
    [Route("Teams")]
    public async Task<ApiResponse<object>> GetAllTeams(Guid organizationId)
    {
        try
        {
            var teams = await context.TeamRepository.GetTeamsByOrganizationId(organizationId);
            if (teams == null || !teams.Any())
            {
                return ApiResponse<object>.Fail("No teams found.");
            }

            return ApiResponse<object>.Success(teams);
        }
        catch (Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }
    
    [HttpPut]
    [Route("Team/{id}")]
    public async Task<ApiResponse<Team>> UpdateTeam(int id, Team team)
    {
        try
        {
            if (team == null)
            {
                return ApiResponse<Team>.Fail("Team cannot be null.");
            }

            var existingTeam = await context.TeamRepository.GetByIdAsync(id);
            if (existingTeam == null)
            {
                return ApiResponse<Team>.Fail("Team not found.");
            }

            existingTeam.Name = team.Name;
            existingTeam.DateModified = DateTime.Now;
            existingTeam.IsDeleted = false;

            context.TeamRepository.Update(existingTeam);
            await context.SaveChangesAsync();

            return ApiResponse<Team>.Success(existingTeam);
        }
        catch (Exception ex)
        {
            return ApiResponse<Team>.Fail(ex.Message);
        }
    }

    [HttpDelete]
    [Route("Team/{id}")]
    public async Task<ApiResponse<Team>> DeleteTeam(int id)
    {
        try
        {
            var team = await context.TeamRepository.GetByIdAsync(id);
            if (team == null)
            {
                return ApiResponse<Team>.Fail("Team not found.");
            }

            team.IsDeleted = true;
            team.DateModified = DateTime.Now;

            context.TeamRepository.Update(team);
            await context.SaveChangesAsync();

            return ApiResponse<Team>.Success(team);
        }
        catch (Exception ex)
        {
            return ApiResponse<Team>.Fail(ex.Message);
        }
    }
}
        
