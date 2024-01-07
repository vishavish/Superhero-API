using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;
using Superhero.Api.Models;

namespace Superhero.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationRepository organizationRepository, ILogger<OrganizationController> logger)
        {
            _organizationRepository = organizationRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
            return Ok(await _organizationRepository.GetOrganizations());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizationById(Guid id)
        {
            var organization = await _organizationRepository.GetOrganizationById(id);
            if(organization is null)
            {
                _logger.LogInformation("Organization {Id} was not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Organization {id} was not found.");
            }

            return Ok(Result<Organization>.Success(organization));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization(Organization organization)
        {
            _organizationRepository.AddOrganization(organization);
            await _organizationRepository.Save();

            return Ok(Result<Organization>.Success(organization));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(Guid id, Organization organization)
        {
            var org = await _organizationRepository.GetOrganizationById(id);
            if(org is null)
            {
                _logger.LogInformation("Organization {Id} was not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Organization {id} was not found.");
            }

            _organizationRepository.UpdateOrganization(organization);
            await _organizationRepository.Save();

            return Ok(Result<Organization>.Success(organization));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrganization(Guid id)
        {
            var org = await _organizationRepository.GetOrganizationById(id);
            if(org is null)
            {
                _logger.LogInformation("Organization {Id} was not found.", id);
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Organization {id} was not found.");
            }

            _organizationRepository.RemoveOrganization(org);
            await _organizationRepository.Save();

            return Ok(Result<Guid>.Success(id));
        }
    }
}
