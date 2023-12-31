﻿using Microsoft.EntityFrameworkCore;
using Superhero.Api.Context;
using Superhero.Api.Entities;
using Superhero.Api.Interfaces;

namespace Superhero.Api.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly AppDbContext _context;

        public OrganizationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Organization>> GetOrganizations()
        {
            return await _context.Organizations 
                !.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Organization?> GetOrganizationById(Guid id)
        {
            return await _context.Organizations
                !.AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void AddOrganization(Organization newOrganization)
        {
            _context.Organizations!.Add(newOrganization);
        }

        public void UpdateOrganization(Organization organization)
        {
            _context.Entry(organization).State = EntityState.Modified;
            _context.Organizations!.Update(organization);
        }

        public void RemoveOrganization(Organization organization)
        {
            _context.Organizations!.Remove(organization);
        }

    }
}
