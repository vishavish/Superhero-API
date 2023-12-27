using Superhero.Api.Entities;

namespace Superhero.Api.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetOrganizations();
        Task<Organization?> GetOrganizationById(Guid id);
        Task<bool> Save();
        void AddOrganization(Organization newOrganization);
        void UpdateOrganization(Organization organization);
        void RemoveOrganization(Organization organization);
    }
}
