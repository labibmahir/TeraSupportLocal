using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace TicketingSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TeamLeadController : Controller
{
   private readonly IUnitOfWork context;
   private readonly JwtExtensions jwtExtensions;

   public TeamLeadController(IUnitOfWork context, JwtExtensions jwtExtensions)
   {
      this.context = context;
      this.jwtExtensions = jwtExtensions;
   }
   [HttpPost]
   [Route("TeamLead")]
   public async Task<ApiResponse<TeamLead>> CreateTeamLead(TeamLead teamLead)
   {
      try
      {
         if (teamLead == null)
         {
            return ApiResponse<TeamLead>.Fail("Team Lead cannot be null.");
         }

         teamLead.DateCreated = DateTime.Now;
         teamLead.IsDeleted = false;

         context.TeamLeadRepository.Add(teamLead);
         await context.SaveChangesAsync();

         return ApiResponse<TeamLead>.Success(teamLead);
      }
      catch (Exception ex)
      {
         return ApiResponse<TeamLead>.Fail(MessageConstants.GenericError);
      }
   }
   
   [HttpGet]
   [Route("TeamLeads")]
   public async Task<ApiResponse<object>> GetTeamLeads( Guid organizationId)
   {
      try
      {
         var teamLeads = await context.TeamLeadRepository.GetTeamleadsByOrganizationId(organizationId);
         if (teamLeads == null || !teamLeads.Any())
         {
            return ApiResponse<object>.Fail("No team leads found.");
         }
         return ApiResponse<object>.Success(teamLeads);
      }
      catch (Exception ex)
      {
         return ApiResponse<object>.Fail(MessageConstants.GenericError);
      }
   }
   
   [HttpGet]
   [Route("TeamLead/{id}")]
   public async Task<ApiResponse<TeamLead>> GetTeamLeadById(int id)
   {
      try
      {
         var teamLead = await context.TeamLeadRepository.GetTeamLeadById(id);
         if (teamLead == null)
         {
            return ApiResponse<TeamLead>.Fail("Team Lead not found.");
         }
         return ApiResponse<TeamLead>.Success(teamLead);
      }
      catch (Exception ex)
      {
         return ApiResponse<TeamLead>.Fail(MessageConstants.GenericError);
      }
   }
   
   [HttpPut]
   [Route("TeamLead/{id}")]
   public async Task<ApiResponse<TeamLead>> UpdateTeamLead(int id, TeamLead teamLead)
   {
      try
      {
         if (teamLead == null)
         {
            return ApiResponse<TeamLead>.Fail("Team Lead cannot be null.");
         }

         var existingTeamLead = await context.TeamLeadRepository.GetTeamLeadById(id);
         if (existingTeamLead == null)
         {
            return ApiResponse<TeamLead>.Fail("Team Lead not found.");
         }

         existingTeamLead.UserAccountId = teamLead.UserAccountId;
         existingTeamLead.IsDeleted = false;
         existingTeamLead.DateModified = DateTime.Now;

         context.TeamLeadRepository.Update(existingTeamLead);
         await context.SaveChangesAsync();

         return ApiResponse<TeamLead>.Success(existingTeamLead);
      }
      catch (Exception ex)
      {
         return ApiResponse<TeamLead>.Fail(MessageConstants.GenericError);
      }
   }
   
   [HttpDelete]
   [Route("TeamLead/{id}")]
   public async Task<ApiResponse<TeamLead>> DeleteTeamLead(int id)
   {
      try
      {
         var teamLead = await context.TeamLeadRepository.GetTeamLeadById(id);
         if (teamLead == null)
         {
            return ApiResponse<TeamLead>.Fail("Team Lead not found.");
         }

         teamLead.IsDeleted = true;
         teamLead.DateModified = DateTime.Now;

         context.TeamLeadRepository.Update(teamLead);
         await context.SaveChangesAsync();

         return ApiResponse<TeamLead>.Success(teamLead);
      }
      catch (Exception ex)
      {
         return ApiResponse<TeamLead>.Fail(MessageConstants.GenericError);
      }
   }
}