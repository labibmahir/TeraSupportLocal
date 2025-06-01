using Domain.Dtos;
using Domain.Entities;
using Infrastructure;
using JWTConfiguration;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

namespace TicketingSystem.Controllers;
[Route("api/[controller]")]
[ApiController]

public class IncidentController : Controller
{
    private readonly IUnitOfWork context;
    private readonly JwtExtensions jwtExtensions;
    public IncidentController(IUnitOfWork context, JwtExtensions jwtExtensions)
    {
        this.context = context;
        this.jwtExtensions = jwtExtensions;
    }
    
    [HttpPost]
    [Route("Incident")]
     public async Task<ApiResponse<Incident>> CreateIncident(Incident incident)
     {
         try
         {
             incident.DateCreated = DateTime.Now;
             incident.IsDeleted = false;
             
             context.IncidentRepository.Add(incident);
             await context.SaveChangesAsync();
             
             if (incident.Images?.Count() > 0)
             {
                 foreach (var image in incident.Images)
                 {
                     Screenshot screenshot = new Screenshot()
                     {
                         IncidentId = incident.Oid,
                         Screenshots = Base64ToImage(image),
                         IsDeleted = false,
                         CreatedBy = incident.CreatedBy,
                         DateCreated = DateTime.Now,
                     };

                     context.ScreenshotRepository.Add(screenshot);
                     await context.SaveChangesAsync();
                 }
             }

             var incidentDb = await context.IncidentRepository.GetIncidentById(incident.Oid);
            // var result = await SendTicketCreationEmail(incidentDb);

             return ApiResponse<Incident>.Success(incidentDb);
         }
         catch (Exception ex)
         {
             return ApiResponse<Incident>.Fail(MessageConstants.GenericError);
         }
     }
     
     [HttpGet]
     [Route("Incidents")]
        public async Task<ApiResponse<object>> GetIncidentsByOrganizationId(Guid organizationId, int page, int pageSize, string search)
        {
            try
            {
                if(pageSize == 0)
                {
                    var incidents = await context.IncidentRepository.GetIncidentbyOrganizationId(organizationId);
                    if (incidents == null || !incidents.Any())
                    {
                        return ApiResponse<Object>.Fail(MessageConstants.NotFound);
                    }
                    return ApiResponse<Object>.Success(incidents);
                }
                else
                {
                    var incidents = await context.IncidentRepository.GetIncidentbyOrganizationId(organizationId, page, pageSize, search);
                    PagedResultDto<Incident> pagedResult = new PagedResultDto<Incident>
                    {
                        Data = incidents.ToList(),
                        PageNumber = page,
                        PageSize = pageSize,
                        //TotalItems = await context.IncidentRepository.GetIncidentCountByOrganizationId(organizationId, search)
                    };
                    return ApiResponse<Object>.Success(pagedResult);
                }
                
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.Fail(MessageConstants.GenericError);
            }
        }

    private byte[] Base64ToImage(string base64)
    {
        byte[] imagebytes = Convert.FromBase64String(base64);
        return imagebytes;
    }
}