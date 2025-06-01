using Azure.Messaging;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace TicketingSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BranchController : Controller
{
    private readonly IUnitOfWork context;
    private readonly JwtExtensions jwtExtensions;
    public BranchController(IUnitOfWork context, JwtExtensions jwtExtensions)
    {
        this.context = context;
        this.jwtExtensions = jwtExtensions;
    }

    [HttpPost]
    [Route("Branch")]
    public async Task<IActionResult> CreateBranch(Branch branch)
    {
        try
        {
            if (branch == null)
            {
                return BadRequest("Branch cannot be null.");
            }

            branch.DateCreated = DateTime.Now;
            branch.IsDeleted = false;

            context.BranchRepository.Add(branch);
            await context.SaveChangesAsync();

            return Ok(branch);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Branches")]
    public async Task<IActionResult> GetBranches(Guid organizationId)
    {
        try
        {
            var branches = await context.BranchRepository.GetBranchesByOrganizationId(organizationId);
            if (branches == null || !branches.Any())
            {
                return NotFound("No branches found for the provided organization ID.");
            }
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    [Route("Branch/{id}")]
    public async Task<IActionResult> GetBranchById(int id)
    {
        try
        {
            var branch = await context.BranchRepository.GetBranchById(id);
            if (branch == null)
            {
                return NotFound($"Branch with ID {id} not found.");
            }
            return Ok(branch);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    [Route("organization/{organizationId}")]
    public async Task<IActionResult> GetBranchesByOrganizationId(Guid organizationId)
    {
        try
        {
            var branches = await context.BranchRepository.GetBranchesByOrganizationId(organizationId);
            if (branches == null || !branches.Any())
            {
                return NotFound("No branches found for the provided organization ID.");
            }
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Branch/{id}")]
    public async Task<ApiResponse<Branch>> UpdateBranch([FromBody] Branch branch)
    {
        try
        {
            if (branch == null || branch.Oid <= 0)
            {
                return ApiResponse<Branch>.Fail("Invalid branch data.");
            }

            var existingBranch = await context.BranchRepository.GetBranchById(branch.Oid);
            if (existingBranch == null)
            {
                return ApiResponse<Branch>.Fail($"Branch with ID {branch.Name} not found.");
            }

            existingBranch.Name = branch.Name;
            existingBranch.Address = branch.Address;
            existingBranch.IsActive = branch.IsActive;
            existingBranch.OpeningDate = branch.OpeningDate;
            existingBranch.ClosingDate = branch.ClosingDate;
            existingBranch.IsDeleted = branch.IsDeleted;
            existingBranch.DateModified = DateTime.Now;

            context.BranchRepository.Update(existingBranch);
            await context.SaveChangesAsync();

            return ApiResponse<Branch>.Success(existingBranch);
        }
        catch (Exception ex)
        {
            return ApiResponse<Branch>.Fail(MessageConstants.GenericError);
        }
    }
    [HttpDelete]
    [Route("Branch/{id}")]
    public async Task<IActionResult> DeleteBranch(int key)
    {
        try
        {
            if (key <= 0)
            {
                return BadRequest("Invalid branch ID.");
            }

            var branch = await context.BranchRepository.GetBranchById(key);
            if (branch == null)
            {
                return NotFound($"Branch with name {branch.Name} not found.");
            }

            branch.IsDeleted = true;
            branch.DateModified = DateTime.Now;

            context.BranchRepository.Update(branch);
            await context.SaveChangesAsync();

            return Ok(branch);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}