using System.Collections.Generic;
using KitchenEquipmentManager.Domain.Models;

namespace KitchenEquipmentManager.Infrastructure.Services.Sites
{
    public interface ISiteService
    {
        List<Site> RetrieveSitesForUser(User user);
        bool AddSite(Site site);
        bool UpdateSite(Site site);
        bool DeleteSite(Site site);
    }
}
