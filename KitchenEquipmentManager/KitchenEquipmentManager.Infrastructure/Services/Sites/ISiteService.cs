using System.Collections.Generic;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Sites
{
    public interface ISiteService
    {
        List<Site> RetrieveSitesForUser(User user);
        void AddSite(Site site);
        void UpdateSite(Site site);
        void DeleteSite(Site site);
    }
}
