using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace TicketingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamMemberController : Controller
{
    private readonly IUnitOfWork context;
    private readonly JwtExtensions jwtExtensions;

    public TeamMemberController(IUnitOfWork context, JwtExtensions jwtExtensions)
    {
        this.context = context;
        this.jwtExtensions = jwtExtensions;
    }

    [HttpPost]
    [Route("TeamMember")]
    public async Task<ApiResponse<TeamMember>> CreateTeamMember(TeamMember teamMember)
    {
        try
        {
            if (teamMember == null)
            {
                return ApiResponse<TeamMember>.Fail("Team Member cannot be null.");
            }

            teamMember.DateCreated = DateTime.Now;
            teamMember.IsDeleted = false;

            context.TeamMemberRepository.Add(teamMember);
            await context.SaveChangesAsync();

            return ApiResponse<TeamMember>.Success(teamMember);
        }
        catch (Exception ex)
        {
            return ApiResponse<TeamMember>.Fail(ex.Message);
        }
    }

    [HttpGet]
    [Route("TeamMembers")]
    public async Task<ApiResponse<object>> GetAllTeamMembers(Guid organizationId)
    {
        try
        {
            var teamMembers = await context.TeamMemberRepository.GetTeamMembersByOrganizationId(organizationId);
            if (teamMembers == null || !teamMembers.Any())
            {
                return ApiResponse<object>.Fail("No team members found.");
            }
            return ApiResponse<object>.Success(teamMembers);
        }
        catch (Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }
    [HttpGet]
    [Route("TeamMember/{id}")]
    public async Task<ApiResponse<TeamMember>> GetTeamMemberById(int id)
    {
        try
        {
            var teamMember = await context.TeamMemberRepository.GetTeamMemberById(id);
            if (teamMember == null)
            {
                return ApiResponse<TeamMember>.Fail("Team Member not found.");
            }
            return ApiResponse<TeamMember>.Success(teamMember);
        }
        catch (Exception ex)
        {
            return ApiResponse<TeamMember>.Fail(ex.Message);
        }
    }
    
    [HttpPut]
    [Route("TeamMember/{id}")]
    public async Task<ApiResponse<TeamMember>> UpdateTeamMember(int id, TeamMember teamMember)
    {
        try
        {
            if (teamMember == null)
            {
                return ApiResponse<TeamMember>.Fail("Team Member cannot be null.");
            }

            var existingTeamMember = await context.TeamMemberRepository.GetTeamMemberById(id);
            if (existingTeamMember == null)
            {
                return ApiResponse<TeamMember>.Fail("Team Member not found.");
            }

            existingTeamMember.UserAccountId = teamMember.UserAccountId;
            existingTeamMember.IsDeleted = false;
            existingTeamMember.DateModified = DateTime.Now;

            context.TeamMemberRepository.Update(existingTeamMember);
            await context.SaveChangesAsync();

            return ApiResponse<TeamMember>.Success(existingTeamMember);
        }
        catch (Exception ex)
        {
            return ApiResponse<TeamMember>.Fail(ex.Message);
        }
    }
    
    [HttpDelete]
    [Route("TeamMember/{id}")]
    public async Task<ApiResponse<TeamMember>> DeleteTeamMember(int id)
    {
        try
        {
            var teamMember = await context.TeamMemberRepository.GetTeamMemberById(id);
            if (teamMember == null)
            {
                return ApiResponse<TeamMember>.Fail("Team Member not found.");
            }

            teamMember.IsDeleted = true;
            teamMember.DateModified = DateTime.Now;

            context.TeamMemberRepository.Update(teamMember);
            await context.SaveChangesAsync();

            return ApiResponse<TeamMember>.Success(teamMember);
        }
        catch (Exception ex)
        {
            return ApiResponse<TeamMember>.Fail(ex.Message);
        }
    }
}