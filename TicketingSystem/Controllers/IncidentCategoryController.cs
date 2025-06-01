using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace TicketingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IncidentCategoryController : Controller
{
    private readonly IUnitOfWork context;
    private readonly JwtExtensions jwtExtensions;

    public IncidentCategoryController(IUnitOfWork context, JwtExtensions jwtExtensions)
    {
        this.context = context;
        this.jwtExtensions = jwtExtensions;
    }

    [HttpPost]
    [Route("IncidentCategory")]
    public async Task<ApiResponse<IncidentCategory>> CreateIncidentCategory(IncidentCategory incidentCategory)
    {
        try
        {
           

            incidentCategory.DateCreated = DateTime.Now;
            incidentCategory.IsDeleted = false;

            context.IncidentCategoryRepository.Add(incidentCategory);
            await context.SaveChangesAsync();


            return ApiResponse<IncidentCategory>.Success(incidentCategory);
            ;
        }
        catch (Exception)
        {
            return ApiResponse<IncidentCategory>.Fail(MessageConstants.GenericError);
        }
    }
    [HttpGet]
    [Route("IncidentCategory/{id}")]
    public async Task<ApiResponse<IncidentCategory>> GetIncidentCategoryById(int id)
    {
        try
        {
            var incidentCategory = await context.IncidentCategoryRepository.GetIncidentCategoryById(id);
            if (incidentCategory == null)
            {
                return ApiResponse<IncidentCategory>.Fail(MessageConstants.NotFound);
            }
            return ApiResponse<IncidentCategory>.Success(incidentCategory);
        }
        catch (Exception)
        {
            return ApiResponse<IncidentCategory>.Fail(MessageConstants.GenericError);
        }
    }
    [HttpGet]
    [Route("IncidentCategories")]
    public async Task<ApiResponse<object>> GetIncidentCategoriesByOrganizationId(Guid organizationId, int page , int pageSize , string search)
    {
        try
        {
            if (pageSize == 0)
            {
                var IncidentCategories = await context.IncidentCategoryRepository.GetIncidentCategoriesByOrganizationId(organizationId);
                
                if (IncidentCategories == null)
                    return ApiResponse<object>.Fail(MessageConstants.NotFound);
                
                return ApiResponse<object>.Success(IncidentCategories);
            }
            else
            {
                var incidentCategories = await context.IncidentCategoryRepository.GetIncidentCategoriesByOrganizationId(organizationId,page, pageSize, search);
                PagedResultDto<IncidentCategory> incidentCategoryDto = new PagedResultDto<IncidentCategory>
                {
                    Data = incidentCategories.ToList(),
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = await context.IncidentCategoryRepository.getIncidentCategoryCount(organizationId, search)
                };
                return  ApiResponse<object>.Success(incidentCategoryDto);
            }
            
        }
        catch (Exception)
        {
            return ApiResponse<object>.Fail(MessageConstants.GenericError);
        }
    }
}