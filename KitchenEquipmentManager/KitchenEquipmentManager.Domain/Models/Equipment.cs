using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenEquipmentManager.Domain.Models
{
    public class Equipment : BaseDomain
    {
        [Required]
        [StringLength(8)]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Condition { get; set; }

        [Required]
        public Guid UserId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<RegisteredEquipment> RegisteredEquipment { get; set; }
    }
}
