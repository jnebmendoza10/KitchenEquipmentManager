using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenEquipmentManager.Domain.Models
{
    public class Site : BaseDomain
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public bool Active { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<RegisteredEquipment> RegisteredEquipment { get; set; }
    }
}
