using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool AddSite(Site site)
        {
            try
            {
                _siteRepository.Add(site);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSite(Site site)
        {
            try
            {
                _siteRepository.Remove(site.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSite(Site site)
        {
            try
            {
                _siteRepository.Update(site, site.Id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Site> RetrieveSitesForUser(User user)
        {
            try
            {
                var sites = _siteRepository.GetAll().Where(x => x.Id == user.Id).ToList();

                return sites;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
