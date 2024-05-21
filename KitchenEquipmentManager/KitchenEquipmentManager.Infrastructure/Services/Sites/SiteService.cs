using System;
using System.Collections.Generic;
using System.Linq;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Repository.Services;

namespace KitchenEquipmentManager.Infrastructure.Services.Sites
{
    public class SiteService : ISiteService
    {
        private readonly IDataRepository<Site> _siteRepository;
        public SiteService(IDataRepository<Site> siteRepository) 
        {
            _siteRepository = siteRepository ?? throw new ArgumentNullException(nameof(siteRepository));
        }
        public void AddSite(Site site)
        {
            try
            {
                _siteRepository.Add(site);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public void DeleteSite(Site site)
        {
            try
            {
                _siteRepository.Remove(site.Id);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public void UpdateSite(Site site)
        {
            try
            {
                _siteRepository.Update(site, site.Id);
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }

        public List<Site> RetrieveSitesForUser(User user)
        {
            try
            {
                var sites = _siteRepository.GetAll().ToList();

                var filteredSites = sites.Where(x => x.UserId == user.Id).ToList();

                return filteredSites;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
    }
}
